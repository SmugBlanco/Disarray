using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Dusts.Cloud
{
	public class Electricity : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 6, 14);
		}

		public override bool Update(Dust dust)
		{
			dust.velocity.X *= 0.975f;
			dust.velocity.Y += 0.05f;
			dust.velocity.Y *= 0.99f;

			dust.scale *= 0.98f;
			if (dust.scale < 0.25f)
            {
				dust.active = false;
            }

			dust.alpha++;
			if (dust.alpha >= 255)
            {
				dust.active = false;
            }

			dust.position += dust.velocity;
			return false;
		}
	}
}