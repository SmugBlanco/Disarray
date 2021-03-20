using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.PlayerProperties;
using Terraria;

namespace Disarray.Gardening.Content.SwordFern.Buffs
{
	public class SwordFernSpeedBoost : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sword Fern Boost - Speed");
			Description.SetDefault("Increases movement speed by 50%");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Speed speed = AutoloadedClass.CreateNewInstance<Speed>();
			speed.MovementSpeedMultiplier += 0.5f;
			PlayerProperty.ImplementProperty(player, speed, false);
		}
	}
}