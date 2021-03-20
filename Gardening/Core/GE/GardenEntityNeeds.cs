using System.Collections.Generic;

namespace Disarray.Gardening.Core.GE
{
	public abstract partial class GardenEntity : TileData
	{
		public virtual IEnumerable<PlantNeeds> Needs { get; protected set; }

		public bool UpdateAndCheckNeeds()
		{
			bool continueUpdate = true;
			foreach (PlantNeeds needs in Needs)
			{
				needs.Update();

				if (continueUpdate && !needs.FulfilledNeeds())
				{
					continueUpdate = false;
				}
			}
			return continueUpdate;
		}

		public virtual void SetUpNeeds() { }

		public override void PostSetupContent() => SetUpNeeds();
	}
}