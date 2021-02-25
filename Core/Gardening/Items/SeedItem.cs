using System;
using Terraria.ModLoader;

namespace Disarray.Core.Gardening.Items
{
	public abstract class SeedItem : ModItem
	{
        public virtual GardeningInformation GardeningInformation => GardeningInformation.GetPlant(GardeningInformation.GetPlantID(GetType().Name.Replace("Seed", string.Empty)));

        public virtual void SafeDefaults() { }

        public override void SetDefaults()
        {
            SafeDefaults();

            if (GardeningInformation == null)
            {
                throw new Exception();
            }
        }
    }
}