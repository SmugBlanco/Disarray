using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Disarray.Forge.Core.Items
{
	public abstract class ForgeMaterial : ForgeCore
	{
		public abstract IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; }

		public string StatTooltip
		{
			get
			{
				string tooltip = string.Empty;
				bool first = true; // the alternative approach of appending '\n' to the end and removing the final two char would be more costly
				foreach ((string identity, float qualityInfluence) materialType in MaterialIdentity)
				{
					tooltip += (first ? string.Empty : "\n") + materialType.identity + " Quality : " + (materialType.qualityInfluence * 100) + "%";
					first = false;
				}
				return tooltip;
			}
		}

		public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine nameLine = tooltips.FirstOrDefault(tooltip => tooltip.mod == "Terraria" && tooltip.Name == "ItemName");

			if (nameLine != null)
			{
				tooltips.Insert(tooltips.IndexOf(nameLine) + 1, new TooltipLine(mod, "ForgeIdentityTag", "[ Forge Material ]") { overrideColor = new Color(240, 180, 90) });
			}

			tooltips.Add(new TooltipLine(mod, "ForgeStat", StatTooltip));

			ModifyTooltipsSafe(tooltips);
		}

		public virtual void ModifyTooltipsSafe(List<TooltipLine> tooltips) { }
	}
}