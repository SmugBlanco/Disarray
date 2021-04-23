using Disarray.Core.Autoload;
using Disarray.Core.Properties;
//using Disarray.Forge.Content.PlayerProperties;
using Disarray.Forge.Core.Items;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern
{
	public class SwordFernsBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Fern's Blade");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
		}

		/*public override void ApplyToAllScenarios(Player player)
		{
			BasicStats stat = AutoloadedClass.CreateNewInstance<BasicStats>();
			stat.DamageIncreaseChance += 0.25f;
			PlayerProperty.ImplementProperty(player, stat, false);
		}

		public override string GeneralDescription => "The blades from a matured Sword-Fern plant, it may have some uses in 'The Forge'.";

		public override string ItemStatistics => "25% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingGuide => "Obtained from Sword Ferns at harvest.";

		public override string Miscellaneous => " ";*/
	}
}