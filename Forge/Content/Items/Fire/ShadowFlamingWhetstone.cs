using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Fire
{
	public class ShadowFlamingWhetstone : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Flaming Whetstone");
			Tooltip.SetDefault("Increases outgoing damage on enemies currently inflicted with 'Shadowflame'.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Increases outgoing damage on enemies currently inflicted with 'Shadowflame' by 3%.";

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.rare = ItemRarityID.Green;
			item.maxStack = 999;
			item.value = 5000;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().ShadowFlameDamage += 0.03f;
	}
}