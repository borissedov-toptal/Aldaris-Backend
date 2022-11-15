using System.Data;
using Aldaris.API.Domain;
using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.API.Data;

public static class DbInitializer
{
    public static void Initialize(AldarisContext context)
    {
        // Look for any students.
        if (context.Questions.Any())
        {
            return; // DB has been seeded
        }

        var kbCompilationCompilable = new KnowledgeBaseFact("compilation = compilable");
        var kbCompilationInterpreted = new KnowledgeBaseFact("compilation = interpreted");
        var kbUsesVMYes = new KnowledgeBaseFact("uses_virtual_machine = yes");
        var kbUsesVMNo = new KnowledgeBaseFact("uses_virtual_machine = no");
        var ImplementsOOPYes = new KnowledgeBaseFact("implements_oop = yes");
        var ImplementsOOPNo = new KnowledgeBaseFact("implements_oop = no");

        var rules = new[]
        {
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = C#"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    new KnowledgeBaseFact("release_year = 2000"),
                    kbUsesVMYes,
                    ImplementsOOPYes
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Swift"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    new KnowledgeBaseFact("release_year = 2014"),
                    kbUsesVMNo,
                    ImplementsOOPYes,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Python"))
            {
                Antecedents = new[]
                {
                    kbCompilationInterpreted,
                    new KnowledgeBaseFact("release_year = 1991"),
                    kbUsesVMNo,
                    ImplementsOOPYes,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Pascal"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    new KnowledgeBaseFact("release_year = 1970"),
                    kbUsesVMNo,
                    ImplementsOOPNo,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Java"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    ImplementsOOPYes,
                    new KnowledgeBaseFact("release_year = 1995"),
                    kbUsesVMYes,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = C++"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    ImplementsOOPYes,
                    new KnowledgeBaseFact("release_year = 1985"),
                    kbUsesVMNo,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Assembler"))
            {
                Antecedents = new[]
                {
                    ImplementsOOPNo,
                    new KnowledgeBaseFact("release_year = 1947"),
                    kbUsesVMNo,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = COBOL"))
            {
                Antecedents = new[]
                {
                    new KnowledgeBaseFact("release_year = 1959"),
                    kbUsesVMNo,
                }
            },
            new KnowledgeBaseRule(new KnowledgeBaseFact("programming_language = Go"))
            {
                Antecedents = new[]
                {
                    kbCompilationCompilable,
                    new KnowledgeBaseFact("release_year = 2009"),
                    kbUsesVMNo,
                    ImplementsOOPYes,
                }
            },
            // new KnowledgeBaseRule(new KnowledgeBaseFact("release_year_range = 1940-1960"))
            // {
            //     Antecedents = new[]
            //     {
            //         new KnowledgeBaseFact("release_year >= 1940"),
            //         new KnowledgeBaseFact("release_year <= 1960")
            //     }
            // },
            // new KnowledgeBaseRule(new KnowledgeBaseFact("release_year_range = 1961-1980"))
            // {
            //     Antecedents = new[]
            //     {
            //         new KnowledgeBaseFact("release_year > 1960"),
            //         new KnowledgeBaseFact("release_year <= 1980")
            //     }
            // },
            // new KnowledgeBaseRule(new KnowledgeBaseFact("release_year_range = 1981-2000"))
            // {
            //     Antecedents = new[]
            //     {
            //         new KnowledgeBaseFact("release_year > 1980"),
            //         new KnowledgeBaseFact("release_year <= 2000")
            //     }
            // },
            // new KnowledgeBaseRule(new KnowledgeBaseFact("release_year_range = after2000"))
            // {
            //     Antecedents = new[]
            //     {
            //         new KnowledgeBaseFact("release_year > 2000"),
            //     }
            // }
        };

        var answerYes = new Answer("Yes", "=", "yes");
        var answerNo = new Answer("No", "=", "no");

        var questions = new Question[]
        {
            new("Does your programming language usually require a virtual machine to execute a program?",
                "uses_virtual_machine")
            {
                PossibleAnswers = new Answer[]
                {
                    answerYes,
                    answerNo
                }
            },
            new("Does your programming language implement OOP paradigm?", "implements_oop")
            {
                PossibleAnswers = new Answer[]
                {
                    answerYes,
                    answerNo
                }
            },
            new("How does a program written on your programming language be running?", "compilation")
            {
                PossibleAnswers = new Answer[]
                {
                    new("It requires compilation. Then a machine will will run it.", "=", "compilable"),
                    new("It will be executed by Interpreter.", "=", "interpreted")
                }
            },
            new("When was your programming language initially released?", "release_year")
            {
                //1940-1960, 1961-1980, 1980-2000, after 2000
                PossibleAnswers = new Answer[]
                {
                    new("1940 - 1960", "&&", "release_year >= 1940 && release_year <= 1960"),
                    new("1961-1980", "&&", "release_year > 1961 && release_year <= 1980"),
                    new("1980-2000", "&&", "release_year > 1981 && release_year <= 2000"),
                    new("after 2000", ">", "2000"),
                }
            }
        };

        context.Rules.AddRange(rules);
        context.Questions.AddRange(questions);
        context.SaveChanges();
        context.SaveChanges();
    }
}