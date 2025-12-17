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
    public class Stolica
    {
        [Key]
        public Guid IdStolice { get; set; }

        public int BrojStolice { get; set; }

        public double PozicijaX { get; set; }

        public double PozicijaY { get; set; }

        public double Pravac { get; set; }

        public bool Zauzeta { get; set; }

        [ForeignKey(nameof(Sto))]
        public Guid IdStola { get; set; }

        [ForeignKey(nameof(TemplateStolice))]
        public Guid IdTemplejtaStolice { get; set; }

        public virtual Sto Sto { get; set; }

        public virtual TemplateStolice TemplateStolice { get; set; }
    }
}
