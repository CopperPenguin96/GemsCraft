using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class Phantom: Flying
    {
        public EntityMetadata Size = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            false
        );

        public double HorizontalHitBoxSize(double size)
        {
            return 0.9 + 0.2 * size;
        }

        public double VerticalHitBoxSize(double size)
        {
            return 0.5 + 0.1 * size;
        }
    }
}
