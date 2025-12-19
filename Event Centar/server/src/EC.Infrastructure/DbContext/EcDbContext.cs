using EC.Domain.Data.Models;
using EC.Domain.Templates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Infrastructure.DbContext
{
    public class EcDbContext(DbContextOptions<EcDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public virtual DbSet<Dogadjaj> Dogadjaji { get; set; }

        public virtual DbSet<EventCenter> EventCentri { get; set; }

        public virtual DbSet<Gost> Gosti { get; set; }

        public virtual DbSet<MapaSedenja> MapeSedenja { get; set; }

        public virtual DbSet<Rezervacija> Rezervacije { get; set; }

        public virtual DbSet<Sto> Stolovi { get; set; } 

        public virtual DbSet<Stolica> Stolice { get; set; }

        public virtual DbSet<Totem> Totemi { get; set; }

        public virtual DbSet<TemplateStolice> TemplateoviStolica { get; set; }

        public virtual DbSet<TemplateStola> TemplateoviStolova { get; set; }


    }
}
