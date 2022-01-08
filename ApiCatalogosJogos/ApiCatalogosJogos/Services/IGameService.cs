using ApiCatalogosJogos.InputModel;
using ApiCatalogosJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGame(int page, int quantity);
        Task<GameViewModel> GetGame(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);

    }
}
