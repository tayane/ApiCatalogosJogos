using ApiCatalogosJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Repositories
{
    public class GameRepository : IGameRepository
    {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("9940548e-dadc-405f-83f9-57431685cf5d"), new Game{ Id = Guid.Parse("9940548e-dadc-405f-83f9-57431685cf5d"), Name = "Fifa 22", Producer = "EA", Price = 230} }
        };

        public Task<List<Game>> GetGame(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> GetGame(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> GetGame(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task<List<Game>> GetGameSemLamba(string name, string producer)
        {
            var retorno = new List<Game>();

            foreach(var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Producer.Equals(producer))
                    retorno.Add(game);
            }

            return Task.FromResult(retorno);
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //fechar conexão com o banco
        }

    }
}
