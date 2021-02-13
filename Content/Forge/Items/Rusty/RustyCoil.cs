using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Data;
using Disarray.Core.Forge;
using Disarray.Core.Forge.Items;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Rusty
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

        public override void HoldItem(Player player)
        {
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DamageIncrementChance);
			if (property is DamageIncrementChance damageIncrementChanceProperty)
			{
				damageIncrementChanceProperty.Chance += 0.2f;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DamageIncrementChance(0.2f));
			}
		}

        public override void UpdateEquip(Player player)
        {
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DamageIncrementChance);
			if (property is DamageIncrementChance damageIncrementChanceProperty)
			{
				damageIncrementChanceProperty.Chance += 0.2f;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DamageIncrementChance(0.2f));
			}
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DamageIncrementChance);
			if (property is DamageIncrementChance damageIncrementChanceProperty)
			{
				damageIncrementChanceProperty.Chance += 0.2f;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DamageIncrementChance(0.2f));
			}
		}

        public override string ItemDescription() => "Seems like it use to be a multi-purpose chain. It's current condition is... poor but it may have some uses in 'The Forge'.";

		public override string ItemStatistics() => "20% chance to increase damage output by 1." + "\nIf the odds stack above 100%, the damage output increase is guaranteed and the remaining odds will go towards guaranteed + 1." + "\nEffect stacks indefinitely.";

		public override string ObtainingDetails() => "Found left behind in Wooden Chests and carried on the Undead.";

		public override string MiscDetails() => " ";
	}
}