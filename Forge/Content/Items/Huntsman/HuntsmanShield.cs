using Disarray.Forge.Content.Items.Materials.Standard;
using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanShield : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Shield");
			Tooltip.SetDefault("When directly striked, the attacker will become more succeptable to damage.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "3 base defense when equipped"
				+ "\nWhen equipped, upon getting attacked via direct strikes 'Battle Marked' will be inflicted onto the attacker for 10 seconds."
				+ "\n'Battle Marked' highlights an enemy with a clear red tint, and increasing all incoming damage onto said enemy by 10%"
				+ "\nEvery 10 quality percent, increases 'Battle Marked's duration by 1 second.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 52;
			item.height = 30;
			item.rare = ItemRarityID.Green;

			item.accessory = true;

			item.defense = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.aggro -= 1000;
			HemorrhagePlayer hemorrhagePlayer = player.GetModPlayer<HemorrhagePlayer>();
			hemorrhagePlayer.BattleMark = true;
			hemorrhagePlayer.BattleMarkDuration = 600;

			if (ImplementedItem != null)
			{
				hemorrhagePlayer.MaxHearBeatSensorRadius += 60 * (int)(ImplementedItem.Quality * 10);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 100);
			recipe.AddIngredient(ModContent.ItemType<FaunaT2>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}