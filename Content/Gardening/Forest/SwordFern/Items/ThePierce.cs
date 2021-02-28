using Terraria.ModLoader;
using Terraria.ID;
using Disarray.Content.Gardening.Forest.SwordFern.Projectiles;
using Terraria;

namespace Disarray.Content.Gardening.Forest.SwordFern.Items
{
	public class ThePierce : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Pierce");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, increases your armor piercing by 20.");
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

			item.shoot = ModContent.ProjectileType<PierceSpark>();
			item.shootSpeed = 7.5f;
		}
	}
}