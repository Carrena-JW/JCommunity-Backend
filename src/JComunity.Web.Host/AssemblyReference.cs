using System.Reflection;

namespace JComunity.Host.Web;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}