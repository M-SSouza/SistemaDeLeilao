using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeLeilao.Models;

namespace SistemaDeLeilao.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<SistemaDeLeilao.Models.Lances> Lances { get; set; }

    }
}
