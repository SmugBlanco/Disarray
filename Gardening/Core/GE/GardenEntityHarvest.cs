using Disarray.Gardening.Core.Needs;
using Terraria;
using System.Linq;

namespace Disarray.Gardening.Core.GE
{
	public abstract partial class GardenEntity : TileData
	{
		public virtual bool PreHarvest() => true;

		public virtual void OnHarvest(bool Elder) { }

		public void Harvest()
		{
			Harvest harvest = Needs.FirstOrDefault(needs => needs.Equals(GetClass<PlantNeeds>().GetData<Harvest>())) as Harvest;
			if (harvest is null || !PreHarvest() || !harvest.CanDisplayIcon())
			{
				return;
			}

			bool elder = GetCurrentStage == GrowthStages.Elder;
			OnHarvest(elder);
			if (elder)
			{
				WorldGen.KillTile(Position.X, Position.Y);
				return;
			}
			harvest.GetTimer = 0;
		}
	}
}