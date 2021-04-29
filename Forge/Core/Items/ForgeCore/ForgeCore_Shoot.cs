using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Disarray.Almanac.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Disarray.Forge.Core.Items
{
	public abstract partial class ForgeCore : ModItem, IAlmanacable
	{
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			IEnumerable<Projectile> newProjectiles = ShootButBetter(player, null, item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
			return newProjectiles is null;
		}

		/// <summary>
		/// An alternative that all children of ForgeCore will use. This allows us to modify the properties of the created projectile via ModifyFiredProjectile(Projectile p).
		/// </summary>
		public virtual IEnumerable<Projectile> ShootButBetter(Player player, Item baseItem, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
			=> item.shoot > ProjectileID.None ? new Collection<Projectile>() { Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI) } : null;

		/// <summary>
		/// Allows the modification of all projectiles directly before firing
		/// </summary>
		public virtual void ModifyFiredProjectiles(Projectile projectile) { } 
	}
}