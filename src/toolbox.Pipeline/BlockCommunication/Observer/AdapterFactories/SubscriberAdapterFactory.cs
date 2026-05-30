using System.Runtime.CompilerServices;
using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication.Observer.Adapters;

namespace toolbox.Pipeline.BlockCommunication.Observer.AdaptersFactories; 

/// <summary>
/// Adapter from <see cref="IConsumerBlock{TContext}"/> to <see cref="ISubscriber{TContext}"/>.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class SubscriberAdapterFactory <TContext> : ISubscriberAdapterFactory <TContext> {

    private readonly ConditionalWeakTable
        <IConsumerBlock <TContext>, ISubscriber <TContext>> adapterMap = new();
    
    // -- methods
    private SubscriberAdapter <TContext> CreateAdapter (IConsumerBlock <TContext> consumerBlock)
        => new SubscriberAdapter <TContext>(consumerBlock);
    
    // -- implemented interfaces
    // TODO: #6
    /// <summary>
    /// Produces an singleton adapter, for the given consumer component.
    /// </summary>
    /// <param name="consumerBlock">Base of the produced adapter.</param>
    /// <returns>The adapter, based on given consumer component.</returns>
    public ISubscriber <TContext> ProduceAdapter (IConsumerBlock <TContext> consumerBlock) {
        var adapter = this.adapterMap.GetValue(
            /* to ensure singleton every produced adapter is documented. In case an adapter already
            exists for a specific Consumerblock, it will be returned, instead of creating a new one. */
            consumerBlock, adaptedConsumerBlock => this.CreateAdapter(consumerBlock));
        return adapter;
    }
}