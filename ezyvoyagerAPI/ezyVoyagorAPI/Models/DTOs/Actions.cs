namespace ezyvoyagerAPI.Models.DTOs
{
    public class Actions
    {
    }
    public enum VoyagerActionType
    {
        Add,
        Edit,
        Delete,
        Cancelled,
        Login,
        Register,
        LogOut,
        Get,
        List
    }

    public enum VoyagerStaticDataType
    {
        Currency,
        Theme,
        Grade,
        ActivityType,
        PriceType,
        CountryCode,
        Frequency,
        DurationUnit,
        GroupType
    }
}