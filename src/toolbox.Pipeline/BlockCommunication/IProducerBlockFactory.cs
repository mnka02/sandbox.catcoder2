namespace toolbox.Pipeline.BlockCommunication; 

// TODO: #10
/// <summary>
/// Implementation of the Factory Design Pattern.
/// It produces instances of <see cref="IProducerBlock{TContext}"/>.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is issued to downstream pipeline components</typeparam>
public interface IProducerBlockFactory <TContext> {
    /// <summary>
    /// It creates an instance of <see cref="IProducerBlock{TContext}"/>.
    /// </summary>
    /// <returns>Instance of <see cref="IProducerBlock{TContext}"/></returns>
    public IProducerBlock <TContext> CreateProducerBlock();
}