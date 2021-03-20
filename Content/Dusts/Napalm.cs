using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Dusts
{
	public class Napalm : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, Main.rand.Next(4) * 20, 10, 20);
		}

		public override bool Update(Dust dust)
		{
			dust.velocity.X *= 0.98f;
			dust.velocity.Y += 0.05f;
			dust.velocity.Y *= 0.995f;

			dust.scale *= 0.99f;
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

			Lighting.AddLight(dust.position, new Vector3(1.75f, 0.5f, 0.25f));
			return false;
		}
	}
}