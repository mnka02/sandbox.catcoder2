using toolbox.Pipeline.BlockCommunication;

namespace toolbox.Pipeline.Blocks; 
/// <summary>
/// It is the first component of any pipeline. It forwards data to downstream pipeline components.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is passed to downstream pipeline components.</typeparam>
public class FirstBlock <TContext> : ABlock <TContext>, IFirstBlock <TContext> {

    private readonly IProducerBlock <TContext> producerCore;
    
    // -- constructors
    public FirstBlock (string identifier, IBlockStrategy <TContext> transformer, IProducerBlock <TContext> producer)
        : base(identifier, transformer)
        => this.producerCore = producer;
    
    // -- implemented interfaces 
    public void Link (IConsumerBlock <TContext> to)
        => this.producerCore.Link(to);

    public void Unlink (IConsumerBlock <TContext> to)
        => this.producerCore.Unlink(to);

    public void Forward (TContext context)
        => this.producerCore.Forward(context);
}