namespace Aldaris.ExpertSystem.Tests;

public class RuleInferenceEngineTests
{
    private RuleInferenceEngine _engine;

    [SetUp]
    public void Setup()
    {
        _engine = new RuleInferenceEngine();

        Rule rule = new Rule("C#", new IsClause("programming_language", "C#"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("release_year", "2000"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "yes"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        _engine.AddRule(rule);

        rule = new Rule("Swift",new IsClause("programming_language", "Swift"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("release_year", "2014"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        _engine.AddRule(rule);

        rule = new Rule("Python",new IsClause("programming_language", "Python"));
        rule.AddAntecedent(new IsClause("compilation", "interpreted"));
        // rule.AddAntecedent(new IsClause("release_year", "1991"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        _engine.AddRule(rule);

        rule = new Rule("Pascal", new IsClause("programming_language", "Pascal"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("release_year", "1970"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        rule.AddAntecedent(new IsClause("implements_oop", "no"));
        _engine.AddRule(rule);

        rule = new Rule("Java", new IsClause("programming_language", "Java"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        rule.AddAntecedent(new IsClause("release_year", "1995"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "yes"));
        _engine.AddRule(rule);

        rule = new Rule("C++", new IsClause("programming_language", "C++"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        rule.AddAntecedent(new IsClause("release_year", "1985"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        _engine.AddRule(rule);

        rule = new Rule("Assembler", new IsClause("programming_language", "Assembler"));
        rule.AddAntecedent(new IsClause("implements_oop", "no"));
        rule.AddAntecedent(new IsClause("release_year", "1947"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        _engine.AddRule(rule);

        rule = new Rule("COBOL", new IsClause("programming_language", "COBOL"));
        rule.AddAntecedent(new LessClause("release_year", "1959"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        _engine.AddRule(rule);

        rule = new Rule("Go", new IsClause("programming_language", "Go"));
        rule.AddAntecedent(new IsClause("compilation", "compilable"));
        rule.AddAntecedent(new IsClause("release_year", "2009"));
        rule.AddAntecedent(new IsClause("uses_virtual_machine", "no"));
        rule.AddAntecedent(new IsClause("implements_oop", "yes"));
        _engine.AddRule(rule);
    }

    [Test]
    public void TestBackwardChain()
    {
        _engine.AddFact(new IsClause("compilation", "compilable"));
        _engine.AddFact(new LessOrEqualClause("release_year", "2010"));
        _engine.AddFact(new GreaterOrEqualClause("release_year", "2005"));
        _engine.AddFact(new IsClause("implements_oop", "yes"));
        _engine.AddFact(new IsClause("uses_virtual_machine", "no"));

        Console.WriteLine("Infer: vehicle");

        List<BaseClause> unprovedConditions = new ();

        BaseClause? conclusion = _engine.Infer("programming_language", unprovedConditions);

        Console.WriteLine("Conclusion: " + conclusion);

        Assert.NotNull(conclusion);
        Assert.That(conclusion!.Value, Is.EqualTo("Go"));
    }

    [Test]
    public void TestForwardChain()
    {
        _engine.AddFact(new IsClause("uses_virtual_machine", "no"));
        _engine.AddFact(new IsClause("implements_oop", "yes"));

        Console.WriteLine("before inference");
        Console.WriteLine("{0}", _engine.Facts);
        Console.WriteLine("");

        _engine.Infer(); //forward chain

        Console.WriteLine("after inference");
        Console.WriteLine("{0}", _engine.Facts);
        Console.WriteLine("");

        Assert.That(_engine.Facts.Count, Is.EqualTo(6));
    }
}