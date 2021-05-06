using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Technodrium
{
	public class TechnodriumChestplate : TechnodriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Body);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technodrium Chestplate");
			Tooltip.SetDefault("Increases damage reduction by 3%"
			+ "\n'Rocket Defense System' - Automatically calls in a missile strike, dealing devastating aoe damage.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "16 template defense, 18 base defense"
				+ "\nncreases damage reduction by 3%"
				+ "\nGrants the wearer access to a rocket defense system, automatically calling in a cruise missile strike targetting the strongest adversary in the immediate area."
				+ "\nThe missle does 1000 base damage at ground zero, with a maximum effect radius of 25 blocks."
				+ "\nRequires a 1 minute cooldown between rocket launches."
				+ "\nSet Bonus: Further increases movement speed by 10%.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.LightRed;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.bodySlot = slot;

			item.defense = ImplementedItem is null ? 16 : 18;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance += 0.03f;
			player.GetModPlayer<TechnodriumPlayer>().MissileDefenseSystem = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TechnodriumBar>(), 24);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}