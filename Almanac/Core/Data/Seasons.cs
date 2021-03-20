using System;

namespace Disarray.Almanac.Core.Data
{
	public static class SeasonData
	{
		public enum Seasons
		{
			Winter,
			Spring,
			Summer,
			Autumn
		}

		public static DateTime refTime => DateTime.Now;

		public static DateTime GetVernalEquinox(int Year)
        {
			return new DateTime(Year, 3, 20);
		}

		public static DateTime GetSummerSolstice(int Year)
		{
			int Date = 21;
			if (Year % 4 == 0)
            {
				Date = 20;
			}
			return new DateTime(Year, 6, Date);
		}

		public static DateTime GetAutumnalEquinox(int Year)
		{
			int ReferencedYear = Year;
			if (Year % 2 != 0)
            {
				ReferencedYear--;
			}

			int Date = 23;
			if (ReferencedYear % 4 == 0)
            {
				Date = 22;
			}
			return new DateTime(Year, 9, Date);
		}

		public static DateTime GetWinterSolstice(int Year)
		{
			int ReferencedYear = Year + 1;
			int Date = 21;
			if (ReferencedYear % 4 == 0)
			{
				Date = 22;
			}
			return new DateTime(Year, 12, Date);
		}

		public static DateTime GetSeasonDate(int Season, int Year)
        {
			int SeasonType = Season % 4;
			switch (SeasonType)
            {
				case 1:
					return GetVernalEquinox(Year);

				case 2:
					return GetSummerSolstice(Year);

				case 3:
					return GetAutumnalEquinox(Year);

				case 0:
				default:
					return GetWinterSolstice(Year);
			}
        }

		public static Seasons GetSeasonOnDate(DateTime date)
        {
			date = date.ToUniversalTime();

			DateTime spring = GetVernalEquinox(date.Year);
			if (spring > date)
            {
				return Seasons.Winter;
            }

			DateTime summer = GetSummerSolstice(date.Year);
			if (summer > date)
			{
				return Seasons.Spring;
			}

			DateTime autumn = GetAutumnalEquinox(date.Year);
			if (autumn > date)
			{
				return Seasons.Summer;
			}

			DateTime winter = GetWinterSolstice(date.Year);
			if (winter > date)
			{
				return Seasons.Autumn;
			}

			return Seasons.Winter;
        }
	}
}