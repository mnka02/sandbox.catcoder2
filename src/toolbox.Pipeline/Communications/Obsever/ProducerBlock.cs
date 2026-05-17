using toolbox.Observables;

namespace toolbox.Pipeline.Communications.Observer; 

/// <summary>
/// Represents a pipeline component that can produce and forward data to downstream consumers.
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public class ProducerBlock <TContext> : IProducerBlock <TContext> {

    private readonly IPublisher <TContext> publisherCore;
    private readonly IConsumerBlockAdapterFactory <TContext> factory;
    
    // -- constructors
    public ProducerBlock() {
        this.publisherCore = new Publisher <TContext>();
        this.factory = new ConsumerBlockAdapterFactory <TContext>();
    }
    
    // -- implemented interfaces 
    public void Link (IConsumerBlock <TContext> to)
        => this.publisherCore.Subscribe(this.factory.ProduceAdapter(to));

    public void Unlink (IConsumerBlock <TContext> to)
        => this.publisherCore.Unsubscribe(this.factory.ProduceAdapter(to));

    public void Forward (TContext context)
        => this.publisherCore.NotifySubscribers(context);
}