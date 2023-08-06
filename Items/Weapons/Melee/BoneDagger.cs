using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TheSkeletronMod.Items.Weapons.Ranged.Projectiles;

namespace TheSkeletronMod.Items.Weapons.Melee
{
	public class BoneDagger : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Melee;
			Item.useTime = 10;
			Item.width = 50;
			Item.height = 50;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 60;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = false;
			Item.crit = 15;
		}
		public override bool AltFunctionUse(Player player)
		{
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                int proj = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BoneDaggerProjectile>(), 12, 1, player.whoAmI);
				Main.projectile[proj].friendly = true;
            }
            return true;
            
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock,27);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
		
	}
}
