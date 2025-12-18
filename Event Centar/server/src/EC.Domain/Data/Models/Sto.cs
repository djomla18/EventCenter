using EC.Domain.Enums;
using EC.Domain.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class Sto
    {
        [Key]
        public Guid IdStola { get; set; }

        public string BrojStola { get; set; }

        public int KapacitetStola { get; set; }

        public OblikStola OblikStola { get; set; }

        public double XKordinata { get; set; }

        public double YKordinata { get; set; }

        public double Rotacija { get; set; }

        [ForeignKey(nameof(TemplateStola))]
        public Guid? IdTemplejtaStola { get; set; }

        [ForeignKey(nameof(Dogadjaj))]
        public Guid IdDogadjaja { get; set; }

        public virtual Dogadjaj Dogadjaj { get; set; }

        public virtual TemplateStola? TemplateStola { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }
    }
}
