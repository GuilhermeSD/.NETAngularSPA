using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        Task<Evento> GetEventosAsyncById(int EventoId, bool includePalestrantes);

        Task<Palestrante> GetPalestrantesAsyncById(int PalestranteId, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByNameAsync(string Nome, bool includeEventos);
    }
}