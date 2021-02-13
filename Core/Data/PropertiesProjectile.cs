using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Data
{
    public abstract class PropertiesProjectile
    {
        public Mod mod => ModLoader.GetMod("Disarray");

        public virtual void PostAI(Projectile projectile) { }

        public virtual void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void OnHitPvp(Projectile projectile, Player target, int damage, bool crit) { }

        public virtual void Kill(Projectile projectile, int timeLeft) { }
    }
}