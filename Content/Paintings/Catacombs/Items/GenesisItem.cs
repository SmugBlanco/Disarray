namespace Disarray.Content.Paintings.Catacombs.Items
{
	public class GenesisItem : PaintingItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Genesis");
			Tooltip.SetDefault("'Anonymous'");
		}

		public override (int Width, int Height) Dimensions { get; internal set; } = (66, 44);
	}
}