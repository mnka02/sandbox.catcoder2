using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication.Observer;

namespace toolbox.Pipeline.BlockCommunication.Blocks; 

// TODO: #10
/// <summary>
/// It is a pipeline component, that is able to connect to other downstream pipeline components and
/// issue data to those pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is issued to downstream pipeline components</typeparam>
public class ProducerBlock <TContext> : IProducerBlock <TContext> {

    private readonly IPublisher <TContext> publisherCore;
    private readonly ISubscriberAdapterFactory <TContext> factory;
    
    // -- constructors
    public ProducerBlock (IPublisher <TContext> publisher, ISubscriberAdapterFactory <TContext> factory) {
        this.publisherCore = publisher;
        this.factory = factory;
    }
    
    // -- implemented interfaces 
    public void Link (IConsumerBlock <TContext> to)
        => this.publisherCore.Subscribe(this.factory.ProduceAdapter(to));

    public void Unlink (IConsumerBlock <TContext> to)
        => this.publisherCore.Unsubscribe(this.factory.ProduceAdapter(to));

    public void Forward (TContext context)
        => this.publisherCore.NotifySubscribers(context);
}