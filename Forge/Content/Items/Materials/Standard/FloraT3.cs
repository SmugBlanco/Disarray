using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.Localization;

namespace Disarray.Forge.Content.Items.Materials.Standard
{
	public class FloraT3 : ForgeMaterial
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Clover");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 50;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override string GeneralDescription => "";

		public override string ItemStatistics => StatTooltip;

		public override string ObtainingGuide => "Among the grass.";

		public override string Miscellaneous => string.Empty;

		public override IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; } = new Collection<(string, float)>() { { ("Flora", 0.15f) } };
	}
}