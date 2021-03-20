using Disarray.Core.Autoload;
using Terraria;

namespace Disarray.Core.Properties
{
    public partial class PlayerProperty : AutoloadedClass
    {
		public virtual bool? CanHitNPC(Player player, Item item, NPC target) => null;

		public virtual bool? CanHitNPCWithProj(Player player, Projectile proj, NPC target) => null;

		public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit) { }

        public virtual void OnHitNPCWithProj(Player player, Projectile proj, NPC target, int damage, float knockback, bool crit) { }

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------

		public virtual bool CanHitPvp(Player player, Item item, Player target) => true;

		public virtual bool CanHitPvpWithProj(Player player, Projectile proj, Player target) => true;

		public virtual void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit) { }

		public virtual void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit) { }

		public virtual void OnHitPvp(Player player, Item item, Player target, int damage, bool crit) { }

		public virtual void OnHitPvpWithProj(Player player, Projectile proj, Player target, int damage, bool crit) { }
	}
}