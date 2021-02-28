using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Gardening.Jungle.HoneySickle
{
	public class HoneySickleFruit : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honey Sickle Fruit");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player) => PlayerProperty.ImplementProperty(player, new HoneySickleHoneyBoost() { Count = 1, AdditionalChance = 0.02f }, false);

		public override void UpdateEquip(Player player) => PlayerProperty.ImplementProperty(player, new HoneySickleHoneyBoost() { Count = 1, AdditionalChance = 0.02f }, false);

		public override void UpdateAccessory(Player player, bool hideVisual) => PlayerProperty.ImplementProperty(player, new HoneySickleHoneyBoost() { Count = 1, AdditionalChance = 0.02f }, false);

		public override string ItemDescription() => "The fruits of a Honey Sickle plant, perhaps it may have some healing properties you can utilise in 'The Forge'";

		public override string ItemStatistics()
		{
			string DefaultAbility = "Allows attacks to a default 20% chance to apply 'Honey' onto yourself for 5 seconds." + "\nEach material increases said chance by 2%.";
			string HoneyBoost = "Every third material increases life regeneration while 'Honey'ed by 1.";
			return DefaultAbility + "\n" + HoneyBoost;
		}

		public override string ObtainingDetails() => "Honey Sickle Fruit can be obtained from harvesting Honey Sickle plants.";

		public override string MiscDetails() => "";
	}
}