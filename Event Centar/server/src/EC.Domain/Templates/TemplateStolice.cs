using EC.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Templates
{
    public class TemplateStolice
    {
        [Key]
        public Guid IdTemplejtaStolice { get; set; }

        public int BrojStolice { get; set; }

        public double PozicijaX { get; set; }

        public double PozicijaY { get; set; }

        public double Pravac { get; set; }

        public bool Zauzeta { get; set; }

        [ForeignKey(nameof(TemplateStola))]
        public Guid IdTemplejtaStola { get; set; }

        public virtual TemplateStola TemplateStola { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }

    }
}
