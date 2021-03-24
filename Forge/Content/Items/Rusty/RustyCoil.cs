using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Rusty
{
	public class RustyCoil : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Coil");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

		public override void ApplyToAllScenarios(Player player)
		{
			BasicStats stat = AutoloadedClass.CreateNewInstance<BasicStats>();
			stat.DamageIncreaseChance += 0.2f;
			PlayerProperty.ImplementProperty(player, stat, false);
		}

		public override string GeneralDescription => "Seems like it use to be a multi-purpose chain. It's current condition is... poor but it may have some uses in 'The Forge'.";

		public override string ItemStatistics => "20% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingGuide => "Found left behind in Wooden Chests and carried on the Undead.";
	}
}