using System;
using AutoMapper;

namespace July09v31.UI.Models.CustomResolvers
{
    public class NullValueResolver : IValueResolver
    {

        public ResolutionResult Resolve(ResolutionResult source)
        {
            return new ResolutionResult(null);
        }
    }
}