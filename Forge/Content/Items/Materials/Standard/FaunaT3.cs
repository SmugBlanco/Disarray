using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria.ID;
using Terraria.Localization;

namespace Disarray.Forge.Content.Items.Materials.Standard
{
	public class FaunaT3 : ForgeMaterial
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mountain Oysters");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 28;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override string GeneralDescription => "";

		public override string ItemStatistics => StatTooltip;

		public override string ObtainingGuide => "The rare spoils of a successful hunt.";

		public override string Miscellaneous => string.Empty;

		public override IEnumerable<(string identity, float qualityInfluence)> MaterialIdentity { get; } = new Collection<(string, float)>() { { ("Fauna", 0.15f) } };
	}
}