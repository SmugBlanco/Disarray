using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumLeggings : AtlastriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Legs);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atlastrium Leggings");
			Tooltip.SetDefault("Reduces movement speed by 15%, while granting knock back immunity."
			+ "\n'Steadfast' - While standing still, increases invincibility time after getting hit."
			+ "\nThis increase is seperate from other sources of lengthening invincibility.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "6 template defense, 7 base defense"
				+ "\nReduces movement speed by 15%, as well as granting knock back immunity "
				+ "\nGrants the wearer the passive ability 'Steadfast'"
				+ "\n'Steadfast' increases invincibility time after getting hit."
				+ "\nThis ability is only active whenever the wearer is completely still."
				+ "\nSet Bonus: Reduces movement speed by a further 10%. Increases defense by 5.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 22;
			item.height = 16;
			item.rare = ItemRarityID.Orange;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.legSlot = slot;

			item.defense = ImplementedItem is null ? 6 : 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 0.85f;
			player.noKnockback = true;
			player.GetModPlayer<ArmorPlayer>().Steadfast = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AtlastriumBar>(), 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}