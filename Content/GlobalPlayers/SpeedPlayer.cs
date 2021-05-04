using Terraria.ModLoader;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class SpeedPlayer : ModPlayer
	{
		public float MovementSpeedMultiplier;

		public float MovementSpeedBoost;

		public override void ResetEffects()
		{
			MovementSpeedMultiplier = 1;
			MovementSpeedBoost = 0;
		}

		public override void PostUpdateRunSpeeds()
		{
			player.moveSpeed = (player.moveSpeed * MovementSpeedMultiplier) + MovementSpeedBoost;
			player.runAcceleration *= player.moveSpeed;
			player.maxRunSpeed *= player.moveSpeed;
			player.accRunSpeed *= player.moveSpeed;
		}
	}
}