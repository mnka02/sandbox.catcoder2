namespace toolbox.Pipeline.BlockCommunication; 

/// <summary>
/// The interfaces restricts the communication interface of a pipeline block. 
/// It is a pipeline component, that is able to receive data from an upstream block source.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface IConsumerBlock <TContext> {
    /// <summary>
    /// It handles incoming data from an upstream pipeline component.
    /// </summary>
    /// <param name="context">Datatype of issued data, from the upstream pipeline component</param>
    public void Receive (TContext context);
}