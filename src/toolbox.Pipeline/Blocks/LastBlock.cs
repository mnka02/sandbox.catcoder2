using toolbox.Observables;

namespace toolbox.Pipeline.Blocks; 
/// <summary>
/// It is the last block of any pipeline. It receives data from upstream pipeline components and produces
/// the final outcome of the pipeline. It acts as a publisher of the Observer Design Pattern. The final
/// outcome of the pipeline is passed on to the subscribers.
/// </summary>
/// <typeparam name="TContext">Datatype of incoming data and of the final result of the pipeline.</typeparam>
public class LastBlock <TContext> : ABlock <TContext>, ILastBlock <TContext> {

    private readonly IPublisher <TContext> publisherCore;

    public LastBlock (string identifier, IBlockStrategy <TContext> transformer, IPublisher <TContext> publisher)
        : base(identifier, transformer)
        => this.publisherCore = publisher;

    // -- implemented interfaces 
    public void Receive (TContext context) {
        var transformedContext = base.Transformer.Execute(context);
        this.publisherCore.NotifySubscribers(transformedContext);
    }

    public void Subscribe (ISubscriber <TContext> subscriber)
        => this.publisherCore.Subscribe(subscriber);

    public void Unsubscribe (ISubscriber <TContext> subscriber)
        => this.publisherCore.Unsubscribe(subscriber);
}