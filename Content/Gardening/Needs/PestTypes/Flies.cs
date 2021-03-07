using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.Needs.PestTypes
{
	public class Flies : PestEntity
	{
		public int GetTimer;
		public override void AI()
		{
			GetTimer++;
			Vector2 Center = SourcePlant.Position.ToWorldCoordinates();
			Vector2 directionTo = Vector2.Normalize(Main.LocalPlayer.Center - Position);
			Velocity = directionTo *= 3f;
			Rotation = Velocity.ToRotation() + MathHelper.ToRadians(-90);
		}
	}
}