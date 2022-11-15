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

    [Test]
    public void MultiplyWith()
    {
        Vector right = new Vector(1f, 0, 0);
        float movementSpeed = 5f;
        Vector movement = right.MultiplyWith(movementSpeed); // (5, 0, 0)
        
        Assert.That(movement, Is.EqualTo(new Vector(5,0,0)));
    }

    [Test]
    public void DivideBy()
    {
        Vector right = new Vector(1f, 0, 0);
        float fps = 50;
        Vector movement = right.DivideBy(fps); // (0.02, 0, 0)
        
        Assert.That(movement, Is.EqualTo(new Vector(0.02f, 0, 0)));
    }

    [Test]
    public void Add()
    {
        Vector position = new Vector(12, 3, -2);
        Vector movement = new Vector(2, -1, 0);
        position = position.Add(movement); // (14, 2, -2)
        
        Assert.That(movement, Is.EqualTo (new Vector(14 ,2 ,-2)));
    }
    
    [Test]
    public void Subtract()
    {
        Vector playerPosition = new Vector(12, 3, -2);
        Vector enemyPosition = new Vector(14, 1, -2);
        Vector directionToEnemy = enemyPosition.Subtract(playerPosition); // (2, -2, 0)
        
        Assert.That(playerPosition, Is.EqualTo (new Vector(2 ,-2 ,0)));
    }
}