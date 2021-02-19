using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Marble
{
	public class MarbleRelic : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Energy Pillar");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 36;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			MarbleEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override void UpdateEquip(Player player)
		{
			MarbleEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			MarbleEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override string ItemDescription() => "These relics seem to have some sort of power able to be utilised in 'The Forge'.";

		public override string ItemStatistics()
		{
			string EnergyEffect = "Allows attacks access to a 25% chance to release an orb of marble energy." + "\nEach material increases said chance by 5%." + "\nMaximum amount of energy able to be present at any given moment scales with the number of this materials used.";
			string PassiveEffects = "The following bonuses are granted if you're standing on a marble block:";
			string Effect = "Outgoing Damage bonus: 1%" + "\nDamage Reduction bonus: 1%" + "\nMana Regeneration: 1";
			string Notice = "Effects stackable indefinitely.";
			return EnergyEffect + "\n" + PassiveEffects + "\n" + Effect + "\n" + Notice;
		}

		public override string ObtainingDetails() => "Crafted from blocks of marble on a Demon Altar; also can be found carried on occasion by various marble enemies.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Marble, 50);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}