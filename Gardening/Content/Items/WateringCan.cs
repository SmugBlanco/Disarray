using Disarray.Gardening.Core.Items;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Gardening.Content.Items
{
	public class WateringCan : WateringCanClass
	{
		public override int MaxReach { get; protected set; } = 5;

		public override int WateringSpeed { get; protected set; } = 5;

		public override string ItemName { get; protected set; } = "Watering Can";

		public override int MaxQuantity { get; protected set; } = 125;

		public override void Defaults()
		{
			ItemDimensions = new Point(34, 26);
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