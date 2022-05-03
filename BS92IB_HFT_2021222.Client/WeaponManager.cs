using BS92IB_HFT_2021222.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    internal class WeaponManager : EntityManager<Weapon>
    {
        public WeaponManager(IRestService restService) : base(restService, "Manage Weapon designs", "weapon")
        {
        }
        protected override void Create()
        {
            Weapon weapon = new Weapon();
            var menu = GetEditorMenu(weapon)
                .Add("Save", m =>
                {
                    restService.Post(weapon, controller);
                    UIHelpers.PromptAnyKey("Saved weapon design");
                    m.CloseMenu();
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Creating new Weapon design"); });
            menu.Show();
        }
        protected override void Show(Weapon weapon)
        {
            Console.WriteLine("Weapon design details");
            Console.WriteLine();
            Console.WriteLine($"Weapon type: {weapon.WeaponType}");
            Console.WriteLine($"Designation: {weapon.Designation}");
            UIHelpers.PromptAnyKey();
        }

        protected override void Update(Weapon weapon)
        {
            var menu = GetEditorMenu(weapon)
                .Add("Save", m =>
                {
                    restService.Put(weapon, controller);
                    UIHelpers.PromptAnyKey("Saved weapon design");
                    m.CloseMenu();
                })
                .Add("Cancel", m => m.CloseMenu())
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Editing Weapon design"); });
            menu.Show();
        }

        private ConsoleMenu GetEditorMenu(Weapon weapon)
        {
            return new ConsoleMenu()
                .AddPropertyEditor(() => $"Weapon Type {weapon.WeaponType}", "New type: ", value => weapon.WeaponType = value)
                .AddPropertyEditor(() => $"Designation {weapon.Designation}", "New designation: ", value => weapon.Designation = value);
        }

        protected override void Delete(Weapon weapon)
        {
            if (UIHelpers.PromptConfirm($"Do you want to delete the weapon design {weapon.WeaponType}({weapon.Designation})?"))
            {
                restService.Delete(weapon.Id, controller);
                UIHelpers.PromptAnyKey("Deleted weapon design");
            }
        }

        public override void List(Action<Weapon> weaponAction)
        {
            var weapons = restService.Get<Weapon>(controller);
            var menu = new ConsoleMenu()
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Select a weapon design";
                    c.EnableWriteTitle = true;
                    c.EnableFilter = true;
                });

            menu.AddRange(weapons.Select(w =>
                Tuple.Create<string, Action>(
                    $"{w.WeaponType}({w.Designation})", () =>
                    {
                        weaponAction(w);
                        menu.CloseMenu();
                    }
                )
            ));
            menu.Add("Back", m => m.CloseMenu());
            menu.Show();
        }
    }
}
