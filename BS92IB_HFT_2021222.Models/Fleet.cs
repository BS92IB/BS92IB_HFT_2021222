using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Models
{
    public class Fleet : IEntity
    {
        public Fleet()
        {
            Ships = new HashSet<Ship>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ship> Ships { get; set; }
    }
}
