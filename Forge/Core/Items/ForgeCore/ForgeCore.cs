using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Disarray.Almanac.Core;

namespace Disarray.Forge.Core.Items
{
	public abstract partial class ForgeCore : ModItem, IAlmanacable
	{
		public virtual bool PreInsert(ForgeTemplate template) => true;

		public ForgeItem ImplementedItem { get; internal set; } = null;

		public virtual bool PreDrawAnimation(ref Texture2D texture, ref Vector2 drawPosition, ref Rectangle sourceRectangle, ref Color drawColor, ref float rotation, ref Vector2 drawOrigin, ref float scale, ref SpriteEffects spriteEffects) => true;

		public virtual void NonProductDefaults() { }
	}
}