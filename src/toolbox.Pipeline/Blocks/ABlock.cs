namespace toolbox.Pipeline.Blocks; 

/// <summary>
/// The most basic component of any pipeline. Defines a single processing step that transforms or
/// enriches data before forwarding it to the next stage.
/// </summary>
/// <typeparam name="TContext">Datatype of forwarded data</typeparam>
public abstract class ABlock <TContext> : IBlock <TContext> {

    // -- constructor
    protected ABlock (string identifier, IBlockStrategy <TContext> transformer) {
        this.Identifier = identifier;
        this.Transformer = transformer;
    }
    
    // -- implemented interfaces 
    public string Identifier { get; init; }
    public IBlockStrategy <TContext> Transformer { get; init; }
}