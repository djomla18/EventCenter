using EC.Domain.Data.Models;
using EC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Templates
{
    public class TemplateStola
    {
        public Guid IdTemplejtaStola { get; set; }

        public string BrojStola { get; set; }

        public int KapacitetStola { get; set; }

        public OblikStola OblikStola { get; set; }

        public double XKordinata { get; set; }

        public double YKordinata { get; set; }  

        public double Rotacija { get; set; }

        [ForeignKey(nameof(MapaSedenja))]
        public Guid IdMape { get; set; }

        public virtual MapaSedenja MapaSedenja { get; set; }
    }
}
