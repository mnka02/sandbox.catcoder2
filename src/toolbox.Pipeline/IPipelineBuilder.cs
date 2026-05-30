namespace toolbox.Pipeline; 

/// <summary>
/// Implementation of the Builder Design Pattern.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is forwarded by each pipeline component.</typeparam>
public interface IPipelineBuilder <TContext> {
    /// <summary>
    /// Get the built pipeline.
    /// </summary>
    /// <returns>The built pipeline, according to the called builder methods beforehand.</returns>
    public IPipeline <TContext> GetPipeline();
    
    /// <summary>
    /// Reset the pipeline. The called builder methods beforehand become irrelevant.
    /// </summary>
    public void Reset();
    
    /// <summary>
    /// Defines the first component of the pipeline.
    /// </summary>
    /// <param name="identifier">Unique name of the first pipeline component</param>
    /// <param name="transformer">Business logic of the first pipeline component</param>
    /// <returns></returns>
    public IPipelineBuilder <TContext> LinkHead (string identifier, IBlockStrategy <TContext> transformer);
    /// <summary>
    /// Defines a new middleware pipeline component.
    /// </summary>
    /// <param name="identifier">Unique name of the middleware pipeline component.</param>
    /// <param name="transformer">Business logic of the middleware pipeline component</param>
    /// <returns></returns>
    public IPipelineBuilder <TContext> LinkJoin (string identifier, IBlockStrategy <TContext> transformer);
    /// <summary>
    /// Defines the last component of the pipeline.
    /// </summary>
    /// <param name="identifier">Unique name of the last pipeline component.</param>
    /// <param name="transformer">Business logic of the last pipeline component</param>
    /// <returns></returns>
    public IPipelineBuilder <TContext> LinkTail (string identifier, IBlockStrategy <TContext> transformer);
}