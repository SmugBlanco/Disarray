using Microsoft.Xna.Framework;

namespace Disarray.Content.Gardening.Items.Watering
{
	public class WateringCan : WateringCanClass
	{
		public override int WateringSpeed { get; protected set; } = 5;

		public override string ItemName { get; protected set; } = "Watering Can";

		public override Point Dimensions { get; protected set; } = new Point(34, 26);

		public override int MaxCapacity { get; protected set; } = 100;
	}
}