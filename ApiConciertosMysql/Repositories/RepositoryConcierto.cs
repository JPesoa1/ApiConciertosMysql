using ApiConciertosMysql.Data;
using ApiConciertosMysql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosMysql.Repositories
{
    public class RepositoryConcierto
    {
        private ConciertoContext context;

        public RepositoryConcierto(ConciertoContext context)
        {
            this.context = context;
        }


        public async Task<List<CategoriaEvento>> GetCategoriasEventosAsync()
        {
            return await this.context.CategoriaEventos.ToListAsync();
        }

        public async Task<List<Eventos>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }

        public async Task<List<Eventos>> GetEventosPorCategoriaAsync(int idcategoria)
        {
            return await this.context.Eventos.Where(x =>x.IdCategoria==idcategoria).ToListAsync();
        }

        private int GetMaxId()
        {
            return this.context.Eventos.Max(x => x.IdEvento) + 1;
        }

        public async Task InsertarEventoAsync(string nombre,string artista,int categoria,string imagen)
        { 
            Eventos eventos = new Eventos();
            eventos.IdEvento = this.GetMaxId();
            eventos.Nombre = nombre;
            eventos.Artista = artista;
            eventos.IdCategoria = categoria;
            eventos.Imagen = imagen;

            await this.context.Eventos.AddAsync(eventos);
            await this.context.SaveChangesAsync();
        }
    }
}
