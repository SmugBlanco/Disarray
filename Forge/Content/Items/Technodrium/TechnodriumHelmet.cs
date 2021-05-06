using Disarray.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Technodrium
{
	public class TechnodriumHelmet : TechnodriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Head);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technodrium Helmet");
			Tooltip.SetDefault("Increases critical strike chance by 12%"
			+ "\nIncreases outgoing damage by 10%"
			+ "\n'Informational Visor' - Highlights nearby enemies in a red tint.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "10 template defense, 12 base defense"
				+ "\nIncreases critical strike chance by 12%"
				+ "\nIncreases outgoing damage by 10%"
				+ "\nGrants the wearer the passive ability 'Informational Visor', tinting nearby enemies in a shade of distinguishable red."
				+ "\nSet Bonus: Further increases movement speed by 10%.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 22;
			item.height = 22;
			item.rare = ItemRarityID.LightRed;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.headSlot = slot;

			item.defense = ImplementedItem is null ? 10 : 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 12;
			player.rangedCrit += 12;
			player.magicCrit += 12;
			player.allDamage += 0.1f;
			player.GetModPlayer<TechnodriumPlayer>().InformationalVisor = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			bool forgedChestplate = body.modItem is ForgeItem chestPlate && chestPlate.GetTemplate != null && chestPlate.GetTemplate.item.type == ModContent.ItemType<TechnodriumChestplate>();
			bool forgedLeggings = legs.modItem is ForgeItem legging && legging.GetTemplate != null && legging.GetTemplate.item.type == ModContent.ItemType<TechnodriumLeggings>();
			return (body.type == ModContent.ItemType<TechnodriumChestplate>() || forgedChestplate) && (legs.type == ModContent.ItemType<TechnodriumLeggings>() || forgedLeggings);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Further increases movement speed by 10%.";
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 1.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TechnodriumBar>(), 18);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}