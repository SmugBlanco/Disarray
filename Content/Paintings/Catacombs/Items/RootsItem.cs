namespace Disarray.Content.Paintings.Catacombs.Items
{
	public class RootsItem : PaintingItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Roots");
			Tooltip.SetDefault("'Anonymous'");
		}

		public override (int Width, int Height) Dimensions { get; internal set; } = (66, 44);
	}
}