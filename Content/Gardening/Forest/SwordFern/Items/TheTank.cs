using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Content.Gardening.Forest.SwordFern.Projectiles;
using Terraria;

namespace Disarray.Content.Gardening.Forest.SwordFern.Items
{
	public class TheTank : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Tank");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, decreases your next incoming damage by 25%");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
			item.value = 1000;

			item.damage = 3;

			item.UseSound = SoundID.Item20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 30;
			item.useTime = 30;

			item.shoot = ModContent.ProjectileType<TankSpark>();
			item.shootSpeed = 7.5f;
		}
	}
}