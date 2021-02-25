using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace Disarray.Content.Gardening.Items
{
	public class LightMeasurer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light Measurer");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item1;
			item.useTime = 15;
			item.useAnimation = 15;
		}

        public override bool UseItem(Player player)
        {
			if (player.itemAnimation == player.itemAnimationMax - 1)
			{
				Vector3 subLight = Lighting.GetSubLight(Main.MouseWorld);
				float averagedLighting = (subLight.X + subLight.Y + subLight.Z) / 3 * 1.2f;
				if (averagedLighting > 1)
                {
					averagedLighting = 1;
                }
				Main.NewText(averagedLighting * 100 + "% Lighted");
			}
            return base.UseItem(player);
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}