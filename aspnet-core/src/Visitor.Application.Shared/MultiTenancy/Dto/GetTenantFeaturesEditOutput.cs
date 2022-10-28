using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Visitor.Editions.Dto;

namespace Visitor.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}