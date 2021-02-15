using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Sharks
{
	public class SandsharksMaw : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandshark's Maw");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 46;
			item.rare = ItemRarityID.LightRed;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			player.armorPenetration += 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.armorPenetration += 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.armorPenetration += 2;
		}

		public override string ItemDescription() => "An entire shark's maw, lined to the brim with sharpened daggers built to shred and pierce; could be utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Armor Penetration by 2";

		public override string ObtainingDetails() => "It's rare that a Sandshark's Maw drops in such a pristine condition.";

		public override string MiscDetails() => " ";
	}
}