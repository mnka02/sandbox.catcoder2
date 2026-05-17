namespace toolbox.Pipeline.Communications; 

/// <summary>
/// Represents a pipeline component that can produce and forward data to downstream consumers.
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public interface IProducerBlock <TContext> {

    /// <summary>
    /// Subscribes a consumer block to receive forwarded data from this component.
    /// </summary>
    /// <param name="to">Receives the data.</param>
    public void Link (IConsumerBlock <TContext> to);
    /// <summary>
    /// Unsubscribes a consumer block from receiving forwarded data from this component.
    /// </summary>
    /// <param name="to">Does not receive data anymore.</param>
    public void Unlink (IConsumerBlock <TContext> to);
    /// <summary>
    /// Forwards the processed data to all linked consumer blocks.
    /// </summary>
    /// <param name="context">Is issued to every subscriber.</param>
    public void Forward (TContext context);
}