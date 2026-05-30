namespace toolbox.Pipeline.BlockCommunication; 

/// <summary>
/// The interfaces restricts the communication interface of a pipeline block.
/// It is a pipeline component, that is able to connect to other downstream pipeline components and
/// issue data to those pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is issued to downstream pipeline components</typeparam>
public interface IProducerBlock <TContext> : ILinkableBlock <TContext> {
    /// <summary>
    /// It forwards data to downstream pipeline components.
    /// </summary>
    /// <param name="context">It is forwarded to the downstream pipeline components.</param>
    public void Forward (TContext context);
}