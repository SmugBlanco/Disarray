using Disarray.Core.GlobalPlayers;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumChestplate : AtlastriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Body);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlastrium Chestplate");
			Tooltip.SetDefault("Reduces movement speed by 15%"
			+ "\n'Man of Stone' - Grants immense defensive capabilities at critically low health.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "7 template defense, 8 base defense"
				+ "\nReduces movement speed by 15%"
				+ "\nGrants the wearer the passive ability 'Protective Sights' when their health reaches critically low levels ( <= 10% )."
				+ "\nThe ability causes all damage from NPCs or Projectiles to be negated by 90%."
				+ "\nEach consecutive hit reduces the ability's effectiveness by 10%"
				+ "\nAfter 10 seconds of not getting hit, the ability is recharged completely."
				+ "\nCauses a further 66% reduction in movement speed when active."
				+ "\nSet Bonus: Reduces movement speed by a further 10%. Increases defense by 5.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 34;
			item.height = 24;
			item.rare = ItemRarityID.Orange;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.bodySlot = slot;

			item.defense = ImplementedItem is null ? 7 : 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 0.85f;
			player.GetModPlayer<AtlastriumPlayer>().ManOfStone = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AtlastriumBar>(), 24);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}