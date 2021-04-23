using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Caverns
{
	public class Darkness : ModItem
	{
		public const int MaxTimeInWorld = 3600;

		public int TimeInWorld;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkness");
			Tooltip.SetDefault("'Cover for those who like to remain anonymous'");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 999;
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