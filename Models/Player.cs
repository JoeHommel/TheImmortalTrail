using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TheImmortalTrail.Models
{
    public class Player
    {
        public int Score { get; set; } = 100;
        public string saveKey { get; set; } = "S1";

    }
}
