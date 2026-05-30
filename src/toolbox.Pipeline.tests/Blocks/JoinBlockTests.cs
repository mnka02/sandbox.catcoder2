using Moq;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.Blocks;
using Xunit;

namespace toolbox.Pipeline.tests.Blocks; 

public class JoinBlockTests {

    private readonly JoinBlock <int> testObject;
    // assets
    private readonly string identifier = "otto";
    private readonly Mock <IConsumerBlock <int>> mockedConsumer;
    private readonly Mock <IBlockStrategy <int>> mockedStrategy;
    private readonly Mock <IProducerBlock <int>> mockedProducer;
    
    // -- assign 
    public JoinBlockTests() {
        this.mockedConsumer = new Mock <IConsumerBlock <int>>();
        this.mockedProducer = new Mock <IProducerBlock <int>>();
        this.mockedStrategy = new Mock <IBlockStrategy <int>>();
        this.testObject = new JoinBlock <int>(
            this.identifier, this.mockedStrategy.Object, this.mockedProducer.Object);
    }
    
    // -- act
    [Fact]
    public void ExpectConnectionBetweenBlocks() {
        this.mockedProducer.Setup(producer 
            => producer.Link(It.IsAny <IConsumerBlock <int>>()));
        this.testObject.Link(this.mockedConsumer.Object);
        
        // assert
        this.mockedProducer.Verify(producer 
            => producer.Link(It.IsAny<IConsumerBlock <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectConnectionLossBetweenBlocks() {
        this.mockedProducer.Setup(producer 
            => producer.Unlink(It.IsAny <IConsumerBlock <int>>()));
        this.testObject.Unlink(this.mockedConsumer.Object);
        
        // assert
        this.mockedProducer.Verify(producer 
            => producer.Unlink(It.IsAny<IConsumerBlock <int>>()), Times.Once);
    }

    [Fact]
    public void ExpectTransformationOfContext() {
        this.mockedProducer.Setup(producer 
            => producer.Forward(It.IsAny <int>()));
        this.mockedStrategy.Setup(strategy
            => strategy.Execute(It.IsAny <int>()));
        this.testObject.Receive(1964);
        
        // assert
        this.mockedStrategy.Verify(strategy 
            => strategy.Execute(It.IsAny<int>()), Times.Once);
        this.mockedProducer.Verify(producer
            => producer.Forward(It.IsAny<int>()), Times.Once);
    }
}