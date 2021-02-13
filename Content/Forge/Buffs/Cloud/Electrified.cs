using Disarray.Core.Data;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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