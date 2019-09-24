using System;
using System.Collections.Generic;
using GemsCraft.Players;

public class PlayerList : List<Player>
{
    public new void Add(Player player)
    {
        foreach (Player p in this)
        {
            if (p.UUID == player.UUID)
            {
                throw new Exception("duplicate player");
            }
        }
        base.Add(player);
    }
}