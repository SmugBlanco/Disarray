using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Content.Gardening.Items.Nutrients
{
	public class PouchOfManure : NutrientItemClass
	{
		public override int MaxReach { get; protected set; } = 5;

		public override string ItemName { get; protected set; } = "Pouch Of Manure";

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;

			item.maxStack = 99;
			item.consumable = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item69;

			item.useTime = 15;
			item.useAnimation = 15;
			item.noUseGraphic = true;
			item.autoReuse = true;
		}
	}
}