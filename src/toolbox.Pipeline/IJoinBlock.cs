using toolbox.Pipeline.BlockCommunication;

namespace toolbox.Pipeline; 

/// <summary>
/// It is the middleware of any pipeline. It receives data from upstream pipeline components and
/// it publishes data to downstream pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of incoming and outgoing data.</typeparam>
public interface IJoinBlock <TContext> 
    : IBlock <TContext>, IConsumerBlock <TContext>, ILinkableBlock <TContext> { }