using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace Disarray.Forge.Core.Items
{
	public abstract class ForgeTemplate : ForgeAppendages
	{
		public abstract IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; }

		public virtual void SafeDefaults(Item item, float quality) { }

		public sealed override void SetDefaults()
		{
			SafeDefaults(item, 0);
			NonProductDefaults();
		}

		public virtual bool PreDrawForgeItem(SpriteBatch spriteBatch, Vector2 originalPosition, Color drawColor) => true;
	}
}