using EC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class Dogadjaj
    {
        [Key]
        public Guid IdDogadjaja { get; set; }

        public string NazivDogadjaja { get; set; }

        public string OpisDogadja { get; set; }

        public DateTime DatumDogadjaja { get; set;}

        public EventStatus StatusDogadjaja { get; set; }

        [ForeignKey(nameof(EventCenter))]
        public Guid EventCenterID { get; set; }

        public virtual EventCenter EventCentar { get; set; }  
    }
}
