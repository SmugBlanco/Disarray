using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.Localization;

namespace Disarray.Forge.Content.Items.Materials.Standard
{
	public class MetalT1 : ForgeMaterial
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Metal Ore");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 100;
		}

		public override string GeneralDescription => "";

		public override string ItemStatistics => StatTooltip;

		public override string ObtainingGuide => "Chance to be excavated from various stones on destruction.";

		public override string Miscellaneous => string.Empty;

		public override IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; } = new Collection<(string, float)>() { { ("Metal", 0.05f) } };
	}
}