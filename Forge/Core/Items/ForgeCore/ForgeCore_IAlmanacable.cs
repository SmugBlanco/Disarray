using Terraria.ModLoader;
using Disarray.Almanac.Core;

namespace Disarray.Forge.Core.Items
{
	public abstract partial class ForgeCore : ModItem, IAlmanacable
	{
		public virtual string GeneralDescription { get; set; } = string.Empty;

		public virtual string ItemStatistics { get; set; } = string.Empty;

		public virtual string ObtainingGuide { get; set; } = string.Empty;

		public virtual string Miscellaneous { get; set; } = string.Empty;
	}
}