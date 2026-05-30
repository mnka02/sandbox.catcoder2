using Moq;
using toolbox.Observables;
using toolbox.Pipeline.BlockFactories;
using toolbox.Pipeline.PipelineBuilders;
using toolbox.Pipeline.Pipelines;
using Xunit;

namespace toolbox.Pipeline.tests.integration.PipelineBuilder; 

public class PipelineBuilderTests {
    
    // assets
    private readonly IBlockFactory <string> blockFactory;
    private readonly IPipelineBuilder <string> pipelineBuilder;
    
    // -- assign
    public PipelineBuilderTests() {
        this.blockFactory = new BlockFactory <string>();
        this.pipelineBuilder = new PipelineBuilder <string>(this.blockFactory);
    }
    
    [Fact, IntegrationTest]
    public void ExpectDataRunsThrough() {
        var testedPipeline = this.pipelineBuilder
            .LinkHead("head", new ForwardStrategy())
            .LinkTail("tail", new ForwardStrategy())
            .GetPipeline();

        var input = "26092026";
        testedPipeline.Forward(input, new AssertionSubscriber(input));
    }

    [Fact, IntegrationTest]
    public void ExpectCorrectOrderOfBlocks() {
        var testedPipeline = this.pipelineBuilder
            .LinkHead("M", new ABCStrategy("M"))
            .LinkJoin("A", new ABCStrategy("A"))
            .LinkJoin("Y", new ABCStrategy("Y"))
            .LinkTail("A", new ABCStrategy("A"))
            .GetPipeline();

        testedPipeline.Forward("M", new AssertionSubscriber("MAYA"));
    }

    [Fact, IntegrationTest]
    public void ExpectSubscriberHasUnsubscribedAfterResult() {
        var testedPipeline = this.pipelineBuilder
            .LinkHead("head", new ForwardStrategy())
            .LinkTail("tail", new ForwardStrategy())
            .GetPipeline();

        var mockedSubscribers = new Mock <ISubscriber <string>>[] {
            new Mock <ISubscriber <string>>(),
            new Mock <ISubscriber <string>>()
        };

        foreach (var mockedSubscriber in mockedSubscribers)
            mockedSubscriber.Setup(subscriber => subscriber.Notify(It.IsAny <string>()));
        
        testedPipeline.Forward("Micelli", mockedSubscribers[0].Object);
        testedPipeline.Forward("Sascha", mockedSubscribers[1].Object);
        
        mockedSubscribers[0].Verify(subscriber => subscriber.Notify(It.IsAny<string>()), Times.Once);
    }
    
    

    /// <summary>
    /// Forwards incoming context to the next downstream pipeline component.
    /// </summary>
    private class ForwardStrategy : IBlockStrategy <string> {
        public string Execute (string context)
            => context;
    }
    
    /// <summary>
    /// Compares outcome of pipeline to expected value.
    /// </summary>
    private class AssertionSubscriber : ISubscriber <string> {

        private readonly string expectedPipelineOutcome;
        
        // -- constructors
        public AssertionSubscriber (string expected)
            => this.expectedPipelineOutcome = expected;
        
        // -- implemented interfaces 
        public void Notify (string context)
            => Assert.True(String.Compare(this.expectedPipelineOutcome, context, StringComparison.Ordinal) == 0);
    }
    
    /// <summary>
    /// Appends a literal to the context.
    /// </summary>
    private class ABCStrategy: IBlockStrategy <string> {

        private readonly string increment;
        
        // -- constructors
        public ABCStrategy (string increment)
            => this.increment = increment;
        
        // -- implemented interfaces 
        public string Execute (string context) {
            string transformed = context + this.increment;
            return transformed;
        }
    }
    
    
}