using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Desert
{
	public class DustDevil : ModItem
	{
		public const int MaxTimeInWorld = 3600;

		public int TimeInWorld;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dust Devil");
			Tooltip.SetDefault("'A product of the furious sands'");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 32;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.value = 50;
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			if (++TimeInWorld > MaxTimeInWorld)
			{
				item.TurnToAir();
			}
		}
	}
}