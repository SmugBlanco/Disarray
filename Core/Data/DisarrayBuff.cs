using System;
using Terraria.ModLoader;

namespace Disarray.Core.Data
{
	public abstract class DisarrayBuff : ModBuff
	{
		public PropertiesBuffs Properties { get; internal set; }
	}
}