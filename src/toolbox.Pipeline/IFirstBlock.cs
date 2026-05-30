using toolbox.Pipeline.BlockCommunication;

namespace toolbox.Pipeline; 

/// <summary>
/// It is the first component of any pipeline. It forwards data to downstream pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is passed to downstream pipeline components.</typeparam>
public interface IFirstBlock <TContext> 
    : IBlock <TContext>, IProducerBlock <TContext> { }