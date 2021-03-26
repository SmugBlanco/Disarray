using Disarray.Core.Properties;
using Microsoft.Xna.Framework;
using Terraria;

namespace Disarray.PlayerProperties
{
    public class Light : PlayerProperty
    {
        public float PlayerLightIntensity;

        public float CursorLightIntensity;

        public override void Combine(PlayerProperty newProperty)
        {
            if (newProperty is Light property)
            {
                PlayerLightIntensity += property.PlayerLightIntensity;
                CursorLightIntensity += property.CursorLightIntensity;
            }
        }

		public override void PostUpdateMiscEffects(Player player)
		{
            if (Main.myPlayer != player.whoAmI)
			{
                return;
			}

            if (PlayerLightIntensity > 0)
            {
                Lighting.AddLight(player.Center, new Vector3(PlayerLightIntensity));
            }

            if (CursorLightIntensity > 0)
            {
                Lighting.AddLight(Main.MouseWorld, new Vector3(CursorLightIntensity));
            }
        }
	}
}