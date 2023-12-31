using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSkeletronMod.Content.Buffs
{
    class BonedDebuff : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            Random random=new();
            Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), 1, 1, DustID.Stone, (float)(random.NextDouble()-0.5)*5, (float)(random.NextDouble()-0.5)*5, 30);
            npc.defense = npc.defDefense - 5;
        }
    }
}