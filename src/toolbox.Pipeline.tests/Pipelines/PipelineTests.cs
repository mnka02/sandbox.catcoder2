using Moq;
using toolbox.Observables;
using toolbox.Pipeline.BlockFactories;
using toolbox.Pipeline.Pipelines;
using Xunit;

namespace toolbox.Pipeline.tests.Pipelines;

public class PipelineTests {

    private Pipeline <int> testedPipeline;
    // assets
    private readonly IBlockFactory <int> blockFactory;
    private readonly IFirstBlock <int> firstBlock;
    private readonly ILastBlock <int> lastBlock;
    private readonly Mock <IBlockStrategy <int>> mockedStrategy;
    private readonly Mock <ISubscriber <int>> [] mockedSubscriberList
        = new [] { new Mock <ISubscriber <int>>(), new Mock <ISubscriber <int>>()};

    // -- assign
    public PipelineTests() {
        this.mockedStrategy = new Mock <IBlockStrategy <int>>();
        // blocks
        this.blockFactory = new BlockFactory <int>();
        this.firstBlock = this.blockFactory.ProduceFirstBlock(
            "Herberth", this.mockedStrategy.Object);
        this.lastBlock = this.blockFactory.ProduceLastBlock(
            "Harry", this.mockedStrategy.Object);
    }

    [Fact]
    public void ExpectNoNotificationsAfterDispose() {
        var subsetSize = 1;
        var subsetOfSubscribers = new List <ISubscriber <int>>();
        for (int i = 0; i < subsetSize; i++)
            subsetOfSubscribers.Add(this.mockedSubscriberList[i].Object);
    
        // -- assign
        this.testedPipeline = new Pipeline <int>(
            this.firstBlock, this.lastBlock, subsetOfSubscribers);
    
        // -- act
        this.testedPipeline.Dispose();
        this.testedPipeline.Forward(1305);
    
        // -- assert
        for (int i = 0; i < subsetSize; i++)
            this.mockedSubscriberList[i].Verify(subscriber 
                => subscriber.Notify(It.IsAny<int>()), Times.Never);
    }
}
