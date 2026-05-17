namespace toolbox.Pipeline.Communications; 

/// <summary>
/// Represents a pipeline component that can receive data from an upstream source.
/// </summary>
/// <typeparam name="TContext">Type of issued data.</typeparam>
public interface IConsumerBlock <TContext> {

    /// <summary>
    /// Receives incoming data from an upstream pipeline component.
    /// </summary>
    /// <param name="context">Issued data.</param>
    public void Receive (TContext context);
}