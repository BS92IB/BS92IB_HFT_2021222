using BS92IB_HFT_2021222.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public class ArmamentManager : EntityManager<Armament>
    {
        public ArmamentManager(IRestService restService) : base(restService, "Manage Armaments", "armament")
        {
        }
        protected override void Create()
        {
            Armament armament = new Armament();
            var menu = GetEditorMenu(armament)
                .Add("Save", m =>
                {
                    // some validation, ship and weapon must be assigned
                    if (armament.ShipId != default)
                    {
                        if (armament.WeaponId != default)
                        {
                            restService.Post(armament, controller);
                            UIHelpers.PromptAnyKey("Saved armament");
                            m.CloseMenu();
                        }
                        else
                        {
                            UIHelpers.PromptAnyKey("The must have an assigned weapon design before it can be saved.");
                        }
                    }
                    else
                    {
                        UIHelpers.PromptAnyKey("The armament must be mounted on a ship before it can be saved.");
                    }
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Creating new Armament"); });
            menu.Show();
        }
        protected override void Show(Armament armament)
        {
            if (armament.Ship is null)
            {
                // reload ship, the [JsonIgnore] on the navigation property is ignored when requesting a ship directly
                armament = restService.Get<Armament>(armament.Id, controller);
            }
            Console.WriteLine("Armament details");
            Console.WriteLine();
            Console.WriteLine($"{armament.Name}");
            Console.WriteLine($"{armament.Quantity}x {armament.Weapon?.WeaponType}({armament.Weapon?.Designation})");
            Console.WriteLine($"Mounted on: {armament.Ship?.Name}");
            UIHelpers.PromptAnyKey();
        }

        protected override void Update(Armament armament)
        {
            var menu = GetEditorMenu(armament)
                .Add("Save", m =>
                {
                    restService.Put(armament, controller);
                    UIHelpers.PromptAnyKey("Saved armament");
                    m.CloseMenu();
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Editing Armament"); });
            menu.Show();
        }

        protected override void Delete(Armament armament)
        {
            if (UIHelpers.PromptConfirm($"Do you want to delete the armament {armament.Name}?"))
            {
                restService.Delete(armament.Id, controller);
                UIHelpers.PromptAnyKey("Deleted armament");
            }
        }

        public override void List(Action<Armament> armamentAction)
        {
            var armaments = restService.Get<Armament>(controller);
            var menu = new ConsoleMenu()
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Select an armament";
                    c.EnableWriteTitle = true;
                    c.EnableFilter = true;
                });

            // armament names don't make much sense without the ship name, but it's [JsonIgnore]'d
            // load the ships as well for lookup
            var ships = new ShipManager(restService).GetShipNames();

            menu.AddRange(armaments.Select(a =>
                Tuple.Create<string, Action>(
                    $"{ships[a.ShipId]} - {a.Name}", () =>
                    {
                        armamentAction(a);
                        menu.CloseMenu();
                    }
                )
            ));
            menu.Add("Back", m => m.CloseMenu());
            menu.Show();
        }

        private ConsoleMenu GetEditorMenu(Armament armament)
        {
            EntityManager<Weapon> weaponManager = new WeaponManager(restService);
            Action updateWeaponReference = () =>
            {
                // load referenced ship if there should be one (based on the foreign key) and it's not yet loaded, or it's not the correct one
                if (armament.WeaponId != default && (armament.Weapon is null || armament.Weapon.Id != armament.WeaponId))
                {
                    armament.Weapon = weaponManager.Get(armament.WeaponId);
                }
            };
            EntityManager<Ship> shipManager = new ShipManager(restService);
            Action updateShipReference = () =>
            {
                // load referenced ship if there should be one (based on the foreign key) and it's not yet loaded, or it's not the correct one
                if (armament.ShipId != default && (armament.Ship is null || armament.Ship.Id != armament.ShipId))
                {
                    armament.Ship = shipManager.Get(armament.ShipId);
                }
            };
            var menu = new ConsoleMenu()
                .AddPropertyEditor(() => $"Name: {armament.Name}", "New name: ", value => armament.Name = value)
                .AddPropertyEditor(() => $"Quantity: {armament.Name}", "New quantity: ", value => armament.Quantity = value)
                .AddReferenceEditor(() => $"Type: {armament.Weapon?.WeaponType}({armament.Weapon?.Designation})", weaponManager, value => armament.WeaponId = value, updateWeaponReference)
                .AddReferenceEditor(() => $"Mounted on ship: {armament.Ship?.Name}", shipManager, value => armament.ShipId = value, updateShipReference);
            updateWeaponReference();
            updateShipReference();
            return menu;
        }
    }
}
