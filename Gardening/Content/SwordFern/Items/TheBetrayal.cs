using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public class TheBetrayal : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Betrayal");
			Tooltip.SetDefault("Conjures a shortlived spark that can damage Town NPCs, said damage is multiplied by 25");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<BetrayalSpark>();
	}
}