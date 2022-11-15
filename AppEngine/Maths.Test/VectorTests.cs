namespace Maths.Test;

public class VectorTests
{
    [Test]
    [TestCase(3F, 2F, 5F)]
    [TestCase(-1F, 0F, 12F)]
    [TestCase(-2F, 1.5F, 0.001F)]
    public void ConstructorAssignAllComponents(float x, float y, float z)
    {
        Vector actual = new Vector(x, y, z);

        Vector expected;
        expected.X = x;
        expected.Y = y;
        expected.Z = z;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ZeroPropertyReturnZeroVector()
    {
        Vector expected = new Vector(0F, 0F, 0F);

        Assert.That(Vector.Zero, Is.EqualTo(expected));
    }

    [Test]
    public void NegatePropertyReturnNegatedVector()
    {
        Vector right = new Vector(1f, 0, 0);
        Vector left = right.Inverse;
        
        Assert.That(left, Is.EqualTo(new  Vector(-1,0,0)));
    }
}