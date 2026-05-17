using toolbox.Observables;

namespace toolbox.Pipeline.Communications.Observer; 

/// <summary>
/// Provides an adaption from <see cref="IConsumerBlock{TContext}"/> to <see cref="ISubscriber{TContext}"/>.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface IConsumerBlockAdapterFactory <TContext> {

    /// <summary>
    /// Provides an adapter based on given consumer block.
    /// </summary>
    /// <param name="consumerBlock">Based of adapter.</param>
    /// <returns>The adapter.</returns>
    public ISubscriber <TContext> ProduceAdapter (IConsumerBlock <TContext> consumerBlock);
}