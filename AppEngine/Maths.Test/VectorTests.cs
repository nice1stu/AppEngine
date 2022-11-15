namespace Maths.Test;

public class VectorTests
{
    [Test]
    public void ConstructorAssignAllComponents()
    {
        Vector actual = new Vector(3, 2, 5);

        Vector expected;
        expected.X = 3;
        expected.Y = 2;
        expected.Z = 5;
        
        Assert.AreEqual(expected, actual);
    }
}