using NUnit.Framework;

namespace Maths.Test;

public class VectorTests
{

    [Test]
    [TestCase()]
    public void ConstructorAssignsAllComponents()
    {
        Vector actual = new Vector(3, 2, 5);

        Vector expected;
        expected.X = 3;
        expected.Y = 2;
        expected.Z = 5;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NegatePropertyInverseVector(float x, float y, float z)
    {
        Vector right = new Vector(1f, 0, 0);
        Vector left = right.Inverse();
    }
}