using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.ChatSystem;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class MinecartCommandBlock: Minecart
    {
        public EntityMetadata Command = new EntityMetadata(
            12,
            EntityMetadataType.String,
            ""
        );

        public EntityMetadata LastOutput = new EntityMetadata(
            13,
            EntityMetadataType.Chat,
            new ChatBuilder()
        );
        
    }
}
