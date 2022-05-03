using BS92IB_HFT_2021222.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public class FleetManager : EntityManager<Fleet>
    {

        public FleetManager(IRestService restService) : base(restService, "Manage Fleets", "fleet")
        {
        }

        protected override void Create()
        {
            Console.WriteLine("Creating new Fleet");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Fleet fleet = new Fleet() { Name = name };

            if (UIHelpers.PromptConfirm($"Do you want to save Fleet {fleet.Name}?"))
            {
                restService.Post(fleet, controller);
                UIHelpers.PromptAnyKey("Saved fleet");
            }
        }

        protected override void Show(Fleet f)
        {
            Console.WriteLine("Fleet details");
            Console.WriteLine($"Name: {f.Name}");
            Console.WriteLine($"Number of ships in fleet: {f.Ships.Count}");

            foreach (var ship in f.Ships)
            {
                Console.WriteLine();
                Console.WriteLine(ship.Name);
                Console.WriteLine($"{ship.Class}-class {ship.HullType}");
                Console.WriteLine($"Displacement: {ship.Displacement}t");
                Console.WriteLine($"Length: {ship.Length}m");
                Console.WriteLine($"Beam: {ship.Beam}m");
                Console.WriteLine($"Draft: {ship.Draft}m");
                Console.WriteLine($"Max. speed: {ship.MaxSpeedKnots} knots");
            }
            UIHelpers.PromptAnyKey();
        }
        protected override void Update(Fleet f)
        {
            var menu = new ConsoleMenu()
                .AddPropertyEditor(() => $"Name: {f.Name}", "New fleet name: ", value => f.Name = value)
                .Add("Save", m =>
                {
                    restService.Put(f, controller);
                    UIHelpers.PromptAnyKey("Saved fleet");
                    m.CloseMenu();
                })
                .Add("Cancel", m =>
                {
                    m.CloseMenu();
                })
                .Configure(c => c.WriteHeaderAction = () => { Console.WriteLine("Editing Fleet"); });
            menu.Show();
        }


        protected override void Delete(Fleet f)
        {
            if (UIHelpers.PromptConfirm($"Do you want to delete Fleet {f.Name}?"))
            {
                restService.Delete(f.Id, controller);
                UIHelpers.PromptAnyKey("Deleted fleet");
            }
        }

        public override void List(Action<Fleet> fleetAction)
        {
            var fleets = restService.Get<Fleet>(controller);
            var menu = new ConsoleMenu()
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Select a fleet";
                    c.EnableWriteTitle = true;
                });

            menu.AddRange(fleets.Select(f =>
                Tuple.Create<string, Action>(
                    f.Name, () =>
                    {
                        fleetAction(f);
                        menu.CloseMenu();
                    }
                )
            ));
            menu.Add("Back", m => m.CloseMenu());
            menu.Show();
        }
    }
}
