using System;
using static Disarray.Almanac.Core.Data.Moonphase;

namespace Disarray.Extensions
{
	public static class DateTimeExtensions
    {
        public static PhasesOfMoon GetMoonphase(this DateTime date)
        {
            TimeSpan difference = date.ToUniversalTime().Subtract(refNewMoon);
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