namespace Maths.Test;

public class VectorTests
{
    [Test]
    [TestCase(3F, 2F, 5F)]
    [TestCase(-1F, 0F, 12F)]
    public void ConstructorAssignAllComponents(float x, float y, float z)
    {
        Vector actual = new Vector(x, y, z);

        Vector expected;
        expected.X = x;
        expected.Y = y;
        expected.Z = z;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}