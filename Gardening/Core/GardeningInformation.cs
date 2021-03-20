namespace Disarray.Gardening.Core
{
	public struct GardeningInformation
	{
		public GardeningInformation(string texture, string name, string description, float difficulty, float lighting, (int type, float rating) watering)
		{
			Texture = texture;
			DisplayName = name;
			Description = description;
			Difficulty = difficulty;
			Lighting = lighting;
			Watering = watering;
		}

		public string Texture;

		public string DisplayName;

		public string Description;

		public float Difficulty;

		public float Lighting;

		public (int type, float rating) Watering;
	}
}