using Xunit.Sdk;

namespace toolbox.Pipeline.tests.integration; 

public class IntegrationTestAttribute : Attribute, ITraitAttribute {
    public IEnumerable<KeyValuePair<string, string>> GetTraits()
        => new[] { new KeyValuePair<string, string>("Category", "Integration") };
}