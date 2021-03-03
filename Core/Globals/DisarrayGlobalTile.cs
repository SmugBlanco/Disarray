using Disarray.Core.Gardening.Tiles;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
	public class DisarrayGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			foreach (FloraBase flora in FloraBase.LoadedBases)
            {
				//flora.NaturalSpawning(i, j, type);
            }
		}
	}
}