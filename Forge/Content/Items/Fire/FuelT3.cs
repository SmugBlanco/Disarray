using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Fire
{
	public class FuelT3 : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Can of Grease");
			Tooltip.SetDefault("Increases duration of flame based debuffs when inflicted.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Each component increases duration of flame based debuffs when inflicted by 3 second.";

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 22;
			item.rare = ItemRarityID.Green;
			item.maxStack = 999;
			item.value = 1000;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().GeneralDuration += 180;
	}
}