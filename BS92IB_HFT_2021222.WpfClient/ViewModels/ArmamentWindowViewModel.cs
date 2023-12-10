using BS92IB_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BS92IB_HFT_2021222.WpfClient.ViewModels
{
    public class ArmamentWindowViewModel:ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Armament> Armaments { get; set; }
        private Armament selectedArmament;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public Armament SelectedArmament
        {
            get { return selectedArmament; }
            set
            {
                if (value != null)
                {
                    selectedArmament = new Armament()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Quantity = value.Quantity,
                        ShipId = value.ShipId,
                        WeaponId = value.WeaponId,
                        
                    };

                    OnPropertyChanged();
                    (DeleteArmamentCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateArmamentCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateArmamentCommand { get; set; }

        public ICommand DeleteArmamentCommand { get; set; }

        public ICommand UpdateArmamentCommand { get; set; }

        public ArmamentWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Armaments = new RestCollection<Armament>("http://localhost:21990/", "armament", "hub");
                CreateArmamentCommand = new RelayCommand(() =>
                {
                    Armaments.Add(new Armament()
                    {
                        Id = selectedArmament.Id,
                        Name = selectedArmament.Name,
                        Quantity = selectedArmament.Quantity,
                        ShipId = selectedArmament.ShipId,
                        WeaponId = selectedArmament.WeaponId,
                        
                        

                    });
                    System.Threading.Thread.Sleep(150);
                    Armaments.Update(Armaments.Last());
                });

                UpdateArmamentCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Armaments.Update(selectedArmament);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteArmamentCommand = new RelayCommand(() =>
                {
                    Armaments.Delete(selectedArmament.Id);
                },
                () =>
                {
                    return selectedArmament != null;
                });
                selectedArmament = new Armament();
            }
        }
    }
}
