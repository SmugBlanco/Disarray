using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Reflection;
using Terraria.ModLoader.IO;

namespace Disarray.Core.Forge.Items
{
	public abstract partial class ForgeBase : ModItem
	{
		public float Damage = 0;
		public int DamageFlat = 0;
		public int Defense = 0;
		public float DamageReduction = 0;
		public int MaxHealth = 0;
	}
}