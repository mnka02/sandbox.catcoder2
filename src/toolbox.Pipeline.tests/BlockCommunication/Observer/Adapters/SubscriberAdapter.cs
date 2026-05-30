using Moq;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.BlockCommunication.Observer.Adapters;
using Xunit;

namespace toolbox.Pipeline.tests.BlockCommunication.Observer.Adapters; 

public class SubscriberAdapterTests {

    private readonly SubscriberAdapter <int> testedAdapter;
    // assets
    private readonly Mock <IConsumerBlock <int>> mockedConsumerBlock;
    
    // -- assign
    public SubscriberAdapterTests() {
        this.mockedConsumerBlock = new Mock <IConsumerBlock <int>>();
        this.testedAdapter = new SubscriberAdapter <int>(
            this.mockedConsumerBlock.Object);
    }
    
    // -- act
    [Fact]
    public void ExpectAdaptedConsumerIsNotified() {
        this.mockedConsumerBlock.Setup(consumerBlock
            => consumerBlock.Receive(It.IsAny <int>()));
        this.testedAdapter.Notify(1705);
        
        // assert
        this.mockedConsumerBlock.Verify(consumerBlock 
            => consumerBlock.Receive(It.IsAny<int>()), Times.Once);
    }
}