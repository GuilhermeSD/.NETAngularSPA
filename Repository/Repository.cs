using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public DataContext Context { get; }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes=false)
        {
            var retorno = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);
            if(includePalestrantes) 
            {
                retorno.Include(pe => pe.PalestrantesEventos).ThenInclude(p=>p.Palestrante);
            }
            return await retorno.ToArrayAsync();
        }

        public async Task<Evento> GetEventosAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> retorno = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);
            if(includePalestrantes) 
            {
                retorno.Include(pe => pe.PalestrantesEventos).ThenInclude(p=>p.Palestrante);
            }
            retorno = retorno.OrderByDescending(c=>c.DataEvento).Where(c=>c.Id.Equals(EventoId));
            return await retorno.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> retorno = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);
            if(includePalestrantes) 
            {
                retorno.Include(pe => pe.PalestrantesEventos).ThenInclude(p=>p.Palestrante);
            }
            retorno = retorno.OrderByDescending(c=>c.DataEvento).Where(c=>c.Tema.Contains(tema));
            return await retorno.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string Nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> retorno = _context.Palestrantes.Include(c => c.RedeSociais);
            if(includeEventos)
            {
                retorno.Include(pe=> pe.PalestrantesEventos).ThenInclude(e=>e.Evento);
            }
            retorno = retorno.Where(c=>c.Nome.ToLower().Contains(Nome.ToLower()));
            return await retorno.ToArrayAsync();        
        }

        public async Task<Palestrante> GetPalestrantesAsyncById(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> retorno = _context.Palestrantes.Include(c => c.RedeSociais);
            if(includeEventos)
            {
                retorno.Include(pe=> pe.PalestrantesEventos).ThenInclude(e=>e.Evento);
            }
            retorno = retorno.Where(c=>c.Id.Equals(PalestranteId));
            return await retorno.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync())> 0;
        }
        
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}