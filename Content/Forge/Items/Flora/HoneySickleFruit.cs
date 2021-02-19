using Disarray.Content.Forge.Dusts.Misc;
using Disarray.Content.Forge.PlayerProperties;
using Disarray.Content.Forge.Projectiles.Properties;
using Disarray.Core.Forge.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Flora
{
	public class HoneySickleFruit : Materials
	{
		public float ChanceIncrement = 0.025f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honey Sickle Fruit");
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

		public override string ItemDescription() => "The fruits of a Honey Sickle plant, perhaps it may have some healing properties you can utilise in 'The Forge'";

		public override string ItemStatistics()
		{
			string DefaultAbility = "Allows attacks to a default 20% chance to apply 'Honey' onto yourself for 5 seconds." + "\nEach material increases said chance by 2%.";
			string HoneyBoost = "Every third material increases life regeneration while 'Honey'ed by 1.";
			return DefaultAbility + "\n" + HoneyBoost;
		}

		public override string ObtainingDetails() => "Honey Sickle Fruit can be obtained from harvesting Honey Sickle plants.";

		public override string MiscDetails() => "";
	}
}