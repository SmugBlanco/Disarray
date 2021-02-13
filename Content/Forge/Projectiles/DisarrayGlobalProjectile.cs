using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Disarray.Core.Data;

namespace Disarray.Core.Globals
{
    //OOP this later
    public class DisarrayGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => false;

        public ICollection<PropertiesProjectile> ActiveProperties = new Collection<PropertiesProjectile>();

        public override void PostAI(Projectile projectile)
        {
            foreach (PropertiesProjectile properties in ActiveProperties)
            {
                properties.PostAI(projectile);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            foreach (PropertiesProjectile properties in ActiveProperties)
            {
                properties.OnHitNPC(projectile, target, damage, knockback, crit);
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            foreach (PropertiesProjectile properties in ActiveProperties)
            {
                properties.OnHitPlayer(projectile, target, damage, crit);
            }
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            foreach (PropertiesProjectile properties in ActiveProperties)
            {
                properties.OnHitPvp(projectile, target, damage, crit);
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            foreach (PropertiesProjectile properties in ActiveProperties)
            {
                properties.Kill(projectile, timeLeft);
            }
        }
    }
}