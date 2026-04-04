using Xunit;

namespace toolbox.Duplicatables.tests;
public class DuplicatablesTests {

    private class Rectangle : IDuplicatable <Rectangle> {
        
        public Double Length { get; init; }
        public Double Width { get; init; }
        
        // -- constructors
        public Rectangle (double length, double width) {
            this.Length = length;
            this.Width = width;
        }

        public Rectangle (Rectangle rectangle)
            => new Rectangle(rectangle.Length, rectangle.Width);
        
        // -- implemented interfaces 
        public Rectangle Copy()
            => new(this);
    }

    [Fact]
    public void ExpectDeepCopy() {
        // -- arrange/act
        Rectangle object2copy = new Rectangle(11, 07);
        Rectangle copiedObject = object2copy.Copy();
        // -- assert
        Assert.False(object2copy.Equals(copiedObject));
    }
}