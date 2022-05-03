using BS92IB_HFT_2021222.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public class ShipManager : EntityManager<Ship>
    {
        public ShipManager(IRestService restService) : base(restService, "Manage Ships", "ship")
        {
        }

        protected override void Create()
        {
            Ship ship = new Ship();
            var menu = GetEditorMenu(ship)
                .Add("Save", m =>
                {
                    // some validation, fleet must be assigned
                    if (ship.FleetId != default)
                    {
                        restService.Post(ship, controller);
                        UIHelpers.PromptAnyKey("Saved ship");
                        m.CloseMenu();
                    }
                    else
                    {
                        UIHelpers.PromptAnyKey("The ship must be assigned to a fleet before it can be saved.");
                    }
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Creating new Ship"); });
            menu.Show();
        }

        protected override void Show(Ship ship)
        {
            if (ship.Fleet is null)
            {
                // reload ship, the [JsonIgnore] on the navigation property is ignored when requesting a ship directly
                ship = restService.Get<Ship>(ship.Id, controller);
            }
            Console.WriteLine("Ship details");
            Console.WriteLine();
            Console.WriteLine(ship.Name);
            Console.WriteLine($"{ship.Class}-class {ship.HullType}");
            Console.WriteLine($"Displacement: {ship.Displacement}t");
            Console.WriteLine($"Length: {ship.Length}m");
            Console.WriteLine($"Beam: {ship.Beam}m");
            Console.WriteLine($"Draft: {ship.Draft}m");
            Console.WriteLine($"Max. speed: {ship.MaxSpeedKnots} knots");
            Console.WriteLine($"Fleet: {ship.Fleet.Name}");
            Console.WriteLine();
            Console.WriteLine("Armaments:");
            foreach (var armament in ship.Armaments)
            {
                Console.WriteLine();
                Console.WriteLine($"{armament.Name}");
                Console.WriteLine($"{armament.Quantity}x {armament.Weapon.WeaponType}({armament.Weapon.Designation})");
            }
            UIHelpers.PromptAnyKey();
        }

        protected override void Update(Ship ship)
        {
            var menu = GetEditorMenu(ship)
                .Add("Save", m =>
                {
                    restService.Put(ship, controller);
                    UIHelpers.PromptAnyKey("Saved ship");
                    m.CloseMenu();
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Editing Ship"); });
            menu.Show();
        }
        protected override void Delete(Ship ship)
        {
            if (UIHelpers.PromptConfirm($"Do you want to delete the ship {ship.Name}?"))
            {
                restService.Delete(ship.Id, controller);
                UIHelpers.PromptAnyKey("Deleted ship");
            }
        }

        public override void List(Action<Ship> shipAction)
        {
            var ships = restService.Get<Ship>(controller);
            var menu = new ConsoleMenu()
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Select a ship";
                    c.EnableWriteTitle = true;
                    c.EnableFilter = true;
                });

            menu.AddRange(ships.Select(s =>
                Tuple.Create<string, Action>(
                    s.Name, () =>
                    {
                        shipAction(s);
                        menu.CloseMenu();
                    }
                )
            ));
            menu.Add("Back", m => m.CloseMenu());
            menu.Show();
        }

        private ConsoleMenu GetEditorMenu(Ship ship)
        {
            EntityManager<Fleet> fleetManager = new FleetManager(restService);
            Action updateFleetReference = () =>
            {
                // load referenced fleet if there should be one (based on the foreign key) and it's not yet loaded, or it's not the correct one
                if (ship.FleetId != default && (ship.Fleet is null || ship.Fleet.Id != ship.FleetId))
                {
                    ship.Fleet = fleetManager.Get(ship.FleetId);
                }
            };

            var menu = new ConsoleMenu()
                .AddPropertyEditor(() => $"Name: {ship.Name}", "New name: ", value => ship.Name = value)
                .AddPropertyEditor(() => $"Class: {ship.Class}", "New class: ", value => ship.Class = value)
                .AddPropertyEditor(() => $"Hull type: {ship.HullType}", "New hull type: ", value => ship.HullType = value)
                .AddPropertyEditor(() => $"Displacement: {ship.Displacement}t", "New displacement: ", (double value) => ship.Displacement = value)
                .AddPropertyEditor(() => $"Length: {ship.Length}m", "New length: ", (double value) => ship.Length = value)
                .AddPropertyEditor(() => $"Beam: {ship.Beam}m", "New beam: ", (double value) => ship.Beam = value)
                .AddPropertyEditor(() => $"Draft: {ship.Draft}m", "New draft: ", (double value) => ship.Draft = value)
                .AddPropertyEditor(() => $"Max. speed: {ship.MaxSpeedKnots} knots", "New max. speed: ", (double value) => ship.MaxSpeedKnots = value)
                .AddReferenceEditor(() => $"Fleet: {ship.Fleet?.Name}", fleetManager, value => ship.FleetId = value, updateFleetReference);
            updateFleetReference();
            return menu;
        }

        public IDictionary<int, string> GetShipNames()
        {
            return restService.Get<Ship>(controller)
                .ToDictionary(s => s.Id, s => s.Name);
        }
    }
}
