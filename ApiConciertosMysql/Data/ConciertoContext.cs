using ApiConciertosMysql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosMysql.Data
{
    public class ConciertoContext : DbContext
    {
        public ConciertoContext(DbContextOptions<ConciertoContext> options) : base(options)
        {
        }

        public DbSet<Eventos> Eventos {get;set;}
        public DbSet<CategoriaEvento> CategoriaEventos { get;set;}
    }
}
