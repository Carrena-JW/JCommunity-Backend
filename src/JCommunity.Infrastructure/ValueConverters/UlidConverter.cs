namespace JCommunity.Infrastructure.ValueConverters;
 
public class UlidConverter : ValueConverter<Ulid, string>
{
    private static readonly ConverterMappingHints defaultHints = new ConverterMappingHints(size: 26);

    public UlidConverter() : this(null!)
    {
    }

    public UlidConverter(ConverterMappingHints mappingHints = null!)
        : base(
                convertToProviderExpression: x => x.ToString(),
                convertFromProviderExpression: x => Ulid.Parse(x),
                mappingHints: defaultHints.With(mappingHints))
    {
    }
}