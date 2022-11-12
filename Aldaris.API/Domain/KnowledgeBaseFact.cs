namespace Aldaris.API.Domain;

public class KnowledgeBaseFact
{
    public KnowledgeBaseFact(string expression)
    {
        Expression = expression;
    }

    public int Id { get; set; }
    
    public string Expression { get; set; }
}