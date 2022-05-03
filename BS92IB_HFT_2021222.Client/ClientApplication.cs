using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public class ClientApplication
    {
        private readonly IRestService restService;
        public ClientApplication(IRestService restService)
        {
            this.restService = restService;
        }

        public void Start()
        {
            var menu = new ConsoleMenu()
                .Add("Fleet", Fleet)
                .Add("Ship", Ship)
                .Add("Armament", Armament)
                .Add("Weapon", Weapon)
                .Add("Statistics", Statistics)
                .Add("Close", ConsoleMenu.Close)
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = "Main Menu";
                    c.EnableWriteTitle = true;
                });
            menu.Show();

        }

        private void Statistics()
        {
            var manager = new StatisticsManager(restService);
            manager.Open();
        }

        private void Weapon()
        {
            var manager = new WeaponManager(restService);
            manager.Open();
        }

        private void Armament()
        {
            var manager = new ArmamentManager(restService);
            manager.Open();
        }

        private void Ship()
        {
            var manager = new ShipManager(restService);
            manager.Open();
        }

        private void Fleet()
        {
            var manager = new FleetManager(restService);
            manager.Open();
        }
    }
}
