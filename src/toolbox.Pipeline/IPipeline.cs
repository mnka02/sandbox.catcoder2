using toolbox.Observables;

namespace toolbox.Pipeline; 

/// <summary>
/// The most basic form of a pipeline.
/// </summary>
/// <typeparam name="TContext">Datatype of the data, that is forwarded through the pipeline.</typeparam>
public interface IPipeline <TContext> {
    /// <summary>
    /// Starts the processing of the input by the pipeline.
    /// </summary>
    /// <param name="context">Input, that is send into the pipeline.</param>
    /// <param name="subscriber">Destination of every outcome of the pipeline.</param>
    public void Forward (TContext context, ISubscriber <TContext> subscriber);
}