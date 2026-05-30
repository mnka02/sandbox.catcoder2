namespace toolbox.Pipeline; 

/// <summary>
/// The most basic component of any pipeline. Defines a single processing step that transforms or
/// enriches data before forwarding it to the next stage.
/// <typeparam name="TContext">Datatype of forwarded data</typeparam>
/// </summary>
public interface IBlock <TContext> {
    /// <summary>
    /// Unique name of this pipeline component. It will be used for logging.
    /// </summary>
    public string Identifier { get; init; }
    
    /// <summary>
    /// Processes incoming data and produces the dataset, that is passed on the next block in the
    /// pipeline.
    /// </summary>
    public IBlockStrategy <TContext> Transformer { get; init; }
}