using System.Threading.Tasks.Dataflow;

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

        Vector expected = default;
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
    
     [Test]
     public void MagnitudeofVector()
     {
         Vector playerP = new Vector(3, 4, 0);
         //float magnitude = MathF.Sqrt((playerP.x * playerP.x) + (playerP.y * playerP.y) + (playerP.z * playerP.z));
         float magnitude = playerP.Magnitude;//playerPosition.Magnitude; //5

         Assert.That(magnitude, Is.EqualTo(5f));
     }
    
     [Test]
     public void SquareMagnitude()
     {
         Vector playerPosition = new Vector(3, 4, 0);
         // SquareMagnitude =  x*x + y*y + z+z
         float magnitude = playerPosition.SquareMagnitude; // 25
         
         Assert.That(magnitude, Is.EqualTo(25.0f));
     }
    
     [Test]
    
     public void DistanceTo()
     {
         Vector playerPosition = new Vector(3, 4, -2);
         Vector enemyPosition = new Vector(9, 4, 6);
         float distance = playerPosition.DistanceTo(enemyPosition);
         
         Assert.That(distance, Is.EqualTo(10f));
     }
    
     [Test]
     public void SquareDistanceTo()
     {
         Vector playerPosition = new Vector(3, 4, -2);
         Vector enemyPosition = new Vector(9, 4, 6);
         float distance = playerPosition.SquareDistanceTo(enemyPosition); // 100
                 
         Assert.That(distance, Is.EqualTo(playerPosition.SquareDistanceTo(enemyPosition)));
     }

     /*[Test]
     public void Normalized()
     {
         Vector enemyDisplacement = new Vector(4, 0, 3);
         Vector enemyDirection = enemyDisplacement.Normalized; // (0.8, 0. 0.6)
         
         Assert.That(enemyDirection, Is.EqualTo (0.8, 0.0, 0.6));
     }
     [Test]
     public void IsUnitVector()
     {
         bool isUnitVector = new Vector(3, -1, 0).IsUnitVector; // false
         isUnitVector = new Vector(-1, 0, 0).IsUnitVector; // true  
     }*/
     
     [Test]
     public void Dot()
     {
     Vector a = new Vector(1, -2, 4);
     Vector b = new Vector(-2, 3, 0.5f);
     float dot = a.Dot(b); // -6
     }
     
     [Test]
     public void AngleBetweenRad()
     {
     Vector a = new Vector(5, 0, 0);
     Vector b = new Vector(3, 3, 0);
     float angle = Vector.AngleBetweenRad(a, b); // arccos(0.5·√2) ≈ 0.785398163
     }
     
     [Test]
     public void AngleBetweenDeg()
     {
     Vector a = new Vector(5, 0, 0);
     Vector b = new Vector(3, 3, 0);
     float angle = Vector.AngleBetweenDeg(a, b); // 45
     }
     
     /*[Test]
     public void Cross()
     {
     Vector right = new Vector(1, 0, 0);
     Vector up = new Vector(0, 1, 0);
     Vector forward = right.Cross(up); // (0, 0, 1)
     }*/
}