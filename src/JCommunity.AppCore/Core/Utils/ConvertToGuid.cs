namespace JCommunity.AppCore.Core.Utils;

public static class GuidExtention
{
    public static Guid ConvertToGuid(this string value)
    {
        return Guid.Parse(value);
    }
}
