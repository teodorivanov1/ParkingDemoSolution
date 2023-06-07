using Xunit.Abstractions;
using Xunit.Sdk;

namespace Demo.Tests.Api
{
    public class SequentialTestCaseOrderer : ITestCaseOrderer
    {
        public const string TypeName = "Demo.Tests.Api.Tests";
        public const string AssemblyName = "Demo.Tests.Api";

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var orderedTestCases = testCases.OrderBy(t => t.DisplayName);
            return orderedTestCases;
        }
    }
}
