namespace Aldaris.ExpertSystem.Tests;

public class ClauseTests
{
    [Test]
    public void TestNewClause()
    {
        BaseClause c = new IsClause("name", "Toptal");
        Assert.That(c.Variable, Is.EqualTo("name"));
        Assert.That(c.Value, Is.EqualTo("Toptal"));
        Assert.That(c.Condition, Is.EqualTo("="));
    }
}