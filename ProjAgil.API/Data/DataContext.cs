using Microsoft.EntityFrameworkCore;
using ProjAgil.API.Model;

namespace ProjAgil.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {

        }

        public DbSet<Evento> Eventos {get; set;}
        
    }
}