using Disarray.Forge.Core.GlobalPlayers;
using Disarray.Forge.Core.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Forge.Content.Items.Atlastrium
{
	public class AtlastriumHelmet : AtlastriumItem
	{
		public override bool Autoload(ref string name) => AutoloadArmor(name, item, EquipType.Head);

		public override IReadOnlyDictionary<string, float> MaterialTypeInfluence { get; } = new Dictionary<string, float> { { "Metal", 1f } };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AtlastriumHelmet");
			Tooltip.SetDefault("Reduces movement speed by 10%"
			+ "\n'Protective Sights' - Double equipment defense but severly limits vision.");
		}

		public override string ItemStatistics
		{
			get
			{
				string statistic = "6 template defense, 7 base defense"
				+ "\nReduces movement speed by 10%"
				+ "\nGrants the wearer the passive ability 'Protective Sights'"
				+ "\n'Protective Sights' doubles defense after equipment is accounted for, but limits vision to the direction your player is facing."
				+ "\nSet Bonus: Reduces movement speed by a further 10%. Increases defense by 5.";
				return statistic + "\n" + StatTooltip;
			}
		}

		public override void SafeDefaults(Item item, float quality)
		{
			item.width = 18;
			item.height = 20;
			item.rare = ItemRarityID.Orange;

			SlotData.TryGetValue(base.item.type, out int slot);
			item.headSlot = slot;

			item.defense = ImplementedItem is null ? 6 : 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 0.9f;
			player.GetModPlayer<ArmorPlayer>().ProtectiveSights = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			bool forgedChestplate = body.modItem is ForgeItem chestPlate && chestPlate.GetTemplate != null && chestPlate.GetTemplate.item.type == ModContent.ItemType<AtlastriumChestplate>();
			bool forgedLeggings = body.modItem is ForgeItem legging && legging.GetTemplate != null && legging.GetTemplate.item.type == ModContent.ItemType<AtlastriumLeggings>();
			return (body.type == ModContent.ItemType<AtlastriumChestplate>() || forgedChestplate) && (legs.type == ModContent.ItemType<AtlastriumLeggings>() || forgedLeggings);
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Reduces movement speed by a further 10%. Increases defense by 5.";
			player.statDefense += 5;
			player.GetModPlayer<SpeedPlayer>().MovementSpeedMultiplier *= 0.9f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AtlastriumBar>(), 18);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}