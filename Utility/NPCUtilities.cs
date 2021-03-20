using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Utility
{
	public static class NPCUtilities
	{
		public static string GetInternalName(int ID)
		{
			if (NPCID.Search.TryGetName(ID, out string internalName))
			{
				return internalName;
			}

			return NPCLoader.GetNPC(ID)?.Name;
		}
	}
}