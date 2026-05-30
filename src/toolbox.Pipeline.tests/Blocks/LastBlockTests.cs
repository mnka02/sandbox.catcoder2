using Moq;
using toolbox.Observables;
using toolbox.Pipeline.Blocks;
using Xunit;

namespace toolbox.Pipeline.tests.Blocks; 

public class LastBlockTests {

    private readonly LastBlock <int> testObject;
    // assets
    private readonly string identifier = "maya";
    private readonly Mock <ISubscriber <int>> mockedSubscriber;
    private readonly Mock <IBlockStrategy <int>> mockedStrategy;
    private readonly Mock <IPublisher <int>> mockedPublisher;
    
    // -- assign
    public LastBlockTests() {
        this.mockedStrategy = new Mock <IBlockStrategy <int>>();
        this.mockedSubscriber = new Mock <ISubscriber <int>>();
        this.mockedPublisher = new Mock <IPublisher <int>>();
        this.testObject = new LastBlock <int>(
            this.identifier, this.mockedStrategy.Object, this.mockedPublisher.Object);
    }
    
    // -- act
    [Fact]
    public void ExpectSubscription() {
        this.mockedPublisher.Setup(publisher 
            => publisher.Subscribe(It.IsAny <ISubscriber <int>>()));
        
        this.testObject.Subscribe(this.mockedSubscriber.Object);
        
        this.mockedPublisher.Verify(publisher 
            => publisher.Subscribe(It.IsAny<ISubscriber <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectUnsubscription() {
        this.mockedPublisher.Setup(publisher 
            => publisher.Unsubscribe(It.IsAny <ISubscriber <int>>()));
        
        this.testObject.Unsubscribe(this.mockedSubscriber.Object);
        
        this.mockedPublisher.Verify(publisher 
            => publisher.Unsubscribe(It.IsAny<ISubscriber <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectTransformationOfContextAfterConception() {
        this.mockedStrategy
            .Setup(strategy => strategy.Execute(It.IsAny <int>()));
        this.mockedPublisher
            .Setup(publisher => publisher.NotifySubscribers(It.IsAny <int>()));
        this.testObject.Receive(2004);
        
        // assert
        this.mockedStrategy.Verify(strategy 
            => strategy.Execute(It.IsAny<int>()), Times.Once);
        this.mockedPublisher.Verify(publisher 
            => publisher.NotifySubscribers(It.IsAny<int>()), Times.Once);
    }
}