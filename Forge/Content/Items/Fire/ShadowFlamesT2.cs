using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Fire
{
	public class ShadowFlamesT2 : ForgeComponent
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadowflame Inferno");
			Tooltip.SetDefault("Allows attacks the ability to inflict 'Shadowflame' onto target."
			+ "\nIncreases chance of inflicting said debuff.");
		}

		public override string GeneralDescription => "Ripe for a catharsis of embers.";

		public override string ItemStatistics => "Allows attacks the ability to inflict 'Shadowflame' onto target." + "\nThis occurs with a default chance of 5%, and a default duration of 3 seconds." + "\nEach component increases inflict chance by 5%.";

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Green;
			item.maxStack = 999;
			item.value = 5000;
		}

		public override void ApplyToAllScenarios(Player player) => player.GetModPlayer<FirePlayer>().ShadowFlameChance += 0.05f;
	}
}