using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public abstract class SwordFernCombatModifiers : ModItem
	{
		public virtual int ProjectileType { get; protected set; }

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

			item.shoot = ProjectileType;
			item.shootSpeed = 7.5f;
		}
	}
}