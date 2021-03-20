using Terraria.ModLoader.IO;

namespace Disarray.Gardening.Core.GE
{
	public abstract partial class GardenEntity : TileData
	{
		public sealed override TagCompound Save()
		{
			TagCompound data = new TagCompound()
			{
				{ "GrowthTimer", GrowthTimer % GrowthInfo.GrowthInterval },
				{ "Growth", Growth },
				{ "Extra", SaveExtra() },
			};

			foreach (PlantNeeds needs in Needs)
			{
				data.Add(needs.Name, needs.Save());
			}

			return data;
		}

		public virtual TagCompound SaveExtra() => null;

		public sealed override void Load(TagCompound tagCompound)
		{
			GrowthTimer = tagCompound.Get<int>("GrowthTimer");
			GetGrowth = tagCompound.Get<float>("Growth");
			LoadExtra(tagCompound.Get<TagCompound>("Extra"));

			if (Needs is null)
			{
				return;
			}

			foreach (PlantNeeds needs in Needs)
			{
				if (tagCompound.ContainsKey(needs.Name))
				{
					needs.Load(tagCompound.Get<TagCompound>(needs.Name));
				}
			}
		}

		public virtual void LoadExtra(TagCompound tagCompound) { }
	}
}