using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Disarray.Almanac.Core;

namespace Disarray.Core.Globals
{
	public class DisarrayGlobalItem : GlobalItem
	{
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.modItem is IAlmanacable)
			{
				tooltips.Add(new TooltipLine(mod, "ForgeInformation", "Place this item in an Almanac to access more information"));
			}
		}
	}
}