using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Forge.Items
{
	public abstract partial class ForgeBase : ModItem
	{
		public float Damage = 0;
		public int DamageFlat = 0;
		public int Defense = 0;
		public float DamageReduction = 0;
		public int MaxHealth = 0;

		public void ImplementStats(Player player)
		{
			ForgePlayer forgePlayer = player.GetModPlayer<ForgePlayer>();
			forgePlayer.Damage += Damage;
			forgePlayer.DamageFlat += DamageFlat;
			player.statDefense += Defense;
			player.endurance += DamageReduction / 100f;
			player.statLifeMax2 += MaxHealth;
		}
	}
}