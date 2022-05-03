using BS92IB_HFT_2021222.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public class StatisticsManager
    {
        private readonly IRestService restService;
        public StatisticsManager(IRestService restService)
        {
            this.restService = restService;
        }

        public void Open()
        {
            var menu = new ConsoleMenu()
                .Add("Fastest Fleet", FastestFleet)
                .Add("Total displacement of fleet", TotalDisplacement)
                .Add("Total AA guns in fleet", TotalAaGuns)
                .Add("Most armed fleet", MostArmedFleet)
                .Add("Fleets without a carrier", FleetsWithoutCarrier)
                .Add("Back", ConsoleMenu.Close)
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Statistics";
                    c.EnableWriteTitle = true;
                });
            menu.Show();
        }

        private void FleetsWithoutCarrier()
        {
            var fleets = restService.Get<Fleet>("stat/fleetsWithoutCarrier");
            if (fleets.Count > 0)
            {
                Console.WriteLine("Fleets without a carrier (ship hull type is 'Carrier'):");
                Console.WriteLine();
                foreach (var fleet in fleets)
                {
                    Console.WriteLine(fleet.Name);
                }
            }
            else
            {
                Console.WriteLine("Every fleet has at least one carrier.");
            }
            UIHelpers.PromptAnyKey();
        }

        private void MostArmedFleet()
        {
            Console.WriteLine("The fleet with the highest combined quantity of armaments is considered the most armed.");
            Console.Write("The most armed fleet is: ");
            var fleet = restService.GetSingle<Fleet>("stat/mostArmedFleet");
            Console.WriteLine(fleet.Name);
            UIHelpers.PromptAnyKey();
        }

        private void TotalAaGuns()
        {
            EntityManager<Fleet> fleetManager = new FleetManager(restService);
            int fleetId = default;
            fleetManager.List(f => fleetId = f.Id);
            if (fleetId != default)
            {
                Console.Write("Total AA guns: ");
                int total = restService.Get<int>(fleetId, "stat/totalAaGuns");
                Console.WriteLine(total);
                UIHelpers.PromptAnyKey();
            }
        }

        private void TotalDisplacement()
        {
            EntityManager<Fleet> fleetManager = new FleetManager(restService);
            int fleetId = default;
            fleetManager.List(f => fleetId = f.Id);
            if (fleetId != default)
            {
                Console.Write("Total displacement: ");
                double total = restService.Get<double>(fleetId, "stat/totalDisplacement");
                Console.WriteLine($"{total}t");
                UIHelpers.PromptAnyKey();
            }
        }

        private void FastestFleet()
        {
            Console.WriteLine("The speed of a fleet is the speed of the slowest ship in the fleet.");
            Console.Write("The fastest fleet is: ");
            var fleet = restService.GetSingle<Fleet>("stat/fastestFleet");
            Console.WriteLine(fleet.Name);
            var fleetSpeed = fleet.Ships.Min(s => s.MaxSpeedKnots);
            Console.WriteLine($"Fleet max. speed: {fleetSpeed} knots");
            UIHelpers.PromptAnyKey();
        }
    }
}
