using toolbox.Observables;
using toolbox.Pipeline.BlockCommunication;
using toolbox.Pipeline.BlockCommunication.BlockFactories;
using toolbox.Pipeline.Blocks;

namespace toolbox.Pipeline.BlockFactories;
/// <summary>
/// It produces pipeline components of the same family.
/// </summary>
/// <typeparam name="TContext">Datatype of data, that is forwarded through the pipeline.</typeparam>
public class BlockFactory <TContext> : IBlockFactory <TContext> {

    private readonly IProducerBlockFactory <TContext> producerBlockFactory
        = new ProducerBlockFactory <TContext>();
    
    // -- implemented interfaces 
    public IFirstBlock <TContext> ProduceFirstBlock (string identifier, IBlockStrategy <TContext> transformer)
        => new FirstBlock <TContext>(identifier, transformer, this.producerBlockFactory.CreateProducerBlock());

    public IJoinBlock <TContext> ProduceJoinBlock (string identifier, IBlockStrategy <TContext> transformer)
        => new JoinBlock <TContext>(identifier, transformer, this.producerBlockFactory.CreateProducerBlock());

    public ILastBlock <TContext> ProduceLastBlock (string identifier, IBlockStrategy <TContext> transformer)
    // TODO: maybe PublisherFactory ??
        => new LastBlock <TContext>(identifier, transformer, new Publisher <TContext>());
}