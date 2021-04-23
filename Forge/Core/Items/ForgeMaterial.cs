using System;
using System.Collections.Generic;

namespace Disarray.Forge.Core.Items
{
	public abstract class ForgeMaterial : ForgeCore
	{
		public abstract IEnumerable<string> MaterialIdentity { get; }

		public abstract float QualityInfluence { get; }
	}
}