using ApiCatalogosJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> GetGame(int page, int quantity)
        {
            var games = new List<Game>();
            var command = $"Select * From Games Order By Id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";


            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

              while (sqlDataReader.Read())
              {
                  games.Add(new Game
                  {
                      Id = (Guid)sqlDataReader["Id"],
                      Name = (string)sqlDataReader["Name"],
                      Producer = (string)sqlDataReader["Producer"],
                      Price = (double)sqlDataReader["Price"]
                  });
              }

            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> GetGame(Guid id)
        {
            Game game = null;

            var command = $"Select * From Games Where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            
            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                };
            }
            await sqlConnection.CloseAsync();
            return game;
        }

        public async Task<List<Game>> GetGame(string name, string producer)
        {
            var games = new List<Game>();

            var command = $"Select * From Games Where Name = '{name}' and Producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }
            await sqlConnection.CloseAsync();
            return games;

        }

        public async Task Insert(Game game)
        {
            var command = $"Insert Games (Id, Name, Producer, Price) Values ('{game.Id}', '{game.Name}', '{game.Producer}', '{game.Price.ToString().Replace(",", ".")}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();

        }

        public async Task Update(Game game)
        {
            var command = $"Update Games set Name = '{game.Name}', Producer = '{game.Producer}', Price = '{game.Price.ToString().Replace(",", ".")}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Delete(Guid id)
        {
            var command = $"Delete From Games Where Id = '{id}'";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();            
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();            
        }
    }
}
