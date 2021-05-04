using Terraria.ModLoader;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class InvincePlayer : ModPlayer
	{
		public int InvinceTime;

		public override void ResetEffects() => InvinceTime = 0;

		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			player.immuneTime += InvinceTime;
		}
	}
}
