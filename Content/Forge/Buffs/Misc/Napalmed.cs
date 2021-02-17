using Disarray.Core.Data;
using Terraria;

namespace Disarray.Content.Forge.Buffs.Misc
{
	public class Napalmed : DisarrayBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Napalmed");
			Description.SetDefault("'Some folks are born, made to wave the flag. They're red, white, and blue.'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}
	}
}