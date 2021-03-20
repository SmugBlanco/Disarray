using Terraria.ModLoader;

namespace Disarray.Core.Properties
{
	public abstract class BuffProperties : ModBuff
	{
		public PlayerProperty PlayerProperties { get; internal set; }

		public NPCProperty NPCProperties { get; internal set; }
	}
}