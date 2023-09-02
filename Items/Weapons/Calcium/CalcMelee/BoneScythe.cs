﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Common.Globals;
using TheSkeletronMod.projectiles.Calcprojs.CalcMeleeproj;


namespace TheSkeletronMod.Items.Weapons.Calcium.CalcMelee
{
    internal class BoneScythe : ModItem, MeleeWeaponWithImprovedSwing
    {
        public float swingDegree => 150;

        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeShootCustomProjectile(54, 33, 70, 7f, 25, 25, ItemUseStyleID.Swing, ModContent.ProjectileType<BoneScytheP>(), 10, false);
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            Item.UseSound = SoundID.Item1;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}