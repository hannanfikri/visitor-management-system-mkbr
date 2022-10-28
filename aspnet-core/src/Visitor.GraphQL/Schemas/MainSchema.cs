using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Visitor.Queries.Container;
using System;

namespace Visitor.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}