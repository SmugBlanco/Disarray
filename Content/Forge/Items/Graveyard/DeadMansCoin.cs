using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Graveyard
{
	public class DeadMansCoin : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dead Man's Coin");
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
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DeadMansSpark);
			if (property is DeadMansSpark sparkProperty)
			{
				sparkProperty.SparkCount++;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DeadMansSpark(1));
			}
		}

		public override void UpdateEquip(Player player)
		{
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DeadMansSpark);
			if (property is DeadMansSpark sparkProperty)
			{
				sparkProperty.SparkCount++;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DeadMansSpark(1));
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is DeadMansSpark);
			if (property is DeadMansSpark sparkProperty)
			{
				sparkProperty.SparkCount++;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new DeadMansSpark(1));
			}
		}

		public override string ItemDescription() => "Utilised in 'The Forge'.";

		public override string ItemStatistics() => "Allows your attacks to erupt burst(s) of sparks upon killing enemies.";

		public override string ObtainingDetails() => "Drops";

		public override string MiscDetails() => "In antiquity, the dead would be burried with a coin, known as 'Charon's Opol', in their mouth. The coin is meant to be payment towards a safe crossing across the river Styx from the Ferrymen of Hades.";
	}
}