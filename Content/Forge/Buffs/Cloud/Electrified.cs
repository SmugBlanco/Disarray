using Disarray.Core.Data;
using Terraria;

namespace Disarray.Content.Forge.Buffs.Cloud
{
	public class Electrified : DisarrayBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Electrified");
			Description.SetDefault("'An inordinary amount of electricity is passing through your system'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}
    }
}