using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Disarray.Core.Forge.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Forge.Items.Blacksmith
{
	public class Steel : Materials
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steel");
			Tooltip.SetDefault("Low grade tool steel");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
		}

		public override string ItemDescription() => "Steel is an alloy of iron with typically a few percent of carbon to improve its strength and fracture resistance compared to iron. Many other elements may be present or added. Stainless steels that are corrosion- and oxidation-resistant need typically an additional 11% chromium.";

		public override string ItemStatistics() => "+ 1 defense when held or equipped";

		public override string ObtainingDetails() => "Bought from the Blacksmith for 3 gold per piece";

		public override string MiscDetails() => "Iron and steel are used widely in the construction of roads, railways, other infrastructure, appliances, and buildings. Most large modern structures, such as stadiums and skyscrapers, bridges, and airports, are supported by a steel skeleton. Even those with a concrete structure employ steel for reinforcing. In addition, it sees widespread use in major appliances and cars. Despite the growth in usage of aluminium, it is still the main material for car bodies. Steel is used in a variety of other construction materials, such as bolts, nails and screws and other household products and cooking utensils.";



		public override void UpdateEquip(Player player)
        {
			player.statDefense += 1;
        }

        public override void HoldItem(Player player)
        {
            if (player.HeldItem.type != item.type)
            {
				player.statDefense += 1;
            }
        }
    }
}