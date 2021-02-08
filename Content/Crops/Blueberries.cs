using Disarray.Core.Data;

namespace Disarray.Content.Crops
{
    public class Blueberries : Crop
    {
        public override void SetDefaults()
        {
            DisplayName = name;
            Description = "A perennials berry which has been noted as an antioxidant";
            Origin = "???";
            PlantingMonths = "Spring or a mild fall, cooler climates may kill some varieties";
            HarvestMonths = "June - August";
            PricePerPound = "Averages between $2 and $4 from standard retailers";
        }
    }
}