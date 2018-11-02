namespace Lambda.Common.Interfaces
{
    public interface IResponseMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class, new()
    {
        TDestination Map(TSource messageResponse);
    }
}