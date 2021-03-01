using Disarray.Content.PlayerProperties;
using Disarray.Core.Data;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Disarray.Content.Gardening.SwordFern.Buffs
{
	public class SwordFernSpeedBoost : DisarrayBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sword Fern Boost - Speed");
			Description.SetDefault("Increases movement speed by 50%");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) => PlayerProperty.ImplementProperty(player, new Speed() { MovementSpeedMultiplier = 0.5f }, false);
	}
}