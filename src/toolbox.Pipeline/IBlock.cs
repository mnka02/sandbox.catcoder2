namespace toolbox.Pipeline; 

/// <summary>
/// The fundamental component of a pipeline. Defines a single processing step that transforms or
/// enriches data before forwarding it to the next stage.
/// </summary>
/// <typeparam name="TContext">Type of data</typeparam>
public interface IBlock <TContext> {
    /// <summary>
    /// Identifier of pipeline component
    /// </summary>
    public string Identifier { get; init; }
    
    /// <summary>
    /// Processes incoming data and produces the enriched output to be passed downstream.
    /// </summary>
    public IBlockStrategy <TContext> Transformer { get; init; }
}