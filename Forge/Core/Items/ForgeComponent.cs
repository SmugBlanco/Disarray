using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Disarray.Forge.Core.Items
{
	public abstract class ForgeComponent : ForgeAppendages
	{
		public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine nameLine = tooltips.FirstOrDefault(tooltip => tooltip.mod == "Terraria" && tooltip.Name == "ItemName");

			if (nameLine != null)
			{
				tooltips.Insert(tooltips.IndexOf(nameLine) + 1, new TooltipLine(mod, "ForgeIdentityTag", "[ Forge Component ]") { overrideColor = new Color(240, 180, 90) });
			}

			ModifyTooltipsSafe(tooltips);
		}

		public virtual void ModifyTooltipsSafe(List<TooltipLine> tooltips) { }
	}
}