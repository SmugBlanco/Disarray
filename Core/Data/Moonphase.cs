using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using Terraria.GameInput;
using System.IO;
using Terraria.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;

namespace Disarray.Core.Data
{
	public struct Moonphase
	{
		public enum PhasesOfMoon
        {
			NewMoon,
			WaxingCrescent,
			FirstQuarter,
			WaxingGibbous,
			FullMoon,
			WaningGibbous,
			ThirdQuarter,
			WaningCrescent
        }

		public const double MoonPhaseCycleInHour = 708.744;

		public const float SimpleDaysBetweenPhase = 3.6875f;

		public const float SimpleHoursBetweenPhase = 88.5f;

		public static DateTime refNewMoon => new DateTime(2021, 1, 13, 5, 0, 0);

		public DateTime refTime { get; private set; }

		public Moonphase(DateTime refTime)
		{
			this.refTime = refTime.ToUniversalTime();
		}

		public PhasesOfMoon GetMoonphase()
        {
			TimeSpan difference = refTime.Subtract(refNewMoon);
			double HoursIntoMoonCycle = difference.TotalHours % MoonPhaseCycleInHour;
			double HoursInDay = 24;
			if (HoursIntoMoonCycle < HoursInDay || HoursIntoMoonCycle > MoonPhaseCycleInHour - HoursInDay)
            {
				return PhasesOfMoon.NewMoon;
            }

			if (HoursIntoMoonCycle > (MoonPhaseCycleInHour / 2) - HoursInDay && HoursIntoMoonCycle < (MoonPhaseCycleInHour / 2) + HoursInDay)
			{
				return PhasesOfMoon.FullMoon;
			}

			if (HoursIntoMoonCycle > (MoonPhaseCycleInHour / 4) - HoursInDay && HoursIntoMoonCycle < (MoonPhaseCycleInHour / 4) + HoursInDay)
			{
				return PhasesOfMoon.FirstQuarter;
			}

			if (HoursIntoMoonCycle > (MoonPhaseCycleInHour * 0.75) - HoursInDay && HoursIntoMoonCycle < (MoonPhaseCycleInHour * 0.75) + HoursInDay)
			{
				return PhasesOfMoon.ThirdQuarter;
			}

			if (HoursIntoMoonCycle < (MoonPhaseCycleInHour / 4) - HoursInDay)
            {
				return PhasesOfMoon.WaxingCrescent;
            }

			if (HoursIntoMoonCycle < (MoonPhaseCycleInHour / 2) - HoursInDay)
			{
				return PhasesOfMoon.WaxingGibbous;
			}

			if (HoursIntoMoonCycle < (MoonPhaseCycleInHour * 0.75) - HoursInDay)
			{
				return PhasesOfMoon.WaningGibbous;
			}

			return PhasesOfMoon.WaningCrescent;
		}
	}
}