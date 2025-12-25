using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.ClientApp.Models
{
    public class MapaSedenja
    {
        public Guid IdMape { get; set; }

        public string NazivMape { get; set; }

        public string Opis { get; set; }

        public string RasporedJSON { get; set; }

        public int Kapacitet { get; set; }
    }
}
