using Disarray.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Fire
{
	public class Fuel : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dried Foliage");
			Tooltip.SetDefault("Increases duration of flame based debuffs when inflicted.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Each component increases duration of flame based debuffs when inflicted by 1 second.";

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
			item.value = 100;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().GeneralDuration += 60;
	}
}