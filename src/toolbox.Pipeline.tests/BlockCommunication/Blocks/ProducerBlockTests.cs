using Moq;
using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.BlockCommunication.Blocks;
using toolbox.Pipeline.BlockCommunication.Observer;
using Xunit;

namespace toolbox.Pipeline.tests.BlockCommunication.Blocks; 

// TODO: #10
public class ProducerBlockTests {

    private readonly ProducerBlock <int> testedProducerBlock;
    // assets
    private readonly Mock <IPublisher <int>> mockedPublisher;
    private readonly Mock <IConsumerBlock <int>> mockedConsumer;
    private readonly Mock <ISubscriberAdapterFactory <int>> mockedAdapterFactory;
    
    // -- assign
    public ProducerBlockTests() {
        this.mockedPublisher = new Mock <IPublisher <int>>();
        this.mockedConsumer = new Mock <IConsumerBlock <int>>();
        this.mockedAdapterFactory = new Mock <ISubscriberAdapterFactory <int>>();
        this.testedProducerBlock = new ProducerBlock <int>(
            this.mockedPublisher.Object, this.mockedAdapterFactory.Object);
    }
    
    // -- act
    [Fact]
    /* TODO:  I don't think I m interested whether factory.ProduceAdapter is called with the given
     consumer, I don't want to know if the adapter works, because it is a mock anyway, I just want
     to know whether the link is initiated */
    public void ExpectBlocksAreLinked() {
        this.mockedPublisher.Setup(publisher 
            => publisher.Subscribe(It.IsAny <ISubscriber <int>>()));
        this.testedProducerBlock.Link(this.mockedConsumer.Object);
        
        // assert
        this.mockedPublisher.Verify(publisher 
            => publisher.Subscribe(It.IsAny<ISubscriber <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectBlocksAreUnlinked() {
        this.mockedPublisher.Setup(publisher 
            => publisher.Unsubscribe(It.IsAny <ISubscriber <int>>()));
        this.testedProducerBlock.Unlink(this.mockedConsumer.Object);
        
        // assert
        this.mockedPublisher.Verify(publisher 
            => publisher.Unsubscribe(It.IsAny<ISubscriber <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectContextIsForwarded() {
        this.mockedPublisher.Setup(publisher
            => publisher.NotifySubscribers(It.IsAny <int>()));
        this.testedProducerBlock.Forward(2910);
        
        // assert
        this.mockedPublisher.Verify(publisher 
            => publisher.NotifySubscribers(It.IsAny<int>()), Times.Once);
    }
}