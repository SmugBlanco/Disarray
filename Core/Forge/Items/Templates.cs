using Terraria;

namespace Disarray.Core.Forge.Items
{
	public abstract class Templates : ForgeBase
	{
		public virtual void SafeDefaults(Item item)
		{

		}

		public sealed override void SetDefaults()
		{
			SafeDefaults(item);

			NonProductDefaults();
		}
	}
}