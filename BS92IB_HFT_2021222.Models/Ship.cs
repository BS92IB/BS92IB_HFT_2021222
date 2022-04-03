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
    public class Ship : IEntity
    {
        public Ship()
        {
            Armaments = new HashSet<Armament>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string HullType { get; set; }
        public double Displacement { get; set; }
        public double Length { get; set; }
        public double Beam { get; set; }
        public double Draft { get; set; }
        public double MaxSpeedKnots { get; set; }
        public virtual ICollection<Armament> Armaments { get; set; }

        [ForeignKey(nameof(Fleet))]
        public int FleetId { get; set; }

        [JsonIgnore]
        public virtual Fleet Fleet { get; set; }
    }
}
