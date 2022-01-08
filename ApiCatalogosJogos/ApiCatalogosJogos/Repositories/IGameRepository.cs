using ApiCatalogosJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGame(int page, int quantity);
        Task<Game> GetGame(Guid id);
        Task<List<Game>> GetGame(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);

    }
}
