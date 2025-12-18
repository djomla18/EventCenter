using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Domain.Data.Models
{
    public class EventCenter
    {
        [Key]
        public Guid EventCenterID { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public string EmailAdresa { get; set; }

        public string KontaktTelefon { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }
    }
}
