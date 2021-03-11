using Disarray.Core.Data;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;

namespace Disarray.Content.Gardening.Needs.PestTypes
{
	public class Flies : PestEntity
	{
		public Vector2 SourcePosition => SourcePlant.Position.ToWorldCoordinates();
		public bool Resting;
		public int AITimer = 180;
		public const int AITimerCap = 180;
		public int PhaseTimer;

		public override void AI()
		{
			if (Resting)
			{
				if (AITimer > AITimerCap)
				{
					AITimer = AITimerCap;
				}

				if (AITimer > 0)
				{
					AITimer--;
				}

				PhaseTimer++;

				if (PhaseTimer > 300)
				{
					PhaseTimer = 0;
					Resting = false;
				}
			}
			else
			{
				if (AITimer <= 0)
				{
					AITimer = AITimerCap;
				}

				PhaseTimer++;

				if (PhaseTimer > 900)
				{
					PhaseTimer = 0;
					Resting = true;
				}
			}

			int determineTimer = Resting ? AITimer : PhaseTimer;
			float xOffset = (float)(determineTimer * Math.Sin(determineTimer / 10) / 3);
			Vector2 offset = new Vector2(xOffset, -AITimer / 5f + (float)(Math.Sin(determineTimer / 3f) * (AITimer / 9))); 
			Vector2 destinationPosition = SourcePosition + offset;
			Vector2 directionTo = Vector2.Normalize(destinationPosition - Position);
			float Speed = 1f;
			Velocity = directionTo * Speed;

			if (Resting && Vector2.DistanceSquared(SourcePosition, Position) < 4)
			{
				Velocity = Vector2.Zero;
			}

			Rotation = Velocity.ToRotation();
			if (Rotation > MathHelper.ToRadians(180))
			{
				Rotation -= MathHelper.ToRadians(180);
			}
		}

		public override bool CanSpawn(Pests pest, int timer)
		{
			return timer > 86400 && (from pestData in pest.CurrentPests where pestData is Flies select pestData).Count() == 0;
		}

		public override bool CanKill(Player player) => true;

		public override void OnKill()
		{
			for (int indexer = 0; indexer < 3; indexer++)
			{
				Dust.NewDustPerfect(Main.MouseWorld, DustID.Firefly, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2)));
			}
		}
	}
}