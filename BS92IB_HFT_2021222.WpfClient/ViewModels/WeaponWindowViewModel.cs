using BS92IB_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BS92IB_HFT_2021222.WpfClient.ViewModels
{
    public class WeaponWindowViewModel:ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Weapon> Weapons { get; set; }

        private Weapon selectedWeapon;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public Weapon SelectedWeapon
        {
            get { return selectedWeapon; }
            set
            {
                if (value != null)
                {
                    selectedWeapon = new Weapon()
                    {
                        Id = value.Id,
                        Designation = value.Designation,
                        WeaponType = value.WeaponType,
                    };
                    OnPropertyChanged();
                    (DeleteWeaponCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateWeaponCommand as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }

        public ICommand CreateWeaponCommand {  get; set; }
        public ICommand DeleteWeaponCommand { get; set; }
        public ICommand UpdateWeaponCommand { get; set; }

        public WeaponWindowViewModel()
        {
            if(!IsInDesignMode)
            {
                Weapons = new RestCollection<Weapon>("http://localhost:21990/", "weapon", "hub");
                CreateWeaponCommand = new RelayCommand(() =>
                {
                    Weapons.Add(new Weapon()
                    {
                        Id = selectedWeapon.Id,
                        Designation = selectedWeapon.Designation,
                        WeaponType = selectedWeapon.WeaponType,
                        
                    });
                    System.Threading.Thread.Sleep(150);
                    Weapons.Update(Weapons.Last());
                });

                UpdateWeaponCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Weapons.Update(selectedWeapon);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteWeaponCommand = new RelayCommand(() =>
                {
                    Weapons.Delete(selectedWeapon.Id);
                },
                () =>
                {
                    return selectedWeapon != null;
                });
                selectedWeapon = new Weapon();
            }
        }

    }
}
