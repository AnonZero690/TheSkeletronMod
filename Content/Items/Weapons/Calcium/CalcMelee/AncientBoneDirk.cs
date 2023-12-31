﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Content.Items.Materials;
using TheSkeletronMod.Content.Projectiles.Calcprojs.CalcMeleeproj;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcMelee
{
    public class AncientBoneDirk : ModItem
    {
        public override void SetStaticDefaults()
        {



        }
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.knockBack = 4f;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.width = 48;
            Item.height = 48;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 60);

            Item.shoot = ModContent.ProjectileType<AncientBoneDirkProjectile>();
            Item.shootSpeed = 2.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AncientBone>(), 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

    }

}
