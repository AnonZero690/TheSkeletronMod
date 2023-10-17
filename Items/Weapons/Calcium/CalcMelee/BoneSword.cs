﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using TheSkeletronMod.Common.Globals;
using TheSkeletronMod.projectiles.Calcprojs.CalcMeleeproj;

namespace TheSkeletronMod.Items.Weapons.Calcium.CalcMelee
{
    public class BoneSword : ModItem, MeleeWeaponWithImprovedSwing
    {
        public float swingDegree => 360;
        public float Offset => 0;
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 8));
        }
        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeShootProjectile(114, 114, 17, 0, 100, 100, ItemUseStyleID.Swing, ModContent.ProjectileType<BoneSwordP>(), 6f, false);
        }
    }
}