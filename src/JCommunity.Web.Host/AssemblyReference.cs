using System.Reflection;

namespace JCommunity.Host.Web;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
