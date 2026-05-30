using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication;

namespace toolbox.Pipeline; 

/// <summary>
/// It is the last block of any pipeline. It receives data from upstream pipeline components and produces
/// the final outcome of the pipeline. It acts as a publisher of the Observer Design Pattern. The final
/// outcome of the pipeline is passed on to the subscribers.
/// </summary>
/// <typeparam name="TContext">Datatype of incoming data and of the final result of the pipeline.</typeparam>
public interface ILastBlock <TContext> 
    : IBlock <TContext>, IConsumerBlock <TContext>, ISubscribable <TContext> { }