using System.Runtime.CompilerServices;
using toolbox.Observables;

namespace toolbox.Pipeline.Communications.Observer; 

public class ConsumerBlockAdapterFactory <TContext> : IConsumerBlockAdapterFactory <TContext> {

    private readonly ConditionalWeakTable <IConsumerBlock <TContext>, ISubscriber <TContext>> adapterMap;
    
    // -- constructors
    public ConsumerBlockAdapterFactory()
        => this.adapterMap = new();
    
    // -- implemented interfaces
    /// <summary>
    /// Provides singleton production of adapters, based on given Consumerblock.
    /// </summary>
    /// <param name="consumerBlock">Consumerblock to adapt.</param>
    /// <returns>The singleton adapter of the given consumer block.</returns>
    // TODO: #6 
    public ISubscriber <TContext> ProduceAdapter (IConsumerBlock <TContext> consumerBlock)
        // to ensure singleton every produced adapter is documented. In case an adapter already
        // exists for a specific Consumerblock, it will be returned, instead of creating a new one.
        => this.adapterMap.GetValue(
            consumerBlock, 
            adaptee => new SubscriberBlockAdapter <TContext>(adaptee));
}