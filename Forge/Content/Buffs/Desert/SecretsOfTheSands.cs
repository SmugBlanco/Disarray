using Disarray.Core.Autoload;
using Disarray.Core.Properties;
using Disarray.PlayerProperties;
using Terraria;
using Terraria.ID;

namespace Disarray.Forge.Content.Buffs.Desert
{
	public class SecretsOfTheSands : BuffProperties
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Secrets of the Sands");
			Description.SetDefault("Increases movement speed by 10%" + "\nImmunity to sandstorms");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Speed speed = AutoloadedClass.CreateNewInstance<Speed>();
			speed.MovementSpeedMultiplier += 0.1f;
			PlayerProperty.ImplementProperty(player, speed, false);

			player.buffImmune[BuffID.WindPushed] = true;
		}
	}
}