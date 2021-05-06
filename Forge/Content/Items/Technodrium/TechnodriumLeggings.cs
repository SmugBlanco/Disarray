using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Technodrium
{
	public class TechnodriumLeggings : TechnodriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Legs);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technodrium Leggings");
			Tooltip.SetDefault("Increases movement speed by 10%"
			+ "\n'Rocket Powered Thrusters' - Increases jump height and flight duration.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "12 template defense, 14 base defense"
				+ "\nIncreases movement speed by 15%"
				+ "\nGrants the wearer the passive ability 'Rocket Powered Thrusters', increasing jump height and increasing flight duration by 2 seconds."
				+ "\nSet Bonus: Further increases movement speed by 10%.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 26;
			item.height = 16;
			item.rare = ItemRarityID.LightRed;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.legSlot = slot;

			item.defense = ImplementedItem is null ? 12 : 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 1.15f;
			player.GetModPlayer<TechnodriumPlayer>().RocketPoweredThrusters = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TechnodriumBar>(), 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}