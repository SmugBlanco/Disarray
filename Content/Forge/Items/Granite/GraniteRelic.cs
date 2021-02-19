using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Granite
{
	public class GraniteRelic : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Energy Obelisk");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 38;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			GraniteEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override void UpdateEquip(Player player)
		{
			GraniteEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			GraniteEnergyRelease.ImplementThis(player, 1, 0.05f);
		}

		public override string ItemDescription() => "These relics seem to have some sort of power able to be utilised in 'The Forge'.";

		public override string ItemStatistics()
		{
			string EnergyEffect = "Allows attacks access to a 25% chance to release an orb of granite energy." + "\nEach material increases said chance by 5%." + "\nMaximum amount of energy able to be present at any given moment scales with the number of this materials used.";
			string PassiveEffects = "The following bonuses are granted if you're standing on a granite block:";
			string Effect = "Outgoing Damage bonus: 1%" + "\nDefense bonus: 2" + "\nLife Regeneration: 1";
			string Notice = "Effects stackable indefinitely.";
			return EnergyEffect + "\n" + PassiveEffects + "\n" + Effect + "\n" + Notice;
		}

		public override string ObtainingDetails() => "Crafted from blocks of granite on a Demon Altar; also can be found carried on occasion by various granite enemies.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GraniteBlock, 50);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}