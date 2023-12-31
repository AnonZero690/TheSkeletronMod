﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Content.Projectiles.Calcprojs.CalcRangeProj;
using TheSkeletronMod.Content.Tiles;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcRange
{
    public class CartilageSpear : ModItem
    {
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<CartilageSpearProjectile>();

            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            Item.damage = 16;

            Item.width = 24;
            Item.height = 24;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 3;

            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.noUseGraphic = true;


        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 36);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            recipe.AddCondition(Condition.InGraveyard);
            recipe.Register();
        }
        public override void PostUpdate()
        {
            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.Bone, velocity);
                dust.scale = 0.25f;
                dust.fadeIn = 0.4f;
                dust.noGravity = true;
                dust.noLight = false;
                dust.alpha = 0;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Item.stack > 1)
            {
                for (int i = 1; i < Item.stack; i++)
                {
                    int p = Projectile.NewProjectile(player.GetSource_FromThis(), player.position, velocity.RotatedBy(i * Math.PI / 32), type, damage, knockback);
                }
               

            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = damage + damage * (Item.stack - 2) / 2;
        }

    }
}
