using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class Totem
    {
        [Key]
        public Guid IdTotema { get; set; }

        public string Lokacija { get; set; }

        public bool Aktivan { get; set; }

        [ForeignKey(nameof(EventCenter))]
        public Guid EventCenterID { get; set; }

        [ForeignKey(nameof(Dogadjaj))]
        public Guid? IdDogadjaja {  get; set; }  

        public virtual Dogadjaj Dogadjaj { get; set; }

        public virtual EventCenter EventCentar{ get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }
    }
}
