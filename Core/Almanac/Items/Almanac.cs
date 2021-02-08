using Disarray.Core.Almanac.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Almanac.Items
{
	public class Almanac : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Almanac");
			Tooltip.SetDefault("Allows you to access to more information regarding certain topics"
			+ "\nUse to toggle almanac");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 15;
			item.useAnimation = 15;
		}

        public override bool UseItem(Player player)
        {
			Main.playerInventory = true;
			Disarray mod = ModContent.GetInstance<Disarray>();
			if (mod.AlmanacUserInterface?.CurrentState == null)
			{
				mod.AlmanacUserInterface?.SetState(new AlmanacUI());
			}
			else
            {
				mod.AlmanacUserInterface?.SetState(null);
			}
			return true;
        }
    }
}