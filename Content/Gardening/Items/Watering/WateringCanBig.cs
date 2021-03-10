using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Content.Gardening.Items.Watering
{
	public class WateringCanBig : WateringCanClass
	{
		public override int MaxReach { get; protected set; } = 5;

		public override int WateringSpeed { get; protected set; } = 5;

		public override int MaxQuantity { get; protected set; } = 50;

		public override string ItemName { get; protected set; } = "Large Watering Can";

		public override void Defaults()
		{
			ItemDimensions = new Point(40, 32);
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.LiquidsHoneyWater;
			item.useTime = WateringSpeed;
			item.useAnimation = WateringSpeed;
			item.holdStyle = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
	}
}