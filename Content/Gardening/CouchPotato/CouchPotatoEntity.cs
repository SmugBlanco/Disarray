using Disarray.Core.Data;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace Disarray.Core.Gardening
{
	public class CouchPotatoEntity : GardenEntity
	{
		public override (int Sturdiness, int CheckInterval) WateringTimerInfo => (60 * 60, 60 * 5);

		public override (int Sturdiness, int CheckInterval) LightingTimerInfo => (60 * 60, 60 * 5);

		public override int TileCheckDistance => 0;

		public override void Update()
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(Position.ToWorldCoordinates(), 0, 0, DustID.SapphireBolt);
			}
		}
	}
}