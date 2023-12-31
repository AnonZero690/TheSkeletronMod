﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Common.Utils;
using TheSkeletronMod.Content.Buffs;
using TheSkeletronMod.Content.Projectiles.Calcprojs.CalcSummProj;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcSumm
{
    class BoneWand : ModItem
    {
        public override string Texture => SkeletronUtils.GetVanillaTexture<Item>(ItemID.BoneWand);
        public override void SetDefaults()
        {
            Item.buffType = ModContent.BuffType<BoneWandBuff>();
            Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.damage = 25;
            Item.knockBack = 5f;
            Item.value = 12000;
            Item.useAnimation = 10;
            Item.useTime = 20;
            Item.shootSpeed = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.mana = 10;
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            Item.shoot = ModContent.ProjectileType<BoneWandSummon>();
            Item.UseSound = SoundID.AbigailSummon;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            player.AddBuff(ModContent.BuffType<BoneWandBuff>(), 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
    }
}
