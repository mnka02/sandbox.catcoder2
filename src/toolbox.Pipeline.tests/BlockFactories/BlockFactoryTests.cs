using Moq;
using toolbox.Pipeline.BlockFactories;
using Xunit;

namespace toolbox.Pipeline.tests.BlockFactories; 

public class BlockFactoryTests {

    private readonly BlockFactory <int> testedFactory;
    // assets
    private readonly string blockIdentifier = "Gabriele";
    private readonly Mock <IBlockStrategy <int>> mockedBlockStrategy;
    public static IEnumerable <object[]> BlockListData
        = new List <object[]>() {
            new object[] { nameof(IFirstBlock <int>) },
            new object[] { nameof(IJoinBlock <int>) },
            new object[] { nameof(ILastBlock <int>) },
        };
    
    // assign
    public BlockFactoryTests() {
        this.mockedBlockStrategy = new Mock <IBlockStrategy <int>>();
        this.testedFactory = new BlockFactory <int>();
    }
    
    // act
    [Theory]
    [MemberData(nameof(BlockListData))]
    public void ExpectIdentifierAsInjected(string blockType)
    // assert
        => Assert.Equal(this.blockIdentifier, this.ProduceBlock(blockType).Identifier);

    [Theory]
    [MemberData(nameof(BlockListData))]
    public void ExpectStrategyAsInjected(string blockType)
    // assert
        => Assert.Equal(this.mockedBlockStrategy.Object, this.ProduceBlock(blockType).Transformer);

    private IBlock <int> ProduceBlock (string blockType) {
        IBlock <int> factoredBlock;
        switch (blockType) {
            case nameof(IFirstBlock <int>):
                factoredBlock = this.testedFactory.ProduceFirstBlock(
                    this.blockIdentifier, this.mockedBlockStrategy.Object);
                break;
            case nameof(IJoinBlock <int>):
                factoredBlock = this.testedFactory.ProduceJoinBlock(
                    this.blockIdentifier, this.mockedBlockStrategy.Object);
                break;
            case nameof(ILastBlock <int>):
                factoredBlock = this.testedFactory.ProduceLastBlock(
                    this.blockIdentifier, this.mockedBlockStrategy.Object); 
                break;
            default:
                throw new ArgumentException($"Unknown block type: {blockType}");
        } return factoredBlock;
    }
}