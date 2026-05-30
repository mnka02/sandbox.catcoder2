using toolbox.Pipeline.BlockCommunication;

namespace toolbox.Pipeline.Blocks; 
/// <summary>
/// It is the middleware of any pipeline. It receives data from upstream pipeline components and
/// it publishes data to downstream pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of incoming and outgoing data.</typeparam>
public class JoinBlock <TContext> : ABlock <TContext>, IJoinBlock <TContext> {

    private readonly IProducerBlock <TContext> producerCore;
    
    // -- constructors
    public JoinBlock (string identifier, IBlockStrategy <TContext> transformer, IProducerBlock <TContext> producer)
        : base(identifier, transformer)
        => this.producerCore = producer;

    // -- implemented interfaces
    public void Receive (TContext context) {
        var transformedContext = base.Transformer.Execute(context);
        this.producerCore.Forward(transformedContext);
    }

    public void Link (IConsumerBlock <TContext> to)
        => this.producerCore.Link(to);

    public void Unlink (IConsumerBlock <TContext> to)
        => this.producerCore.Unlink(to);
}