﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.DamageClasses;
using TheSkeletronMod.Common.Globals;
using TheSkeletronMod.Common.Utils;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcMelee
{
    internal class WhittledBone : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeShootProjectile(54, 66, 50, 7f, 25, 25, ItemUseStyleID.Swing, ModContent.ProjectileType<WhittledBoneP>(), 10, true);
            Item.DamageType = ModContent.GetInstance<Bonecursed>();
            Item.damage = 10;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 10, 10);
            Item.width = 46;
            Item.height = 78;
            Item.autoReuse = false;
            if (Item.TryGetGlobalItem(out ImprovedSwingSword meleeItem))
            {
                meleeItem.ArrayOfAttack =
                    new CustomAttack[]
                    {
                        new PokeAttack{ SwingDownWard = true },
                        new PokeAttack{ SwingDownWard = false }
                    };
                meleeItem.ItemSwingDegree = 150;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bone, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class WhittledBoneP : ModProjectile
    {
        public override string Texture => SkeletronUtils.GetEntityTexture<WhittledBone>();
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = Projectile.height = 10;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.hide = true;
            Projectile.timeLeft = 100;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f) * Main.rand.NextFloat();
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Bone, speed, Scale: Main.rand.NextFloat(.5f, 1.5f));
                d.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.damage > 1)
                Projectile.damage--;
        }
    }
}