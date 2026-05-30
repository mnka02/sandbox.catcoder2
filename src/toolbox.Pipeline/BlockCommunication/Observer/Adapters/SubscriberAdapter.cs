using toolbox.Observables;

namespace toolbox.Pipeline.BlockCommunication.Observer.Adapters; 

// TODO: #11
/// <summary>
/// It is an implementation of the Adapter Design Pattern.
/// Adapter from <see cref="IConsumerBlock{TContext}"/> to <see cref="ISubscriber{TContext}"/>.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class SubscriberAdapter <TContext> : ISubscriber <TContext> {

    private readonly IConsumerBlock <TContext> adaptedConsumerBlock;
    
    // -- constructors
    public SubscriberAdapter (IConsumerBlock <TContext> consumerBlock)
        => this.adaptedConsumerBlock = consumerBlock;
    
    // -- implemented interfaces 
    public void Notify (TContext context)
        => this.adaptedConsumerBlock.Receive(context);
}