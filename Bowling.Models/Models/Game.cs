using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bowling.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public DateTime? GameStarted { get; set; }
        public int LastFrame { get; set; }

        public List<Frame> Frames { get; set; }

        public Game()
        {
            // initialize current frame sequence to 1
            this.LastFrame = 0;

            // initialize frames list
            if (this.Frames == null) this.Frames = new List<Frame>();
        }

        public Frame Roll()
        {
            if (this.Frames != null)
            {
                // if 0 frames, create a new frame
                if (this.Frames.Count == 0)
                {
                    this.LastFrame += 1;
                    Frame nextFrame = new Frame()
                    {
                        GameId = this.GameId,
                        FrameSequence = this.LastFrame
                    };
                    this.Frames.Add(nextFrame);
                }

                Frame lastFrame = this.Frames.Last<Frame>();

                if (this.Frames.Count < 10)
                {
                    if (lastFrame.LastRoll() < 2 && lastFrame.Roll1 != 10)
                    {
                        switch (lastFrame.LastRoll())
                        {
                            case 0:
                                lastFrame.Roll1 = SimulateRoll(10);
                                break;
                            case 1:
                                lastFrame.Roll2 = SimulateRoll(10 - lastFrame.Roll1.Value);
                                break;
                        }
                        return lastFrame;
                    }
                    else
                    {
                        this.LastFrame += 1;
                        Frame nextFrame = new Frame()
                        {
                            GameId = this.GameId,
                            Roll1 = SimulateRoll(10),
                            FrameSequence = this.LastFrame
                        };
                        this.Frames.Add(nextFrame);
                        return nextFrame;
                    }
                }
                else
                {
                    if (lastFrame.LastRoll() < 2)
                    {
                        switch (lastFrame.LastRoll())
                        {
                            case 0:
                                lastFrame.Roll1 = SimulateRoll(10);
                                break;
                            case 1:
                                // if first roll was strike, remaining pins is 10
                                int remainingPins = lastFrame.Roll1 == 10 ? 10 : 10 - lastFrame.Roll1.Value;
                                lastFrame.Roll2 = SimulateRoll(remainingPins);
                                break;
                        }
                    }
                    else if (lastFrame.LastRoll() == 2 && lastFrame.Roll1 == 10)
                    {
                        int remainingPins = lastFrame.Roll2 == 10 ? 10 : 10 - lastFrame.Roll2.Value;
                        lastFrame.Roll3 = SimulateRoll(remainingPins);
                    }
                    else if (lastFrame.LastRoll() == 2 && lastFrame.Roll1 + lastFrame.Roll2 == 10)
                    {
                        lastFrame.Roll3 = SimulateRoll(10);
                    }
                    else
                    {
                        // return null if no changes were made
                        return null;
                    }
                    return lastFrame;
                }
            }
            return null;
        }

        private static int SimulateRoll(int remainingPins)
        {
            Random rnd = new();
            return rnd.Next(0, remainingPins + 1);
        }

        public int CalculateScore()
        {
            int totalScore = 0;
            for (int i = 0; i < Frames.Count; i++)
            {
                // exit if there are more than 10 frames
                if (i > 9) break;

                int roll1 = Frames[i]?.Roll1 ?? 0;
                int roll2 = Frames[i]?.Roll2 ?? 0;
                int roll3 = Frames[i]?.Roll3 ?? 0;

                // always add current frame rolls
                int frameScore = roll1 + roll2;

                if (i == 9)
                {
                    frameScore += roll3;
                }
                else
                {
                    int nextRoll1 = i + 1 < Frames.Count ? Frames[i + 1]?.Roll1 ?? 0 : 0;
                    int nextRoll2 = i + 1 < Frames.Count ? Frames[i + 1]?.Roll2 ?? 0 : 0;

                    int finalRoll;
                    if (i == 8)
                    {
                        finalRoll = nextRoll2;
                    }
                    else
                    {
                        finalRoll = i + 2 < Frames.Count ? Frames[i + 2]?.Roll1 ?? 0 : 0;
                    }

                    // handle strikes and spares
                    if (roll1 == 10)
                    {
                        if (nextRoll1 == 10)
                        {
                            frameScore += nextRoll1 + finalRoll;
                        }
                        else
                        {
                            frameScore += nextRoll1 + nextRoll2;
                        }
                    }
                    else if (frameScore == 10)
                    {
                        frameScore += nextRoll1;
                    }
                }

                totalScore += frameScore;
            }
            return totalScore;
        }
    }
}