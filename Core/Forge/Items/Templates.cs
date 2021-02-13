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
using ReLogic.Utilities;

namespace Disarray.Core.Forge.Items
{
	public enum TemplateType
	{
		Mold,
		Artifact
	}

	public abstract class Templates : ForgeBase
	{
		public virtual void SafeDefaults(Item item)
		{

		}

		public sealed override void SetDefaults()
		{
			SafeDefaults(item);

			NonProductDefaults();
		}
	}
}