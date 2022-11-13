namespace Aldaris.API.Domain;

public class KnowledgeBaseRule
{
    public int Id { get; set; }
    
    public int ConsequentId { get; set; }
    public KnowledgeBaseFact Consequent { get; set; }
    
    public ICollection<KnowledgeBaseFact> Antecedents { get; set; } = new List<KnowledgeBaseFact>();
    public List<KnowledgeBaseRuleAntecedent> KnowledgeBaseRuleAntecedents { get; set; } = new();
    
    public KnowledgeBaseRule(){}
    public KnowledgeBaseRule(KnowledgeBaseFact consequent)
    {
        Consequent = consequent;
    }
}