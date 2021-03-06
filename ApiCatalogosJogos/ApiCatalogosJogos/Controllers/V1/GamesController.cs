using ApiCatalogosJogos.Exceptions;
using ApiCatalogosJogos.InputModel;
using ApiCatalogosJogos.Services;
using ApiCatalogosJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        ///  Buscar todos os jogos de foram paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual ágina está sendo consultada</param>
        /// <param name="quantity">Indica a quantidade de registros por página. Mínimo 1 e Máximo 50 </param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>        


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGame([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)                
        {
            var games = await _gameService.GetGame(page, quantity);

            if (games.Count() == 0)
                return NoContent();
            
            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid idGame)
        {
            var game = await _gameService.GetGame(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var jogo = await _gameService.Insert(gameInputModel);

                return Ok(jogo);
            }
            catch(GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }

        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);
                return Ok();
            }
            catch(UnregisteredGameException ex)
            {
                return NotFound("Não existe este jogo");
            }
            
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);
                return Ok();
            }
            catch(UnregisteredGameException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);
                return Ok();
            }
            catch(UnregisteredGameException ex)
            {
                return NotFound("Não existe este jogo");
            }

        }
    }
}
