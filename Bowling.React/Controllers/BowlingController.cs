using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Bowling.Models;
using Bowling.Models.Context;

namespace Bowling.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BowlingController : ControllerBase
    {

        private readonly BowlingContext _bowlingContext;

        private readonly ILogger<BowlingController> _logger;

        public BowlingController(ILogger<BowlingController> logger, BowlingContext bowlingContext)
        {
            _logger = logger;
            _bowlingContext = bowlingContext;
        }

        [HttpGet]
        [Route("games")]
        public List<Game> GetGames()
        {
            return _bowlingContext.Games.Include(g => g.Frames).ToList();
        }

        [HttpPost]
        [Route("games/new")]
        public IActionResult NewGame()
        {
            Game newGame = new();
            _bowlingContext.Add(newGame);
            _bowlingContext.SaveChanges();

            return Created(Request.Path.ToString() + '/' + newGame.GameId, newGame);
        }

        [HttpGet]
        [Route("games/{gameId}/roll")]
        public Frame Roll(int gameId)
        {
            Game theGame = _bowlingContext.Games.Include(g => g.Frames).FirstOrDefault<Game>(g => g.GameId == gameId);
            Frame updatedFrame = theGame.Roll();
            _bowlingContext.SaveChanges();

            return updatedFrame;
        }

        [HttpGet]
        [Route("games/{gameId}/score")]
        public int CalculateScore(int gameId)
        {
            Game theGame = _bowlingContext.Games.Include(g => g.Frames).FirstOrDefault<Game>(g => g.GameId == gameId);
            return theGame.CalculateScore();
        }
    }
}
