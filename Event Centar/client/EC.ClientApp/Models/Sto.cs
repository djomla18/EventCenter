using EC.ClientApp.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.ClientApp.Models
{
    public class Sto
    {
        public int BrojStola { get; set; }

        public int KapacitetStola { get; set; }

        public OblikStolaEnum OblikStola { get; set; }

        public double XKordinata { get; set; }

        public double YKordinata { get; set; }

        public double Rotacija { get; set; }
    }
}
