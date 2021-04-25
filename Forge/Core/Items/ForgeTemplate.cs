using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

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

		public string StatTooltip
		{
			get
			{
				string tooltip = string.Empty;
				bool first = true; // the alternative approach of appending '\n' to the end and removing the final two char would be more costly
				foreach (string materialType in MaterialTypeInfluence.Keys)
				{
					if (MaterialTypeInfluence.TryGetValue(materialType, out float influence))
					{
						string relationshipStatus = influence > 0.5f ? "Loves : " : "Likes : ";
						tooltip += (first ? string.Empty : "\n") + relationshipStatus + materialType + " ( retains " + (influence * 100) + "% quality influence )";
						first = false;
					}
				}
				return tooltip;
			}
		}

		public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine nameLine = tooltips.FirstOrDefault(tooltip => tooltip.mod == "Terraria" && tooltip.Name == "ItemName");

			if (nameLine != null)
			{
				tooltips.Insert(tooltips.IndexOf(nameLine) + 1, new TooltipLine(mod, "ForgeIdentityTag", "[ Forge Template ]") { overrideColor = new Color(240, 180, 90) });
			}

			tooltips.Add(new TooltipLine(mod, "ForgeStat", StatTooltip));

			ModifyTooltipsSafe(tooltips);
		}

		public virtual void ModifyTooltipsSafe(List<TooltipLine> tooltips) { }
	}
}