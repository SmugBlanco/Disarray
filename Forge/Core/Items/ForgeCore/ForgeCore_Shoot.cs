using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Disarray.Almanac.Core;

namespace Disarray.Forge.Core.Items
{
	public abstract partial class ForgeCore : ModItem, IAlmanacable
	{
		/// <summary>
		/// An alternative that all children of ForgeCore will use. This allows us to modify the properties of the created projectile via ModifyFiredProjectile(Projectile p).
		/// </summary>
		public virtual Projectile ShootButBetter(Player player, Item baseItem, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
			=> item.shoot > ProjectileID.None ? Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI) : null;

		/// <summary>
		/// Allows the modification of all projectiles directly before firing
		/// </summary>
		public virtual void ModifyFiredProjectiles(Projectile projectile) { } 
	}
}