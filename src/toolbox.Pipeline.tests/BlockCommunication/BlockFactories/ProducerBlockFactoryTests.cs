using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.BlockCommunication.BlockFactories;
using Xunit;

namespace toolbox.Pipeline.tests.BlockCommunication.BlockFactories; 

public class ProducerBlockFactoryTests {
    
    private readonly IProducerBlockFactory <int> testedBlockFactory;

    // -- assign
    public ProducerBlockFactoryTests()
        => this.testedBlockFactory = new ProducerBlockFactory <int>();

    // -- act
    [Fact]
    public void ExpectInstanceOfBlockFactory()
        // -- assert
        => Assert.NotNull(this.testedBlockFactory.CreateProducerBlock());
}