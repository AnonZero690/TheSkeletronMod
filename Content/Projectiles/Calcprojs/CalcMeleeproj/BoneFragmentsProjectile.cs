using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSkeletronMod.Common.Utils;
using TheSkeletronMod.Content.Items.Ammo;

namespace TheSkeletronMod.Content.Projectiles.Calcprojs.CalcMeleeproj
{
    internal class BoneFragmentsProjectile : ModProjectile
    {
        public override string Texture => SkeletronUtils.GetTheSameTextureAs<BoneFragments>("BoneFragmentsProjectile");
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 4;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[1] > 0)
            {
                if (Projectile.penetrate < 3)
                {
                    Projectile.maxPenetrate = 3;
                    Projectile.penetrate = 3;
                }
                Projectile.ai[1] = 0;
                Projectile.ai[0] = -5;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 10)
                Projectile.velocity.Y += .25f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawTrail(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}