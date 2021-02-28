using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Disarray.Core.Properties;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Forge.Items.Snow
{
	public class SnowFlinxsFur : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Flinx's Fur");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player) => PlayerProperty.ImplementProperty(player, new KnockbackIncrement() { knockBack = 0.5f }, false);

		public override void UpdateEquip(Player player) => PlayerProperty.ImplementProperty(player, new KnockbackIncrement() { knockBack = 0.5f }, false);

		public override void UpdateAccessory(Player player, bool hideVisual) => PlayerProperty.ImplementProperty(player, new KnockbackIncrement() { knockBack = 0.5f }, false);

		public override string ItemDescription() => "Counter to intuition, the creature who is most affected by knockback would help in dealing more. Utilised in 'The Forge'";

		public override string ItemStatistics() => "Increases Knockback by 0.5";

		public override string ObtainingDetails() => "A rare chance to dropped intact from Snow Flinxes.";

		public override string MiscDetails() => " ";
	}
}