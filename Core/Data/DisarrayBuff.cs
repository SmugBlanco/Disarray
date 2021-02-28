using Disarray.Core.Properties;
using Terraria.ModLoader;

namespace Disarray.Core.Data
{
	public abstract class DisarrayBuff : ModBuff
	{
		public PlayerProperty PlayerProperties { get; internal set; }

		public NPCProperty NPCProperties { get; internal set; }
	}
}