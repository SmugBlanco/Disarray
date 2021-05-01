namespace Disarray.Almanac.Core
{
    public interface IAlmanacable
    {
        string GeneralDescription { get; set; }

        string ItemStatistics { get; set; }

        string ObtainingGuide { get; set; }

        string Miscellaneous { get; set; }
	}
}