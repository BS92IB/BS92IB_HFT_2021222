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

namespace BS92IB_HFT_2021222.WpfClient.ViewModels
{
    public class FleetWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Fleet> Fleets { get; set; }

        private Fleet selectedFleet;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public Fleet SelectedFleet
        {
            get { return selectedFleet; }
            set
            {
                if (value != null)
                {
                    selectedFleet = new Fleet()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        
                    };
                    OnPropertyChanged();
                    (DeleteFleetCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateFleetCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }

        public ICommand CreateFleetCommand { get; set; }
        public ICommand DeleteFleetCommand { get; set; }
        public ICommand UpdateFleetCommand { get; set; }

        public FleetWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Fleets = new RestCollection<Fleet>("http://localhost:21990/", "fleet", "hub");
                CreateFleetCommand = new RelayCommand(() =>
                {
                    Fleets.Add(new Fleet()
                    {
                        Id = selectedFleet.Id,
                        Name = selectedFleet.Name,
                        

                    });
                    System.Threading.Thread.Sleep(150);
                    Fleets.Update(Fleets.Last());
                });

                UpdateFleetCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Fleets.Update(selectedFleet);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteFleetCommand = new RelayCommand(() =>
                {
                    Fleets.Delete(selectedFleet.Id);
                },
                () =>
                {
                    return selectedFleet != null;
                });
                selectedFleet = new Fleet();
            }
        }

    }
}
