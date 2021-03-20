using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents.Caverns.Lava
{
	public class Embers : ModItem
	{
		public const int MaxTimeInWorld = 3600;

		public int TimeInWorld;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Embers");
			Tooltip.SetDefault("'Fiery sparks that hold the potential to roar once more'");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.maxStack = 999;
			item.value = 25;
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