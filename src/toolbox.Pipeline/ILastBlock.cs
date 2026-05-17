using toolbox.Pipeline.Communications;

namespace toolbox.Pipeline;

/// <summary>
/// Last Block of any pipeline. It receives data from other block components of the pipeline.
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public interface ILastBlock <TContext> : 
    IBlock <TContext>, IConsumerBlock <TContext> { }