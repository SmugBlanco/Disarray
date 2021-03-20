namespace Disarray.Content.Paintings.Catacombs.Items
{
	public class VictoryItem : PaintingItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Victory");
			Tooltip.SetDefault("'Anonymous'");
		}

		public override (int Width, int Height) Dimensions { get; internal set; } = (66, 44);
	}
}