﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Common.Globals;
using TheSkeletronMod.Content.Items.Placeables.Bars;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcMelee
{
    public class BoneSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 8));
        }
        public override void SetDefaults()
        {
            Item.width = 114;
            Item.height = 114;
            Item.damage = 17;
            Item.knockBack = 0;
            Item.useTime = 100;
            Item.useAnimation = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = false;
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            if (Item.TryGetGlobalItem(out ImprovedSwingSword meleeItem))
            {
                meleeItem.ArrayOfAttack =
                    new CustomAttack[]
                    {
                        new SwipeAttack(){ SwingDownWard = false },
                        new SwipeAttack(){ SwingDownWard = false }
                    };
                meleeItem.ItemSwingDegree = 150;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<CalciumBar>(), 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}