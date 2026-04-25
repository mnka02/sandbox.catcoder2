using Xunit;
using toolbox.DiscriminatedUnions;

namespace toolbox.DiscriminatedUnions.tests; 

public class DiscriminatedUnionTests {
    
    // -- arrange
    private bool? isStatedToValue;

    private void HandleValue (string value)
        => this.isStatedToValue = true;

    private void HandleError (Exception error)
        => this.isStatedToValue = false;
        
    public static IEnumerable <object[]> ExpectStateOfUnionAccordingToInputData =>
        new List <object[]>() {
            new object[] {new DiscriminatedUnion <string, Exception>("Hello, there!"), true},
            new object[] {new DiscriminatedUnion <string, Exception>(new Exception("Hello, there!")), false}
        };
    
    // -- act
    [Theory]
    [MemberData(nameof(ExpectStateOfUnionAccordingToInputData))]
    public void ExpectStateOfUnionAccordingToInput (DiscriminatedUnion <string, Exception> testUnion, bool isValue) {
        testUnion
            .Then(this.HandleError)
            .Then(this.HandleValue);
        // -- assert
        Assert.True(this.isStatedToValue == isValue);
    }
    
    [Fact]
    public void ExpectImplicitConversionOfInput2ValueStateUnion() {
        // -- assert 
        DiscriminatedUnion <string, Exception> testUnion = "Hello, there!";
    }

    [Fact]
    public void ExpectImplicitConversionOfInput2ErrorStatedUnion() {
        // -- assert
        DiscriminatedUnion <string, Exception> testUnion = new Exception("Hello, there!");
    }
}