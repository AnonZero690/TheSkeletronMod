using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TheSkeletronMod.projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Tiles;
using TheSkeletronMod.Items.Materials;

namespace TheSkeletronMod.Items.Armor.AncientBoneArmour
{
    [AutoloadEquip(EquipType.Body)]
    public class AncientBoneChestplate : ModItem
    {
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(ModContent.GetInstance<Bonecursed>()) += 0.06f;
        }
        public override void SetDefaults()
        {
            Item.value = 6000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AncientBone>(), 20);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            //recipe.AddCondition(conditions: Condition.InGraveyard);
            recipe.Register();
        }

    }
}