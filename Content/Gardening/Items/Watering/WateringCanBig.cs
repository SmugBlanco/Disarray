using Microsoft.Xna.Framework;

namespace Disarray.Content.Gardening.Items.Watering
{
	public class WateringCanBig : WateringCanClass
	{
		public override int WateringSpeed { get; protected set; } = 4;

		public override string ItemName { get; protected set; } = "Large Watering Can";

		public override Point Dimensions { get; protected set; } = new Point(40, 32);

		public override int MaxCapacity { get; protected set; } = 200;
	}
}