namespace Aldaris.API.Domain;

public class KnowledgeBaseRuleAntecedent
{
    public int RuleId { get; set; }
    public KnowledgeBaseRule Rule { get; set; }
    
    public int AntecedentId { get; set; }
    public KnowledgeBaseFact Antecedent { get; set; }

}