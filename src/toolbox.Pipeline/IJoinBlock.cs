using toolbox.Pipeline.Communications;

namespace toolbox.Pipeline; 

/// <summary>
/// Middleware of any pipeline. It receives data from other block components of the pipline
/// and it issues data to other block components.
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public interface IJoinBlock <TContext> : 
    IBlock <TContext>, IConsumerBlock <TContext>, IProducerBlock <TContext> { }