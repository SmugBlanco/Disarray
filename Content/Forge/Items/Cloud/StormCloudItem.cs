using Disarray.Core.Forge.Items;

namespace Disarray.Content.Forge.Items.Cloud
{
	public abstract class StormCloudItem : Templates
	{
		public override string ItemDescription() => "Can be modified in 'The Forge' to become an usable item.";

		public override string ObtainingDetails() => "A mutation that occurs whenever stormy clouds are seeded into regular ones.";

		public override string MiscDetails() => " ";
	}
}