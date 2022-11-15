namespace Maths.Test;

public class VectorTests
{
    [Test]
    public void ConstructorTest()
    {
        Vector v = new Vector(3, 2, 1);
        Assert.AreEqual(3, v.X);
        Assert.AreEqual(2, v.Y);
        Assert.AreEqual(1, v.Z);
    }
}