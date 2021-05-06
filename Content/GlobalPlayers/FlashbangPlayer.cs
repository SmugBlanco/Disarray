using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.GlobalPlayers
{
	public class FlashbangPlayer : ModPlayer
	{
		public int FlashbangTimer = 0;

		public int GetFlashbangFadeTime { get => FlashbangFadeTime; set { FlashbangFadeTime = value; FlashbangFadeTimer = FlashbangFadeTime; } }

		private int FlashbangFadeTime;

		public int FlashbangFadeTimer { get; private set; }

		public override void PostUpdateMiscEffects()
		{
			if (FlashbangTimer > 0 || FlashbangFadeTimer > 0)
			{
				if (Main.netMode != NetmodeID.Server)
				{
					if (!Filters.Scene["Flashbang"].IsActive())
					{
						Filters.Scene.Activate("Flashbang");
					}

					Effect screenShader = Filters.Scene["Flashbang"].GetShader().Shader;
					screenShader.Parameters["active"].SetValue(true);

					if (FlashbangTimer > 0)
					{
						screenShader.Parameters["intensity"].SetValue(1f);
					}
					else
					{
						screenShader.Parameters["intensity"].SetValue(FlashbangFadeTimer / (float)GetFlashbangFadeTime);
					}
				}
			}
			else if (Main.netMode != NetmodeID.Server && Filters.Scene["Flashbang"].IsActive())
			{
				Filters.Scene["Flashbang"].GetShader().Shader.Parameters["active"].SetValue(false);
				Filters.Scene["Flashbang"].Deactivate();
			}

			FlashbangTimer--;

			if (FlashbangTimer < 0)
			{
				FlashbangFadeTimer--;
			}
		}
	}
}