using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.GlobalPlayers
{
	public class ScreenshakePlayer : ModPlayer
	{
		public int Intensity;

		public int Duration;

		public override void ModifyScreenPosition()
		{
			if (Duration > 0)
			{
				Main.screenPosition += new Vector2(Main.rand.Next(-Intensity, Intensity + 1), Main.rand.Next(-Intensity, Intensity + 1));
			}

			Duration--;
		
			if (Intensity > 0 && Duration < 0 && Duration % 60 == 0)
			{
				Intensity--;
			}
		}
	}
}