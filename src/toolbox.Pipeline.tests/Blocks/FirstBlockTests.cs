using Moq;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.Blocks;
using Xunit;

namespace toolbox.Pipeline.tests.Blocks; 

public class FirstBlockTests {
    
    private readonly FirstBlock <int> testObject;
    // assets
    private readonly string identifier = "test object block";
    private readonly Mock <IBlockStrategy <int>> mockedStrategy;
    private readonly Mock <IProducerBlock <int>> mockedProducer;
    
    // -- assign
    public FirstBlockTests() {
        this.mockedProducer = new Mock <IProducerBlock <int>>();
        this.mockedStrategy = new Mock <IBlockStrategy <int>>();
        this.testObject = new FirstBlock <int>(
            "test object block", this.mockedStrategy.Object, this.mockedProducer.Object);
    }
    
    // -- act
    [Fact]
    public void ExpectIdentifierIsInitialized()
        // assert
        => Assert.Equal(this.testObject.Identifier, this.identifier);

    [Fact]
    public void ExpectForwardOfContext() {
        this.mockedProducer
            .Setup(producer => producer.Forward(It.IsAny <int>()));
        
        this.testObject.Forward(1);
        // assert
        this.mockedProducer.Verify(producer => 
            producer.Forward(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void ExpectTransformOfForwardedContext() {
        var output = 1205;
        this.mockedProducer
            .Setup(producer => producer.Forward(It.IsAny <int>()));

        this.mockedStrategy
            .Setup(strategy => strategy.Execute(It.IsAny <int>()))
            .Returns(output);
        
        this.testObject.Forward(1);
        this.mockedProducer
            .Verify(producer => producer.Forward(output), Times.Once);        
        // assert
        this.mockedStrategy
            .Verify(strategy => strategy.Execute(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void ExpectLink() {
        var mockedConsumer = new Mock <IConsumerBlock <int>>();
        this.mockedProducer
            .Setup(producer => producer.Link(It.IsAny <IConsumerBlock <int>>()));
        
        this.testObject.Link(mockedConsumer.Object);
        
        // assert
        this.mockedProducer.Verify(producer 
            => producer.Link(It.IsAny<IConsumerBlock <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectUnlink() {
        var mockedConsumer = new Mock <IConsumerBlock <int>>();
        this.mockedProducer
            .Setup(producer => producer.Unlink(It.IsAny <IConsumerBlock <int>>()));
        
        this.testObject.Unlink(mockedConsumer.Object);
        
        // assert
        this.mockedProducer.Verify(producer 
            => producer.Unlink(It.IsAny<IConsumerBlock <int>>()), Times.Once);
    }
}