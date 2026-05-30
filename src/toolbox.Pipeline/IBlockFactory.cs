namespace toolbox.Pipeline;
/// <summary>
/// Implementation of the Abstract Factory Design Pattern.
/// It produces pipeline components of the same family.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is forwarded through the pipeline.</typeparam>
public interface IBlockFactory <TContext> {
    
    /// <summary>
    /// Creates an instance of <see cref="IFirstBlock{TContext}"/>
    /// </summary>
    /// <param name="identifier">Unique name of the pipeline component.</param>
    /// <param name="transformer">Business logic of the pipeline component.</param>
    /// <returns>Instance of <see cref="IFirstBlock{TContext}"/>, based on given parameters.</returns>
    public IFirstBlock <TContext> ProduceFirstBlock (string identifier, IBlockStrategy <TContext> transformer);
    /// <summary>
    /// Creates an instance of <see cref="IJoinBlock{TContext}"/>
    /// </summary>
    /// <param name="identifier">Unique name of the pipeline component.</param>
    /// <param name="transformer">Business logic of the pipeline component.</param>
    /// <returns>Instance of <see cref="IJoinBlock{TContext}"/>, based on given parameters.</returns>
    public IJoinBlock <TContext> ProduceJoinBlock (string identifier, IBlockStrategy <TContext> transformer);
    /// <summary>
    /// Creates an instance of <see cref="ILastBlock{TContext}"/>
    /// </summary>
    /// <param name="identifier">Unique name of the pipeline component.</param>
    /// <param name="transformer">Business logic of the pipeline component.</param>
    /// <returns>Instance of <see cref="ILastBlock{TContext}"/>, based on given parameters.</returns>
    public ILastBlock <TContext> ProduceLastBlock (string identifier, IBlockStrategy <TContext> transformer);
}