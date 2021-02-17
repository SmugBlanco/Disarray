using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Data;
using Disarray.Core.Forge.Items;
using Disarray.Core.Globals;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Cloud
{
	public class StormCloud : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm Cloud");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is SummonNimbusOnAttack);
			if (property is SummonNimbusOnAttack nimbusProperty)
			{
				nimbusProperty.AdditionalChance += 0.02f;
			}
			else
            {
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new SummonNimbusOnAttack(0.02f));
			}
		}

		public override void UpdateEquip(Player player)
		{
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is SummonNimbusOnAttack);
			if (property is SummonNimbusOnAttack nimbusProperty)
			{
				nimbusProperty.AdditionalChance += 0.02f;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new SummonNimbusOnAttack(0.02f));
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DisarrayGlobalPlayer GlobalPlayer = player.GetModPlayer<DisarrayGlobalPlayer>();
			PropertiesPlayer property = GlobalPlayer.ActiveProperties.FirstOrDefault(prop => prop is SummonNimbusOnAttack);
			if (property is SummonNimbusOnAttack nimbusProperty)
			{
				nimbusProperty.AdditionalChance += 0.02f;
			}
			else
			{
				player.GetModPlayer<DisarrayGlobalPlayer>().ActiveProperties.Add(new SummonNimbusOnAttack(0.02f));
			}
		}

		public override string ItemDescription() => "Somehow you got your hands on a stormy cloud; you must be prickling with electricity.";

		public override string ItemStatistics() => "Allows attacks access to a 10% chance to summon a small nimbus above your enemies." + "\nEach material increases said chance by 2%.";

		public override string ObtainingDetails() => "A mutation that occurs whenever stormy clouds are seeded into regular ones.";

		public override string MiscDetails() => " ";

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 8);
			recipe.AddIngredient(ItemID.RainCloud, 1);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Cloud>());
			recipe.AddIngredient(ItemID.RainCloud, 1);
			recipe.needWater = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}