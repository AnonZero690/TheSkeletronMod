﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Tiles;
using System;
using Terraria.Utilities;

namespace TheSkeletronMod.Items.Weapons.Calcium
{
    public class BloodgemBoneScepter : ModItem
    {
        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeCustomProjectile(12, 12, 17, 2f, 20, 20, ItemUseStyleID.Shoot, ProjectileID.BloodArrow, true);
            Item.value = 10000;
            Item.crit = 5;
            Item.shootSpeed = 7;
            Item.noUseGraphic = false;
            Item.scale = 0.1f;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item5;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 1;
            Item.mana = 10;
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Ruby, 5);
            recipe.AddIngredient(ItemID.Diamond, 3);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.ViciousPowder, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Ruby, 5);
            recipe2.AddIngredient(ItemID.Diamond, 3);
            recipe2.AddIngredient(ItemID.Bone, 25);
            recipe2.AddIngredient(ItemID.VilePowder, 10);
            recipe2.AddIngredient(ItemID.SoulofNight, 5);
            recipe2.AddTile(ModContent.TileType<BoneAltar>());
            recipe2.Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0,-1);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 newPos = new Vector2(position.X - 3, position.Y + 4);
            base.ModifyShootStats(player, ref newPos, ref velocity, ref type, ref damage, ref knockback);
            position = position.PositionOFFSET(velocity, 50);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 500);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int randomNumber = Main.rand.Next(2,5);
            for (int i = 1; i < randomNumber; i++)
            {
                Vector2 vec = velocity.Vector2Evenly(randomNumber, 30, i);
                Projectile.NewProjectile(source, position, vec, type, damage, knockback, player.whoAmI, 0, 1);
            }


            return true;
        }
    }
}
