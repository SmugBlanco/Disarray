using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.Localization;

namespace Disarray.Forge.Content.Items.Materials.Standard
{
	public class FloraT2 : ForgeMaterial
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flower");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 54;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 250;
		}

		public override string GeneralDescription => "";

		public override string ItemStatistics => StatTooltip;

		public override string ObtainingGuide => "Flowering in forest fields.";

		public override string Miscellaneous => string.Empty;

		public override IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; } = new Collection<(string, float)>() { { ("Flora", 0.1f) } };
	}
}