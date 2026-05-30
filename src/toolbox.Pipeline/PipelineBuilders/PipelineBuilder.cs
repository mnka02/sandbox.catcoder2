using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.Pipelines;

namespace toolbox.Pipeline.PipelineBuilders; 

public class PipelineBuilder <TContext> : IPipelineBuilder <TContext> {

    private readonly IBlockFactory <TContext> blockFactory;
    private IFirstBlock <TContext>? firstBlock;
    private ILinkableBlock <TContext>? latestBlock;
    private ILastBlock <TContext>? lastBlock;
    
    // -- constructors
    public PipelineBuilder (IBlockFactory <TContext> blockFactory)
        => this.blockFactory = blockFactory;
    
    // -- Implemented interfaces 
    public IPipeline <TContext> GetPipeline() {
        if (this.firstBlock is null) // = pipeline is not complete
            throw new NullReferenceException("The head of the pipeline is not defined, it is uncompleted!");

        if (this.lastBlock is null) // pipeline is not complete !
            throw new NullReferenceException("The tail of the pipeline is not defined, it is uncompleted!");

        return new Pipeline <TContext>(this.firstBlock, this.lastBlock);
    }

    public void Reset() {
        this.firstBlock = null;
        this.lastBlock = null;
        this.latestBlock = null;
    }

    public IPipelineBuilder <TContext> LinkHead (string identifier, IBlockStrategy <TContext> transformer) {
        if (this.firstBlock is not null)
            throw new InvalidOperationException("The head of the pipeline is already defined!");
        this.firstBlock = this.blockFactory.ProduceFirstBlock(identifier, transformer);
        return this;
    }

    /// <summary>
    /// Defines a new middleware pipeline component. The starting point of the pipeline must be
    /// defined already.
    /// </summary>
    /// <param name="identifier">Unique name of the middleware pipeline component.</param>
    /// <param name="transformer">Business logic of the middleware pipeline component</param>
    public IPipelineBuilder <TContext> LinkJoin (string identifier, IBlockStrategy <TContext> transformer) {
        if (this.firstBlock is null)
            throw new InvalidOperationException("The head of the pipeline is undefined!");

        if (this.lastBlock is not null)
            throw new InvalidOperationException("The tail of the pipeline is already defined!");

        var newLatestBlock = this.blockFactory.ProduceJoinBlock(identifier, transformer);
        if (this.latestBlock is null) { // only head of pipeline exists
            this.latestBlock = newLatestBlock;
            this.firstBlock.Link(newLatestBlock);
        } else { // pipeline consists of more components, that just head
            this.latestBlock.Link(newLatestBlock);
            this.latestBlock = newLatestBlock;
        } return this;
    }

    /// <summary>
    /// Defines the last component of the pipeline. The starting point of the pipeline must be
    /// defined already. No middleware can be appended after the end of the pipeline is defined.
    /// </summary>
    /// <param name="identifier">Unique name of the last pipeline component.</param>
    /// <param name="transformer">Business logic of the last pipeline component</param>
    /// <returns></returns>
    public IPipelineBuilder <TContext> LinkTail (string identifier, IBlockStrategy <TContext> transformer) {
        if (this.firstBlock is null)
            throw new InvalidOperationException("The head of the pipeline is undefined!");
        
        if (this.lastBlock is not null)
            throw new InvalidOperationException("The tail of the pipeline is already defined!");

        this.lastBlock = this.blockFactory.ProduceLastBlock(identifier, transformer);
        if (this.latestBlock is null) // tail is linked to head - no middleware
            this.firstBlock.Link(this.lastBlock);
        else // tail is linked to middleware
            this.latestBlock.Link(this.lastBlock);
        return this;
    }
}