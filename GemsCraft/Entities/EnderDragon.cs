using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Entities.Metadata;

namespace GemsCraft.Entities
{
    public class EnderDragon: Insentient
    {
        public EntityMetadata DragonPhaseValue = new EntityMetadata(
            12,
            EntityMetadataType.VarInt,
            DragonPhase.Circling
        );
    }

    public enum DragonPhase
    {
        Circling = 0,
        StrafingPreparingToShootFire = 1,
        FlyingToPortalToLand = 2,
        LandingOnPortal = 3,
        TakingOffFromPortal = 4,
        LandedBreathingAttack = 5,
        LandedLookingForPlayer = 6,
        LandedRoarBeforeAttack = 7,
        ChargingPlayer = 8,
        FlyingToPortalToDie = 9,
        HoveringNoAI = 10
    }
}
