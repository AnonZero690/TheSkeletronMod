﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Content.Items.Materials;
using TheSkeletronMod.Content.Tiles;

namespace TheSkeletronMod.Content.Items.Accessories.PendantTree
{
    public class BoneLocket : ModItem
    {

        public override void SetDefaults()
        {
            Item.height = 17;
            Item.width = 18;
            Item.value = 17000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.defense = 3;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ModContent.GetInstance<Bonecursed>()) += 0.07f;
            player.GetCritChance(ModContent.GetInstance<Bonecursed>()) += 4f;
            Lighting.AddLight(player.position, r: 0.6f, 0.3f, b: 1f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkullNecklace>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AncientBone>(), 26);
            recipe.AddIngredient(ItemID.BookofSkulls, 1);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            recipe.Register();
        }

    }
}
