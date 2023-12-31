﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.Utils;
using TheSkeletronMod.Content.Items.Materials;
using TheSkeletronMod.Content.Tiles;

namespace TheSkeletronMod.Content.Items.Weapons.Calcium.CalcMelee
{
    internal class BoneCrusher : ModItem
    {
        public override void SetDefaults()
        {
            Item.ItemDefaultMeleeShootProjectile(10, 10, 30, 1, 10, 10, ItemUseStyleID.Shoot, ModContent.ProjectileType<TestItemProjectile>(), 1, false);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 12000;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<TestItemProjectile>()] < 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<AncientBone>(), 35)
            .AddTile(ModContent.TileType<BoneAltar>())
            .Register();
        }

    }
    class TestItemProjectile : ModProjectile
    {
        public override string Texture => SkeletronUtils.GetEntityTexture<BoneCrusher>();
        const int TimeLeftForReal = 9999;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 5;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 74; Projectile.height = 82;
            Projectile.friendly = true;
            Projectile.timeLeft = TimeLeftForReal;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        Player player;
        float acceleration = 2;
        int MaxProgress = 360;
        int progress = TimeLeftForReal;
        int direction = 0;
        int moveAmount = 0;
        int originDmg = 0;
        bool AlreadyRelease = false;
        bool ActivateRetrieve = false;
        public override void AI()
        {
            if (progress > MaxProgress)
            {
                player = Main.player[Projectile.owner];
                progress = MaxProgress;
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
                direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.spriteDirection = direction;
                originDmg = Projectile.damage;
            }
            player.heldProj = Projectile.whoAmI;
            if (Main.mouseLeft && !AlreadyRelease)
            {
                Projectile.damage = (int)(originDmg * (acceleration / 15));
                player.direction = direction;
                if (acceleration < 15)
                {
                    acceleration += .1f;
                }
                float rotation = MathHelper.ToRadians(direction * moveAmount);
                Projectile.Center = player.Center + Projectile.velocity.RotatedBy(rotation) * 60f;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + rotation;
                if (Projectile.spriteDirection == -1)
                    Projectile.rotation += MathHelper.PiOver2;
                float rotational = Projectile.rotation - MathHelper.PiOver4 - MathHelper.PiOver2;
                if (Projectile.spriteDirection == -1)
                    rotational -= MathHelper.PiOver2;
                player.compositeFrontArm = new Player.CompositeArmData(true, Player.CompositeArmStretchAmount.Full, rotational);
            }
            else
            {
                if (!AlreadyRelease)
                {
                    Projectile.tileCollide = true;
                    if (Projectile.spriteDirection == -1)
                        Projectile.rotation += MathHelper.PiOver4 + MathHelper.Pi;
                    else
                        Projectile.rotation -= MathHelper.PiOver4;
                    Projectile.velocity = Projectile.rotation.ToRotationVector2() * acceleration;
                    Projectile.damage *= 2;
                    Projectile.knockBack = 0;
                    AlreadyRelease = true;
                }
                if (Projectile.velocity != Vector2.Zero)
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
                    if (Projectile.spriteDirection == -1)
                        Projectile.rotation += MathHelper.PiOver2;
                    Projectile.velocity.Y += .5f;
                }
                else
                {
                    if (++Projectile.ai[0] >= 10)
                    {
                        Projectile.damage -= 1;
                        Projectile.ai[0] = 0;
                    }
                }
                if (Main.mouseRight && !ActivateRetrieve)
                {
                    Projectile.tileCollide = false;
                    ActivateRetrieve = true;
                }
                if (ActivateRetrieve)
                {
                    Vector2 distance = player.Center - Projectile.Center;
                    float length = distance.Length();
                    if (length > 5)
                    {
                        length = 5;
                    }
                    Projectile.velocity -= Projectile.velocity * .08f;
                    Projectile.velocity += distance.SafeNormalize(Vector2.Zero) * length;
                    Projectile.velocity = Projectile.velocity.LimitLength(20);

                }
            }
            if ((Projectile.velocity == Vector2.Zero || ActivateRetrieve) && ((player.Center - Projectile.Center).LengthSquared() < new Vector2(Projectile.width, Projectile.height).LengthSquared() * .5f || Projectile.damage <= 1))
            {
                Projectile.Kill();
            }
            moveAmount += (int)acceleration;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.position += Projectile.velocity;
                List<NPC> npclist = Projectile.Center.GetNPCsInRange(200);
                foreach (NPC npc in npclist)
                {
                    npc.StrikeNPC(npc.CalculateHitInfo(Projectile.damage, -(Projectile.Center.X > npc.Center.X).ToDirectionInt(), false, acceleration));
                    player.dpsDamage += Projectile.damage;
                }
                for (int i = 0; i < 200; i++)
                {
                    Vector2 pos = SkeletronUtils.SpawnRanPositionThatIsNotIntoTile(Projectile.Center, 200, 200);
                    Dust.NewDust(pos, 0, 0, DustID.Dirt);
                }
            }
            Projectile.velocity = Vector2.Zero;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.ProjectileDefaultDrawInfo(out Texture2D texture, out Vector2 origin);
            //The utils code do not support custom sprite direction so yea, or rather I just outright don't do it
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + origin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], origin, Projectile.scale, direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            return base.PreDraw(ref lightColor);
        }
    }
}