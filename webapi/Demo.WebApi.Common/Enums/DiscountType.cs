using Microsoft.OpenApi.Attributes;

namespace Demo.WebApi.Common.Enums
{
    public enum DiscountType
    {
        [Display("No")]
        None,
        [Display("Yes")]
        Employee
    }
}
