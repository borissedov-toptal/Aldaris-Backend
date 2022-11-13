using Aldaris.API.Domain;
using Aldaris.ExpertSystem.Clauses;

namespace Aldaris.API.Data;

public static class FakeDbInitializer
{
    public static void Initialize(AldarisContext context)
    {
        // Look for any students.
        if (context.Questions.Any() || context.Answers.Any() || context.Rules.Any() || context.Facts.Any())
        {
            return; // DB has been seeded
        }

        //
        // var answers = new Answer[]
        // {
        //     new Answer("Sir Lancelot of Camelot." },
        //     new Answer("Sir Robin of Camelot." },
        //     new Answer("Sir Galahad of Camelot." },
        //     new Answer("It is 'Arthur', King of the Britons." },
        //     new Answer("To seek the Holy Grail." },
        //     new Answer("Blue." },
        //     new Answer("Blue. No, yel-- auuuuuuuugh!" },
        //     new Answer("I don't know that! Auuuuuuuugh!" },
        //     new Answer("What do you mean? An African or European swallow?" }
        // };
        //
        // context.Answers.AddRange(answers);
        // context.SaveChanges();


        var questions = new Question[]
        {
            new( "What... is your name?", "name")
            {
                PossibleAnswers = new Answer[]
                {
                    new("Sir Lancelot of Camelot.", nameof(IsClause), "Lancelot"),
                    new("Sir Robin of Camelot." , nameof(IsClause), "Robin"),
                    new("Sir Galahad of Camelot." , nameof(IsClause), "Galahad"),
                    new("It is 'Arthur', King of the Britons." , nameof(IsClause), "Arthur"),
                }
            },
            new ("What... is your quest?", "quest")
            {
                PossibleAnswers = new Answer[]
                {
                    new("To seek the Holy Grail." , nameof(IsClause), "Seek the Grail"),
                    new("I seek the Grail." , nameof(IsClause), "Seek the Grail")
                }
            },
            new ("What... is your favourite color?", "favouriteColor")
            {
                PossibleAnswers = new Answer[]
                {
                    new("Blue." , nameof(IsClause), "Blue"),
                    new("Blue. No, yel-- auuuuuuuugh!", nameof(IsClause), "Blue" )
                }
            },
            new("What... is a capital of Assyria?", "capitalOfAssyria")
            {
                PossibleAnswers = new Answer[]
                {
                    new("I don't know that! Auuuuuuuugh!", nameof(IsClause), String.Empty)
                }
            },
            new( "What... is the air-speed velocity of an unladen swallow?", "swallowSpeed")
            {
                PossibleAnswers = new Answer[]
                {
                    new("What do you mean? An African or European swallow?" , nameof(IsClause), String.Empty)
                }
            }
        };

        context.Questions.AddRange(questions);
        context.SaveChanges();

        context.SaveChanges();
    }
}