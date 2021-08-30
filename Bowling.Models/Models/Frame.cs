using System;
using System.ComponentModel.DataAnnotations;

namespace Bowling.Models
{
    public class Frame
    {
        [Key]
        public int FrameId { get; set; }

        public int FrameSequence { get; set; }

        public int? Roll1 { get; set; }
        public int? Roll2 { get; set; }
        public int? Roll3 { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public int LastRoll()
        {
            if (this.Roll3 != null)
            {
                return 3;
            }
            else if (this.Roll2 != null)
            {
                return 2;
            }
            else if (this.Roll1 != null)
            {
                return 1;
            }
            return 0;
        }
    }
}





