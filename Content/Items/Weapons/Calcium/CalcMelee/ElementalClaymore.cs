﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Common.Utils;
using TheSkeletronMod.Content.Buffs;
using TheSkeletronMod.Content.Projectiles.Calcprojs.CalcMeleeproj;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcMelee
{
    public class ElementalClaymore : ModItem
    {
        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeCustomProjectile(16, 16, 60, 4, 30, 30, ItemUseStyleID.Swing, ModContent.ProjectileType<ElementalClaymoreP>(), true);
            Item.shootSpeed = 10;
            Item.crit = 20;
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
        }
        int repeatTimes = 2;
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int timeToDebuff = 150;
            if (hit.Crit == true)
            {
                timeToDebuff = 200;
                for (repeatTimes = 0; repeatTimes < 3; repeatTimes++)
                {
                    int randomNumber = Main.rand.Next(1, 10);
                    if (randomNumber == 1)
                    {
                        target.AddBuff(BuffID.Burning, timeToDebuff);
                    }
                    if (randomNumber == 2)
                    {
                        target.AddBuff(BuffID.CursedInferno, timeToDebuff);
                    }
                    if (randomNumber == 3)
                    {
                        target.AddBuff(BuffID.Frostburn, timeToDebuff);
                    }
                    if (randomNumber == 4)
                    {
                        target.AddBuff(BuffID.Venom, timeToDebuff);
                    }
                    if (randomNumber == 5)
                    {
                        target.AddBuff(ModContent.BuffType<BonedDebuff>(), timeToDebuff); //change this to dungeon curse when it is added
                    }
                    if (randomNumber == 6)
                    {
                        target.AddBuff(BuffID.Stinky, timeToDebuff);
                        repeatTimes++;
                    }
                    if (randomNumber == 7)
                    {
                        target.AddBuff(BuffID.WitheredWeapon, timeToDebuff);
                    }
                    if (randomNumber == 8)
                    {
                        target.AddBuff(BuffID.ShadowFlame, timeToDebuff);
                    }
                    if (randomNumber == 9)
                    {
                        target.AddBuff(BuffID.Electrified, timeToDebuff);
                    }
                }
            }
            else
            {
                for (repeatTimes = 0; repeatTimes < 2; repeatTimes++)
                {
                    int randomNumber = Main.rand.Next(1, 10);
                    if (randomNumber == 1)
                    {
                        target.AddBuff(BuffID.Burning, timeToDebuff);
                    }
                    if (randomNumber == 2)
                    {
                        target.AddBuff(BuffID.CursedInferno, timeToDebuff);
                    }
                    if (randomNumber == 3)
                    {
                        target.AddBuff(BuffID.Frostburn, timeToDebuff);
                    }
                    if (randomNumber == 4)
                    {
                        target.AddBuff(BuffID.Venom, timeToDebuff);
                    }
                    if (randomNumber == 5)
                    {
                        target.AddBuff(ModContent.BuffType<BonedDebuff>(), timeToDebuff); //change this to dungeon curse when it is added
                    }
                    if (randomNumber == 6)
                    {
                        target.AddBuff(BuffID.Stinky, timeToDebuff);
                        repeatTimes++;
                    }
                    if (randomNumber == 7)
                    {
                        target.AddBuff(BuffID.WitheredWeapon, timeToDebuff);
                    }
                    if (randomNumber == 8)
                    {
                        target.AddBuff(BuffID.ShadowFlame, timeToDebuff);
                    }
                    if (randomNumber == 9)
                    {
                        target.AddBuff(BuffID.Electrified, timeToDebuff);
                    }
                }
            }
            
        }
    }
}
