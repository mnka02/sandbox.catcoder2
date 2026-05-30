using toolbox.Observables;

namespace toolbox.Pipeline.Pipelines; 

/// <summary>
/// A pipeline.
/// </summary>
/// <typeparam name="TContext">Datatype of the data, that is forwarded through the pipeline.</typeparam>
public class Pipeline <TContext> : IPipeline <TContext>, IDisposable {

    private readonly IFirstBlock <TContext> firstBlock;
    private readonly ILastBlock <TContext> lastBlock;
    
    // -- constructors
    public Pipeline (IFirstBlock <TContext> firstBlock, ILastBlock <TContext> lastBlock) {
        this.firstBlock = firstBlock;
        this.lastBlock = lastBlock;
    }

    /*TODO: public Pipeline (
        IFirstBlock <TContext> firstBlock,
        ILastBlock <TContext> lastBlock,
        ICollection <ISubscriber <TContext>> subscribers)
        : this(firstBlock, lastBlock) {
        foreach (var subscriber in subscribers)
            this.lastBlock.Subscribe(subscriber);
    }*/

    /// <summary>
    /// Starts the processing of the input by the pipeline. The outcome of the pipeline is issued to
    /// the subscribers, that have subscribed beforehand. 
    /// </summary>
    /// <param name="context">Input, that is send into the pipeline.</param>
    public void Forward (TContext context)
        // TODO: what if no one has subscribed yet?
        => this.firstBlock.Forward(context);
    
    // -- implemented interfaces
    /// <summary>
    /// Starts the processing of the input by the pipeline.
    /// </summary>
    /// <param name="context">Input, that is send into the pipeline.</param>
    /// <param name="subscriber">Subscribes to the outcome of the pipeline. It only subscribes to one
    /// outcome of the pipeline. It unsubscribes after receiving the outcome.</param>
    public void Forward (TContext context, ISubscriber <TContext> subscriber) {
        this.lastBlock.Subscribe(subscriber);
        this.firstBlock.Forward(context);
        /* the injected subscriber only subscribe to the result of the injected context, therefore
        it subscribes to the last block, before the context is forwarded and unsubscribe after the 
        pipeline ran through */
        this.lastBlock.Unsubscribe(subscriber);
    }

    public void Dispose()
    // TODO: implement IDisposable for Publisher.cs
    // TODO: empty list of subscribers, for last block
        => throw new NotImplementedException();
}