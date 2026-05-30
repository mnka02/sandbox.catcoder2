namespace toolbox.Pipeline; 

// TODO: #7 
/// <summary>
/// The business logic of a pipeline component. It is an implementation of the Strategy Design Pattern.
/// <see href="https://refactoring.guru/design-patterns/strategy"/>
/// <typeparam name="TContext">Datatype of forwarded data.</typeparam>
/// </summary>
public interface IBlockStrategy <TContext> {
    /// <summary>
    /// Enriches or modifies received data. 
    /// </summary>
    /// <param name="context">The data, issued by the block before, in the pipeline.</param>
    /// <returns>The enriched or modified data.</returns>
    public TContext Execute (TContext context);
}