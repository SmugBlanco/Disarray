using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.CouchPotato
{
	public class CouchPotatoSpud : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Couch Potato");
			Tooltip.SetDefault("Minor improvements to all stats");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.maxStack = 30;

			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useAnimation = 15;
			item.useTime = 15;
			item.UseSound = SoundID.Item3;

			item.consumable = true;
			item.buffType = BuffID.WellFed;
			item.buffTime = 3600 * 3;
		}
	}
}