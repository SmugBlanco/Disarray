using Disarray.Forge.Content.Items.Materials.Standard;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Huntsman
{
	public class HuntsmanWard : HuntsmanItem
	{
		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Fauna", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huntsman's Ward");
			Tooltip.SetDefault("When equipped, heavily reduces enemy aggressivity.");
		}

		public override string GeneralDescription => "Armament made from sturdy yet light bones, bound together by flesh.Usable by default, but excels when utilised in 'The Forge'" + "\n'Pee is stored in the balls.'";

		public override string ItemStatistics
		{
			get
			{
				string statistic = "When equipped, reduces enemy aggressivity by 1000."
				+ "\nThis effect is similar in nature to that of the invisibility potion.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override string Miscellaneous => "Urine of large predators are often used to repel pests that have developed an intrinsic fear against said predator.";

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 52;
			item.height = 30;
			item.rare = ItemRarityID.Green;

			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) => player.aggro -= 1000;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ModContent.ItemType<FaunaT2>());
			recipe.needWater = true;
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}