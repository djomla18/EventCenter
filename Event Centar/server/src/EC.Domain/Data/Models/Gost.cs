using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class Gost
    {
        [Key]
        public Guid IdGosta { get; set; }

        public string Ime { get; set; }

        public string InicijalSrednjegImena { get; set; }

        public string Prezime { get; set; }

        [ForeignKey(nameof(Dogadjaj))]
        public Guid IdDogadjaja { get; set; }

        public virtual Dogadjaj Dogadjaj { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }

        public override string ToString()
        {
            return $"{Ime} {InicijalSrednjegImena}. {Prezime}";
        }
    }
}
