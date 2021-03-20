using Terraria;

namespace Disarray.Forge.Core.Items
{
	public abstract class Materials : ForgeBase
	{
		public virtual void ApplyToAllScenarios(Player player) { }

		public override void HoldItem(Player player) => ApplyToAllScenarios(player);

		public override void UpdateEquip(Player player) => ApplyToAllScenarios(player);

		public override void UpdateAccessory(Player player, bool hideVisual) => ApplyToAllScenarios(player);
	}
}