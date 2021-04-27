namespace Disarray.Utility
{
	public static class ItemUtilities
	{
		public static string GetKnockbackDescriptor(float knockBack, bool lowerCase = false)
		{
			if (knockBack <= 0)
			{
				return string.Empty;
			}

			string upperOutput = string.Empty;

			// Hardcoding the output is faster ( about 5x ) than string.ToLower().

			if (knockBack <= 1.5f)
			{
				upperOutput = "Extremely Weak";
			}
			else if (knockBack <= 3f)
			{
				upperOutput = "Very Weak";
			}
			else if (knockBack <= 4f)
			{
				upperOutput = "Weak";
			}
			else if (knockBack <= 6f)
			{
				upperOutput = "Average";
			}
			else if (knockBack <= 7)
			{
				upperOutput = "Strong";
			}
			else if (knockBack <= 9)
			{
				upperOutput = "Very Strong";
			}
			else if (knockBack <= 11)
			{
				upperOutput = "Extremely Strong";
			}
			else
			{
				upperOutput = "Insane";
			}

			return lowerCase ? upperOutput.ToLower() : upperOutput;
		}

		public static string GetSpeedDescriptor(int useTime, bool lowerCase = false)
		{
			string output = string.Empty;

			// Hardcoding the output is faster ( about 5x ) than string.ToLower().

			if (useTime <= 8)
			{
				output = "Insanely Fast";
			}
			else if (useTime <= 20)
			{
				output = "Very Fast";
			}
			else if (useTime <= 25)
			{
				output = "Fast";
			}
			else if (useTime <= 30)
			{
				output = "Average";
			}
			else if (useTime <= 35)
			{
				output = "Slow";
			}
			else if (useTime <= 45)
			{
				output = "Very Slow";
			}
			else if (useTime <= 55)
			{
				output = "Extremely Slow";
			}
			else
			{
				output = "Snail";
			}

			return lowerCase ? output.ToLower() : output;
		}
	}
}