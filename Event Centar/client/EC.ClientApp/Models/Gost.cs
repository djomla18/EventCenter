using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.ClientApp.Models
{
    class Gost
    {
        public Guid IdGosta { get; set; }

        public string Ime { get; set; }

        public string InicijalSrednjegImena { get; set; }

        public string Prezime { get; set; }


        public override string ToString()
        {
          return !string.IsNullOrEmpty(InicijalSrednjegImena) ? $"{Ime} {InicijalSrednjegImena}. {Prezime}" : $"{Ime} {Prezime}";
        }
    }
}
