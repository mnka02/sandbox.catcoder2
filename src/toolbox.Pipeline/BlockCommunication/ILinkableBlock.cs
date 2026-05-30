namespace toolbox.Pipeline.BlockCommunication; 

/// <summary>
/// The interfaces restricts the communication interface of a pipeline block.
/// It is a pipeline component, that is ables to connect to other downstream pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is issued to downstream pipeline components</typeparam>
public interface ILinkableBlock <TContext> {
    /// <summary>
    /// It defines the next downstream pipeline component. The pipeline component, that is linked
    /// receives data.
    /// </summary>
    /// <param name="to">The next downstream pipeline component</param>
    public void Link (IConsumerBlock <TContext> to);

    /// <summary>
    /// It cuts the link between the these two pipeline components. The downstream pipeline component
    /// does not receive any data anymore.
    /// </summary>
    /// <param name="to">The pipeline component at the other end of the link. This pipeline component
    /// does not receive data anymore.</param>
    public void Unlink (IConsumerBlock <TContext> to);
}