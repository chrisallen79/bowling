using System;
using System.Collections.Generic;
using Xunit;

using Bowling.Models;

namespace Bowling.Models.Tests
{
    public class BowlingModelTests
    {
        [Fact]
        public void ShouldReturn0PointsForEmptyGameScore()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now
            };

            // calculate score
            int perfectGameScore = theGame.CalculateScore();

            // assert
            Assert.Equal(0, perfectGameScore);
        }

        [Fact]
        public void ShouldReturn300PointsForPerfectGameScore()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 6, FrameSequence = 6, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 7, FrameSequence = 7, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 8, FrameSequence = 8, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 9, FrameSequence = 9, Roll1 = 10, GameId = 1 },
                    new Frame { FrameId = 10, FrameSequence = 10, Roll1 = 10, Roll2 = 10, Roll3 = 10, GameId = 1 }
                }
            };

            // calculate score
            int perfectGameScore = theGame.CalculateScore();

            // assert
            Assert.Equal(300, perfectGameScore);
        }

        [Fact]
        public void ShouldReturn110PointsForVenminderExampleGameScore()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 4, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 7, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 5, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 8, Roll2 = 1, GameId = 1 },
                    new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 4, Roll2 = 6, GameId = 1 },
                    new Frame { FrameId = 6, FrameSequence = 6, Roll1 = 2, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 7, FrameSequence = 7, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 8, FrameSequence = 8, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 9, FrameSequence = 9, Roll1 = 8, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 10, FrameSequence = 10, Roll1 = 10, Roll2 = 1, Roll3 = 7, GameId = 1 }
                }
            };

            // calculate score
            int gameScore = theGame.CalculateScore();

            // assert
            Assert.Equal(110, gameScore);
        }

        [Fact]
        public void ShouldGetThirdRollInTenthFrameForSpare()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 4, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 7, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 5, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 8, Roll2 = 1, GameId = 1 },
                    new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 4, Roll2 = 6, GameId = 1 },
                    new Frame { FrameId = 6, FrameSequence = 6, Roll1 = 2, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 7, FrameSequence = 7, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 8, FrameSequence = 8, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 9, FrameSequence = 9, Roll1 = 8, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 10, FrameSequence = 10, Roll1 = 8, Roll2 = 2, GameId = 1 }
                }
            };

            // roll
            Frame frameAfterRoll = theGame.Roll();

            // assert
            Assert.NotNull(frameAfterRoll);
            Assert.NotNull(frameAfterRoll.Roll3);
        }

        [Fact]
        public void ShouldGetThirdRollInTenthFrameForStrike()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 4, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 7, Roll2 = 3, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 5, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 8, Roll2 = 1, GameId = 1 },
                    new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 4, Roll2 = 6, GameId = 1 },
                    new Frame { FrameId = 6, FrameSequence = 6, Roll1 = 2, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 7, FrameSequence = 7, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 8, FrameSequence = 8, Roll1 = 8, Roll2 = 0, GameId = 1 },
                    new Frame { FrameId = 9, FrameSequence = 9, Roll1 = 8, Roll2 = 2, GameId = 1 },
                    new Frame { FrameId = 10, FrameSequence = 10, Roll1 = 10, GameId = 1 }
                }
            };

            // roll twice
            Frame frameAfterFirstRoll = theGame.Roll();
            Frame frameAfterSecondRoll = theGame.Roll();

            // assert
            Assert.NotNull(frameAfterFirstRoll);
            Assert.NotNull(frameAfterSecondRoll);
            Assert.NotNull(frameAfterFirstRoll.Roll2);
            Assert.NotNull(frameAfterSecondRoll.Roll3);
        }

        [Fact]
        public void ScoreShouldIncludeBonusForNextTwoNonStrikeRollsAfterStrike()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 10, GameId = 1 }
                }
            };

            // get the current score (should be 37)
            int currentTotal = theGame.CalculateScore();

            // assert
            Assert.Equal(37, currentTotal);

            // add in a new frame with 2 rolls
            theGame.Frames.Add(new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 5, Roll2 = 4, GameId = 1 });

            // get the score after the next frame
            int finalTotal = theGame.CalculateScore();

            // assert
            Assert.Equal(55, finalTotal);
        }

        [Fact]
        public void ScoreShouldIncludeBonusForNextNonStrikeRollAfterSpare()
        {
            // initialize game
            Game theGame = new Game
            {
                GameId = 1,
                GameStarted = DateTime.Now,
                Frames = new List<Frame>() {
                    new Frame { FrameId = 1, FrameSequence = 1, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 2, FrameSequence = 2, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 3, FrameSequence = 3, Roll1 = 5, Roll2 = 4, GameId = 1 },
                    new Frame { FrameId = 4, FrameSequence = 4, Roll1 = 8, Roll2 = 2, GameId = 1 }
                }
            };

            // get the current score (should be 37)
            int currentTotal = theGame.CalculateScore();

            // assert
            Assert.Equal(37, currentTotal);

            // add in a new frame with 2 rolls
            theGame.Frames.Add(new Frame { FrameId = 5, FrameSequence = 5, Roll1 = 5, Roll2 = 4, GameId = 1 });

            // get the score after the next frame
            int finalTotal = theGame.CalculateScore();

            // assert
            Assert.Equal(51, finalTotal);
        }
    }
}