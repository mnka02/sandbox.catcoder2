using Moq;
using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.BlockCommunication.Observer;
using toolbox.Pipeline.BlockCommunication.Observer.AdaptersFactories;
using Xunit;

namespace toolbox.Pipeline.tests.BlockCommunication.Observer.AdapterFactories; 

public class SubscriberAdapterFactoryTests {

    private readonly ISubscriberAdapterFactory <int> testedFactory;
    // assets
    private readonly Mock <IConsumerBlock <int>> mockedConsumerBlock;
    
    // -- assign
    public SubscriberAdapterFactoryTests() {
        this.mockedConsumerBlock = new Mock <IConsumerBlock <int>>();
        this.testedFactory = new SubscriberAdapterFactory <int>();
    }
    
    // -- act
    [Fact]
    public void ExpectSingletonAdapter() {
        var singletonAdapter = this.testedFactory
            .ProduceAdapter(this.mockedConsumerBlock.Object);

        var secondAdapter = this.testedFactory
            .ProduceAdapter(this.mockedConsumerBlock.Object);
        
        // -- assert
        Assert.Equal(singletonAdapter, secondAdapter);
    }

    [Fact]
    public void ExpectDifferentAdapterBaseOnDifferentConsumerBlocks() {

        var secondConsumerBlock = new Mock <IConsumerBlock <int>>();
        ISubscriber <int>[] prodcuedAdapters = new[] {
            this.testedFactory.ProduceAdapter(this.mockedConsumerBlock.Object),
            this.testedFactory.ProduceAdapter(secondConsumerBlock.Object)
        };
        
        // -- assert
        Assert.NotEqual(prodcuedAdapters[0], prodcuedAdapters[1]);
    }
}