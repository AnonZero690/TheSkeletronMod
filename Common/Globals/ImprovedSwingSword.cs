﻿using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace TheSkeletronMod.Common.Globals
{
    /// <summary>
    /// Ported from CCmod, credit myself Xinim
    /// </summary>
    internal class ImprovedSwingSword : GlobalItem
    {
        public const float PLAYERARMLENGTH = 12f;
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            if (item.ModItem is MeleeWeaponWithImprovedSwing itemswing && !item.noMelee)
            {
                SwipeAttack(player, player.GetModPlayer<ImprovedSwingGlobalItemPlayer>(), itemswing.swingDegree, 1);
            }
        }
        private void SwipeAttack(Player player, ImprovedSwingGlobalItemPlayer modplayer, float swingdegree, int direct)
        {
            float percentDone = player.itemAnimation / (float)player.itemAnimationMax;
            float baseAngle = modplayer.data.ToRotation();
            float angle = MathHelper.ToRadians(baseAngle + swingdegree) * player.direction;
            float start = baseAngle + angle * direct;
            float end = baseAngle - angle * direct;
            float currentAngle = MathHelper.SmoothStep(start, end, percentDone);
            player.itemRotation = currentAngle;
            player.itemRotation += player.direction > 0 ? MathHelper.PiOver4 : MathHelper.PiOver4 * 3f;
            player.compositeFrontArm = new Player.CompositeArmData(true, Player.CompositeArmStretchAmount.Full, currentAngle - MathHelper.PiOver2);
            player.itemLocation = player.MountedCenter + Vector2.UnitX.RotatedBy(currentAngle) * PLAYERARMLENGTH;
        }
        //Credit hitbox code to Stardust
        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (item.ModItem is MeleeWeaponWithImprovedSwing)
            {
                Vector2 handPos = Vector2.UnitY.RotatedBy(player.compositeFrontArm.rotation);
                float length = new Vector2(item.width, item.height).Length() * player.GetAdjustedItemScale(player.HeldItem);
                Vector2 endPos = handPos;
                endPos *= length;
                handPos += player.MountedCenter;
                endPos += player.MountedCenter;
                (int X1, int X2) XVals = SkeletronUtils.Order(handPos.X, endPos.X);
                (int Y1, int Y2) YVals = SkeletronUtils.Order(handPos.Y, endPos.Y);
                hitbox = new Rectangle(XVals.X1 - 2, YVals.Y1 - 2, XVals.X2 - XVals.X1 + 2, YVals.Y2 - YVals.Y1 + 2);
            }
        }
    }
    interface MeleeWeaponWithImprovedSwing
    {
        float swingDegree { get; }
    }
    public class ImprovedSwingGlobalItemPlayer : ModPlayer
    {
        public Vector2 data = Vector2.Zero;
        public Vector2 mouseLastPosition = Vector2.Zero;
        public override void PreUpdate()
        {
            Player.attackCD = 0;
            if (Player.HeldItem.ModItem is not MeleeWeaponWithImprovedSwing || Player.HeldItem.noMelee)
            {
                return;
            }
        }
        public override void PostUpdate()
        {
            if (Player.HeldItem.ModItem is not MeleeWeaponWithImprovedSwing || Player.HeldItem.noMelee)
            {
                return;
            }
            if (Player.ItemAnimationJustStarted)
            {
                data = (Main.MouseWorld - Player.MountedCenter).SafeNormalize(Vector2.Zero);
            }
            if (Player.ItemAnimationActive)
            {
                Player.direction = data.X > 0 ? 1 : -1;
            }
            Player.attackCD = 0;
            for (int i = 0; i < Player.meleeNPCHitCooldown.Length; i++)
            {
                if (Player.meleeNPCHitCooldown[i] > 0)
                {
                    Player.meleeNPCHitCooldown[i]--;
                }
            }
        }
    }
}