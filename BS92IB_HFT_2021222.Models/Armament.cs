using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Models
{
    public class Armament : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(Ship))]
        public int ShipId { get; set; }

        [JsonIgnore]
        public virtual Ship Ship { get; set; }

        [ForeignKey(nameof(Weapon))]
        public int WeaponId { get; set; }
        public virtual Weapon Weapon { get; set; }
    }
}
