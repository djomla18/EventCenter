using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class MapaSedenja
    {
        [Key]
        public Guid IdMape { get; set; }

        public string NazivMape { get; set; }

        public string Opis { get; set; }

        public string RasporedJSON{ get; set; }

        public int Kapacitet { get; set; }

        [ForeignKey(nameof(EventCenter))]
        public Guid EventCenterID { get; set; }

        public virtual EventCenter EventCenter { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }
    }
}
