using Disarray.Core.Properties;
using Terraria;

namespace Disarray.Content.PlayerProperties
{
    public class Speed : PlayerProperty
    {
		public float MovementSpeedBoost;

        public float MovementSpeedMultiplier;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is Speed property)
            {
                MovementSpeedBoost += property.MovementSpeedBoost;
                MovementSpeedMultiplier += property.MovementSpeedMultiplier;
            }
        }

		public override void PostUpdateRunSpeeds(Player player)
		{
            player.moveSpeed = (player.moveSpeed * (MovementSpeedMultiplier + 1)) + MovementSpeedBoost;
            player.runAcceleration *= player.moveSpeed;
            player.maxRunSpeed *= player.moveSpeed;
            player.accRunSpeed *= player.moveSpeed;
        }
	}
}