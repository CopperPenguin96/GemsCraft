using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.Worlds
{
    public class World
    {
        public string LevelType { get; set; }
        public string Options { get; set; }
        public long Seed { get; set; }
        public Vector3 Spawn { get; set; }
        public int WaterLevel { get; set; }
        public string FileName { get; set; }
        public string WorldName { get; set; }
    }
}
