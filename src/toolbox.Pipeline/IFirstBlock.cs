using toolbox.Pipeline.Communications;

namespace toolbox.Pipeline; 

/// <summary>
/// First component of any pipeline. It publishes data to other components of the pipeline. 
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public interface IFirstBlock <TContext>
    : IBlock <TContext>, IProducerBlock <TContext> { }