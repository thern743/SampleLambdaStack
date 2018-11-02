using System.Collections.Generic;
using Lambda.Common.Interfaces;

namespace Lambda.Common.ResponseMappers
{
    /// <summary>
    /// The intent of this class is to mirror functionality similar to AutoMapper without the dependencies.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public abstract class ResponseMapper<TSource, TDestination> : IResponseMapper<TSource, TDestination>
        where TSource: class 
        where TDestination : class, new()
    {
        protected TSource Source { get; set; }
        protected TDestination Destination { get; set; }
        public IDictionary<string, string> Links { get; set; }
        public abstract TDestination Map(TSource messageResponse);
    }
}
