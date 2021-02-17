using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Data
{
    public abstract class PropertiesPlayer
    {
        public Mod mod => ModLoader.GetMod("Disarray");

        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit) { }

        public virtual void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit) { }

        public virtual void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit) { }

        public virtual void PostUpdateMiscEffects(Player player) { }
    }
}