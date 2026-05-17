using toolbox.Observables;

namespace toolbox.Pipeline.Communications.Observer;

/// <summary>
/// Adapter from <see cref="IConsumerBlock{TContext}"/> to <see cref="ISubscriber{TContext}"/> 
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public class SubscriberBlockAdapter <TContext> : ISubscriber <TContext> {

    private readonly IConsumerBlock <TContext> adaptee;
    
    // -- constructors
    public SubscriberBlockAdapter (IConsumerBlock <TContext> consumerBlock)
        => this.adaptee = consumerBlock;
    
    // -- implemented interfaces 
    public void Notify (TContext context)
        => this.adaptee.Receive(context);
}