﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Content.Projectiles.Calcprojs.CalcMeleeproj;
using TheSkeletronMod.Content.Tiles;

namespace TheSkeletronMod.Content.Items.Ammo
{
    internal class BoneFragments : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;

            Item.damage = 4;
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            Item.knockBack = 4f;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.ammo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<BoneFragmentsProjectile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 1);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            recipe.ReplaceResult(this, 5);
            recipe.Register();
        }
    }
}
