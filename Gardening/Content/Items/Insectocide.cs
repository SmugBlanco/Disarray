using Disarray.Gardening.Core.Items;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Disarray.Gardening.Content.Items
{
	public class Insectocide : PesticideClass
	{
		public override int MaxReach { get; protected set; } = 5;

		public override string ItemName { get; protected set; } = "Insectocide";

		public override int MaxQuantity { get; protected set; } = 25;

		public override void Defaults()
		{
			ItemDimensions = new Point(20, 30);
			GetQuantity = MaxQuantity;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.holdStyle = 3;
			item.UseSound = SoundID.Item64;
			item.useTime = 15;
			item.useAnimation = 15;
			item.autoReuse = true;
		}
	}
}