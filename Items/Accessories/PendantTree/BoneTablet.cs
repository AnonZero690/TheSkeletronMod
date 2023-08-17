﻿using Microsoft.Xna.Framework;
using TheSkeletronMod.Items.Materials;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TheSkeletronMod.Common.DamageClasses;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TheSkeletronMod.Tiles;
using Terraria.DataStructures;

namespace TheSkeletronMod.Items.Accessories.PendantTree
{
    public class BoneTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 14));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 23;
            Item.height = 30;
            Item.value = 60000;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Lighting.AddLight(Item.position, r: 112f, 39f, b: 41f);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ModContent.GetInstance<Bonecursed>()) += 0.20f;
            player.endurance += 0.15f;
            Lighting.AddLight(player.position, r: 0.6f, 0.3f, b: 1f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SolarTablet, 1);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 100);
            recipe.AddIngredient(ModContent.ItemType<SkullMetal>(), 1);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            recipe.Register();
        }

    }
}