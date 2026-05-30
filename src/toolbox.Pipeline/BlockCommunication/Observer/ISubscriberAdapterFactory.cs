using toolbox.Observables;

namespace toolbox.Pipeline.BlockCommunication.Observer; 

/// <summary>
/// It is an implementation of the Factory Design Pattern.
/// It produces adaptions from <see cref="IConsumerBlock{TContext}"/> to <see cref="ISubscriber{TContext}"/>
/// </summary>
/// <typeparam name="TContext">Datatype of data, that the consumer component of a pipeline receives.</typeparam>
public interface ISubscriberAdapterFactory <TContext> {
    /// <summary>
    /// Produces an adapter, for the given consumer component.
    /// </summary>
    /// <param name="consumerBlock">Base of the produced adapter.</param>
    /// <returns>The adapter, based on given consumer component.</returns>
    public ISubscriber <TContext> ProduceAdapter (IConsumerBlock <TContext> consumerBlock);
}