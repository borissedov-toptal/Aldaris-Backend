namespace Aldaris.ExpertSystem.Tests;

public class RuleInferenceEngineVehiclesTests
{
    private RuleInferenceEngine _engine;

    [SetUp]
    public void Setup()
    {
        _engine = new RuleInferenceEngine();

        Rule rule = new Rule(new IsClause("vehicle", "Bicycle"));
        rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
        rule.AddAntecedent(new IsClause("num_wheels", "2"));
        rule.AddAntecedent(new IsClause("motor", "no"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "Tricycle"));
        rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
        rule.AddAntecedent(new IsClause("num_wheels", "3"));
        rule.AddAntecedent(new IsClause("motor", "no"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "Motorcycle"));
        rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
        rule.AddAntecedent(new IsClause("num_wheels", "2"));
        rule.AddAntecedent(new IsClause("motor", "yes"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "Sports_Car"));
        rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("size", "medium"));
        rule.AddAntecedent(new IsClause("num_doors", "2"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "Sedan"));
        rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("size", "medium"));
        rule.AddAntecedent(new IsClause("num_doors", "4"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "MiniVan"));
        rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("size", "medium"));
        rule.AddAntecedent(new IsClause("num_doors", "3"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicle", "SUV"));
        rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("size", "large"));
        rule.AddAntecedent(new IsClause("num_doors", "4"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicleType", "cycle"));
        rule.AddAntecedent(new LessClause("num_wheels", "4"));
        _engine.AddRule(rule);

        rule = new Rule(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("num_wheels", "4"));
        rule.AddAntecedent(new IsClause("motor", "yes"));
        _engine.AddRule(rule);
    }

    [Test]
    public void TestBackwardChain()
    {
        _engine.AddFact(new IsClause("num_wheels", "4"));
        _engine.AddFact(new IsClause("motor", "yes"));
        _engine.AddFact(new IsClause("num_doors", "3"));
        _engine.AddFact(new IsClause("size", "medium"));

        Console.WriteLine("Infer: vehicle");

        List<BaseClause> unprovedConditions = new ();

        BaseClause? conclusion = _engine.InferBackward(unprovedConditions);

        Console.WriteLine("Conclusion: " + conclusion);

        Assert.NotNull(conclusion);
        Assert.That(conclusion!.Value, Is.EqualTo("MiniVan"));
    }

    [Test]
    public void TestForwardChain()
    {
        _engine.AddFact(new IsClause("num_wheels", "4"));
        _engine.AddFact(new IsClause("motor", "yes"));
        _engine.AddFact(new IsClause("num_doors", "3"));
        _engine.AddFact(new IsClause("size", "medium"));

        Console.WriteLine("before inference");
        Console.WriteLine("{0}", _engine.Facts);
        Console.WriteLine("");

        _engine.InferForward(); //forward chain

        Console.WriteLine("after inference");
        Console.WriteLine("{0}", _engine.Facts);
        Console.WriteLine("");

        Assert.That(_engine.Facts.Count, Is.EqualTo(6));
    }
}