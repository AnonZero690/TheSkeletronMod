using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Content.Items.Materials;
using TheSkeletronMod.Content.Tiles;

namespace TheSkeletronMod.Content.Items.Armor.AncientBoneArmour
{
    [AutoloadEquip(EquipType.Legs)]
    public class AncientBoneLeggings : ModItem
    {
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(ModContent.GetInstance<Bonecursed>()) += 0.04f;
            player.moveSpeed += 0.1f;
        }
        public override void SetDefaults()
        {
            Item.value = 6000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AncientBone>(), 15);
            recipe.AddTile(ModContent.TileType<BoneAltar>());
            //recipe.AddCondition(conditions: Condition.InGraveyard);
            recipe.Register();
        }

    }
}