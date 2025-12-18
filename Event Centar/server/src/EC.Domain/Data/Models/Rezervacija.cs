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
    public class Rezervacija
    {
        [Key]
        public Guid IdRezervacije { get; set; }

        public Guid IdGrupeKojaJeUsla { get; set; }

        public DateTime DatumRezervacije { get; set; }

        [ForeignKey(nameof(Gost))]
        public Guid IdGosta { get; set; }

        [ForeignKey(nameof(Stolica))]
        public Guid IdStolice { get; set; }


        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Gost Gost { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Stolica Stolica { get; set; }

        public DateTime SysDtCreated { get; set; }

        public DateTime SysDtUpdated { get; set; }
    }
}
