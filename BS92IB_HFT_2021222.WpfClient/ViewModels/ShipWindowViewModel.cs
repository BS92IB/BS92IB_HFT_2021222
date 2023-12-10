using BS92IB_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace BS92IB_HFT_2021222.WpfClient.ViewModels
{
    public class ShipWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Ship> Ships { get; set; }
        private Ship selectedShip;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public Ship SelectedShip
        {
            get { return selectedShip; }
            set
            {
                if (value != null)
                {
                    selectedShip = new Ship()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Class = value.Class,
                        HullType = value.HullType,
                        Displacement = value.Displacement,
                        Length = value.Length,
                        Beam = value.Beam,
                        Draft = value.Draft,
                        MaxSpeedKnots = value.MaxSpeedKnots,
                        FleetId = value.FleetId,

                    };

                    OnPropertyChanged();
                    (DeleteShipCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateShipCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateShipCommand { get; set; }

        public ICommand DeleteShipCommand { get; set; }

        public ICommand UpdateShipCommand { get; set; }

        public ShipWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Ships = new RestCollection<Ship>("http://localhost:21990/", "ship", "hub");
                CreateShipCommand = new RelayCommand(() =>
                {
                    Ships.Add(new Ship()
                    {
                        Id = selectedShip.Id,
                        Name = selectedShip.Name,
                        Class = selectedShip.Class,
                        HullType = selectedShip.HullType,
                        Displacement = selectedShip.Displacement,
                        Length = selectedShip.Length,
                        Beam = selectedShip.Beam,
                        Draft = selectedShip.Draft,
                        MaxSpeedKnots = selectedShip.MaxSpeedKnots,
                        FleetId = selectedShip.FleetId,



                    });
                    System.Threading.Thread.Sleep(150);
                    Ships.Update(Ships.Last());
                });

                UpdateShipCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Ships.Update(selectedShip);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteShipCommand = new RelayCommand(() =>
                {
                    Ships.Delete(selectedShip.Id);
                },
                () =>
                {
                    return selectedShip != null;
                });
                selectedShip = new Ship();
            }
        }
    }
}
