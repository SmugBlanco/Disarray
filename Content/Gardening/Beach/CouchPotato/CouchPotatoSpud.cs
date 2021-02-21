using Disarray.Content.Forge.PlayerProperties;
using Disarray.Core.Forge.Items;
using Terraria;
using Terraria.ID;

namespace Disarray.Content.Gardening.Beach.CouchPotato
{
	public class CouchPotatoSpud : Materials
	{
		public float ChanceIncrement = 0.025f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Couch Potato");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.White;
			item.maxStack = 999;
		}

		public override void HoldItem(Player player)
		{
			HoneySickleHoneyBoost.ImplementThis(player, 1, 0.02f);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			HoneySickleHoneyBoost.ImplementThis(player, 1, 0.02f);
		}

		public override void OnHitPvp(Player player, Player target, int damage, bool crit)
		{
			HoneySickleHoneyBoost.ImplementThis(player, 1, 0.02f);
		}

		public override string ItemDescription() => "Notorious for being lazy, perhaps it may have some healing properties you can utilise in 'The Forge'";

		public override string ItemStatistics()
		{
			string DefaultAbility = "Allows attacks to a default 20% chance to apply 'Honey' onto yourself for 5 seconds." + "\nEach material increases said chance by 2%.";
			string HoneyBoost = "Every third material increases life regeneration while 'Honey'ed by 1.";
			return DefaultAbility + "\n" + HoneyBoost;
		}

		public override string ObtainingDetails() => "Couch Potatos can be obtained from harvesting Couch Potato plants.";

		public override string MiscDetails() => "";
	}
}