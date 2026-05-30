using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication.Blocks;
using toolbox.Pipeline.BlockCommunication.Observer;
using toolbox.Pipeline.BlockCommunication.Observer.AdaptersFactories;

namespace toolbox.Pipeline.BlockCommunication.BlockFactories; 

/// <summary>
/// It produces instances of <see cref="ProducerBlock{TContext}"/>
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is issued to downstream pipeline components,
/// by the ProducerBlock.</typeparam>
public class ProducerBlockFactory <TContext> : IProducerBlockFactory <TContext> {
    
    private readonly ISubscriberAdapterFactory <TContext> subscriberAdapterFactory
        = new SubscriberAdapterFactory <TContext>();
    
    // -- implemented interfaces 
    /* TODO: maybe introduce IPublisherFactory ?? */
    /* TODO: [maybe Bugfix] each produced ProducerBlock has the SAME PublisherCore, maybe the problem
    of blocks subscribing linking to itself. */
    public IProducerBlock <TContext> CreateProducerBlock()
        => new ProducerBlock <TContext>(new Publisher <TContext>(), this.subscriberAdapterFactory);
}