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
        expected.x = x;
        expected.y = y;
        expected.z = z;
        
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void ZeroPropertyReturnZeroVector()
    {
        Vector expected = new Vector(0F, 0F, 0F);

        Assert.That(Vector.Zero, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(1F, 0F, 0F)]
    [TestCase(-12F, 0.001F, -100.5F)]
    public void NegatePropertyReturnNegatedVector(float x, float y, float z)
    {
        Vector original = new Vector(x, y, z);

        Assert.That(original.Inverse(), Is.EqualTo(new  Vector(-x,-y,-z)));
    }
    [Test]
    public void InverseDoesNotAffectOriginalVector()
    {
        Vector original = new Vector(12f, 11f, 10f);
        Vector expected = new Vector(12f, 11f, 10f);
        original.Inverse();

        Assert.That(original, Is.EqualTo(expected));
    }
}