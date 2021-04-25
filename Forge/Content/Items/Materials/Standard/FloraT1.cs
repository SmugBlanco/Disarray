using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.Localization;

namespace Disarray.Forge.Content.Items.Materials.Standard
{
	public class FloraT1 : ForgeMaterial
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 46;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 100;
		}

		public override string GeneralDescription => "";

		public override string ItemStatistics => StatTooltip;

		public override string ObtainingGuide => "Plucked from Trees.";

		public override string Miscellaneous => string.Empty;

		public override IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; } = new Collection<(string, float)>() { { ("Flora", 0.05f) } };
	}
}