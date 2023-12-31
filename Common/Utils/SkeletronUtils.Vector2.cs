﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Utilities;

namespace TheSkeletronMod.Common.Utils
{
    public static partial class SkeletronUtils
    {
        public static Vector2 LimitLength(this Vector2 vec, float length)
        {
            if (vec.Length() > length)
            {
                vec.Normalize();
                vec *= length;
            }
            return vec;
        }
        public static Vector2 LimitPosition(this Vector2 position, Vector2 center, float distance)
        {
            position.X = Math.Clamp(position.X, -distance + center.X, distance + center.X);
            position.Y = Math.Clamp(position.Y, -distance + center.Y, distance + center.Y);
            return position;
        }
        public static Vector2 NextVector2RectangleEdge(this UnifiedRandom r, float RectangleWidthHalf, float RectangleHeightHalf)
        {
            float X = r.NextFloat(-RectangleWidthHalf, RectangleWidthHalf);
            float Y = r.NextFloat(-RectangleHeightHalf, RectangleHeightHalf);
            bool Randomdecider = r.NextBool();
            Vector2 RandomPointOnEdge = new Vector2(X * Randomdecider.ToInt(), Y * (!Randomdecider).ToInt());
            if (RandomPointOnEdge.X == 0)
            {
                RandomPointOnEdge.X = RectangleWidthHalf;
            }
            else
            {
                RandomPointOnEdge.Y = RectangleHeightHalf;
            }
            return RandomPointOnEdge * r.NextBool().ToDirectionInt();
        }
        // This can be done by just using Rectangle.Contains, so just use that instead
        public static bool Vector2WithinRectangle(this Vector2 position, float X, float Y, Vector2 Center)
        {
            Vector2 positionNeedCheck1 = new Vector2(Center.X + X, Center.Y + Y);
            Vector2 positionNeedCheck2 = new Vector2(Center.X - X, Center.Y - Y);
            if (position.X < positionNeedCheck1.X && position.X > positionNeedCheck2.X && position.Y < positionNeedCheck1.Y && position.Y > positionNeedCheck2.Y)
            { return true; }//higher = -Y, lower = Y
            return false;
        }
        public static Vector2 Vector2Evenly(this Vector2 vec, float ProjectileAmount, float rotation, int i)
        {
            if (ProjectileAmount > 1)
            {
                rotation = MathHelper.ToRadians(rotation);
                return vec.RotatedBy(MathHelper.Lerp(rotation * .5f, rotation * -.5f, i / ProjectileAmount));
            }
            return vec;
        }
        public static Vector2 NextVector2RotatedByRandom(Vector2 velocity, float ToRadians) => velocity.RotatedByRandom(MathHelper.ToRadians(ToRadians));
        public static Vector2 NextVector2Spread(this Vector2 ToRotateAgain, float Spread, float additionalMultiplier = 1)
        {
            ToRotateAgain.X += Main.rand.NextFloat(-Spread, Spread) * additionalMultiplier;
            ToRotateAgain.Y += Main.rand.NextFloat(-Spread, Spread) * additionalMultiplier;
            return ToRotateAgain;
        }
        /// <summary>
        /// Only use this if you know the projectile can get spawn into a tile<br/>
        /// </summary>
        /// <param name="positionCurrent"></param>
        /// <param name="positionTo"></param>
        /// <returns></returns>
        public static Vector2 SpawnRanPositionThatIsNotIntoTile(Vector2 positionCurrent, float halfwidth, float halfheight, float rotation = 0)
        {
            int counter = 0;
            Vector2 pos;
            do
            {
                counter++;
                pos = positionCurrent + Main.rand.NextVector2Circular(halfwidth, halfheight).RotatedBy(rotation);
            } while (!Collision.CanHitLine(positionCurrent, 0, 0, pos, 0, 0) || counter < 50);
            return pos;
        }
        public static bool InRange(this Vector2 CurrentPosition, Vector2 Position, float distance) => (Position - CurrentPosition).Length() <= distance;
        
        // everything below hasnt been changed by me
        // - Nurby
        
        /// <summary>
        /// This will take a approximation of the rough position that it need to go and then stop the npc from moving when it reach that position 
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="Position"></param>
        /// <param name="speed"></param>
        public static bool ProjectileMoveToPosition(this Projectile projectile, Vector2 Position, float speed)
        {
            Vector2 distance = Position - projectile.Center;
            if (distance.Length() <= 20f)
            {
                projectile.velocity = Vector2.Zero;
                return true;
            }
            projectile.velocity = distance.SafeNormalize(Vector2.Zero) * speed;
            return false;
        }
        public static Vector2 Vector2SmallestInList(List<Vector2> flag)
        {
            for (int i = 0; i < flag.Count;)
            {
                Vector2 vector2 = flag[i];
                for (int l = i + 1; l < flag.Count; ++l)
                {
                    if (vector2.LengthSquared() > flag[l].LengthSquared())
                    {
                        vector2 = flag[l];
                    }
                }
                return vector2;
            }
            return Vector2.Zero;
        }
        public static Vector2 Vector2RotateByRandom(this Vector2 Vec2ToRotate,float ToRadians) => Vec2ToRotate.RotatedByRandom(MathHelper.ToRadians(ToRadians));
        
        public static Vector2 PositionOFFSET(this Vector2 position, Vector2 ProjectileVelocity, float offSetBy)
        {
            Vector2 OFFSET = ProjectileVelocity.SafeNormalize(Vector2.Zero) * offSetBy;
            if (Collision.CanHitLine(position, 0, 0, position + OFFSET, 0, 0))
            {
                return position += OFFSET;
            }
            return position;
        }
        public static Vector2 IgnoreTilePositionOFFSET(this Vector2 position, Vector2 ProjectileVelocity, float offSetBy)
        {
            Vector2 OFFSET = ProjectileVelocity.SafeNormalize(Vector2.Zero) * offSetBy;
            return position += OFFSET;
        }
        public static Vector2 Vector2RandomSpread(this Vector2 ToRotateAgain, float Spread, float additionalMultiplier = 1)
        {
            ToRotateAgain.X += Main.rand.NextFloat(-Spread, Spread) * additionalMultiplier;
            ToRotateAgain.Y += Main.rand.NextFloat(-Spread, Spread) * additionalMultiplier;
            return ToRotateAgain;
        }
        //circular interpolation
        public static Vector2 Slerp(Vector2 start, Vector2 end, float t, Vector2 center, float radius = default)
        {
            return radius == default ? (center).DirectionTo(start.RotatedBy(t, center)) : (center).DirectionTo(start.RotatedBy(t, center)) * radius;
            //alt implementations
            //return Utils.AngleLerp(start.ToRotation(), end.ToRotation(), t).ToRotationVector2(); 
            // return new((float)Math.Cos(t), (float)Math.Sin(t)); //* radius; 
        }
        public static Vector2 Bezier(Vector2[] controlPoints, float t)
        {
            //my own implementation of a bezier that takes any number of points
            Vector2[] temp = controlPoints;
            while (temp.Length > 1)
            {
                temp = BezierCalculation(temp, t);
            }
            return temp[0];
            
        }
        private static Vector2[] BezierCalculation(Vector2[] c, float t)
        {
            Vector2[] temp = new Vector2[c.Length - 1];
            for (int i = 0; i < c.Length - 1; i++)
            {
                temp[i] = Vector2.Lerp(c[i], c[i + 1], t);
            }
            return temp;
        }
    }
}
