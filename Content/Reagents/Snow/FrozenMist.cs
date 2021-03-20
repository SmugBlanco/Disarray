using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Snow
{
	public class FrozenMist : ModItem
	{
		public const int MaxTimeInWorld = 3600;

		public int TimeInWorld;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Mist");
			Tooltip.SetDefault("'A phenomena that occurs at particularly damp environments below 0 degrees'");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = 5;
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