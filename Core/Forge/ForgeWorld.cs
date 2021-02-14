using Disarray.Content.Forge.Items.Rusty;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Core.Forge
{
	public class ForgeWorld : ModWorld
	{
		public override void PostWorldGen()
		{
			HandlePopulatingChestItems();
		}

		public void HandlePopulatingChestItems()
        {
			for (int Indexer = 0; Indexer < Main.chest.Length; Indexer++)
			{
				Chest chest = Main.chest[Indexer];
				if (chest != null)
				{
					HandleRustyItems(chest);
				}
			}
		}

		public void HandleRustyItems(Chest chest)
        {
			for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
			{
				if (chest.item[inventoryIndex].type == ItemID.None)
				{
					int[] RustyItems = new int[]
					{
						ModContent.ItemType<RustyBow>(),
						ModContent.ItemType<RustyCoil>(),
						ModContent.ItemType<RustyPistol>(),
						ModContent.ItemType<RustySword>(),
						ModContent.ItemType<RustyTome>(),
					};

					int itemTypeToSpawn = Utils.SelectRandom(Main.rand, RustyItems);
					chest.item[inventoryIndex].SetDefaults(itemTypeToSpawn);
					return;
				}
			}
		}
	}
}