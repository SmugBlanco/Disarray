namespace Disarray.Content.Paintings.Catacombs.Items
{
	public class SandsOfTimeItem : PaintingItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sands of Time");
			Tooltip.SetDefault("'Anonymous'");
		}

		public override (int Width, int Height) Dimensions { get; internal set; } = (66, 44);
	}
}