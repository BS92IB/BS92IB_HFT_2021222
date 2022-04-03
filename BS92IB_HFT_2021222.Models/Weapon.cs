using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Models
{
    public class Weapon : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Designation { get; set; }
        public string WeaponType { get; set; }
    }
}
