using Aldaris.API.Domain;

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

        //
        // var answers = new Answer[]
        // {
        //     new Answer { Text = "Sir Lancelot of Camelot." },
        //     new Answer { Text = "Sir Robin of Camelot." },
        //     new Answer { Text = "Sir Galahad of Camelot." },
        //     new Answer { Text = "It is 'Arthur', King of the Britons." },
        //     new Answer { Text = "To seek the Holy Grail." },
        //     new Answer { Text = "Blue." },
        //     new Answer { Text = "Blue. No, yel-- auuuuuuuugh!" },
        //     new Answer { Text = "I don't know that! Auuuuuuuugh!" },
        //     new Answer { Text = "What do you mean? An African or European swallow?" }
        // };
        //
        // context.Answers.AddRange(answers);
        // context.SaveChanges();


        var questions = new Question[]
        {
            new Question
            {
                Text = "What... is your name?", PossibleAnswers = new Answer[]
                {
                    new Answer { Text = "Sir Lancelot of Camelot." },
                    new Answer { Text = "Sir Robin of Camelot." },
                    new Answer { Text = "Sir Galahad of Camelot." },
                    new Answer { Text = "It is 'Arthur', King of the Britons." },
                }
            },
            new Question
            {
                Text = "What... is your quest?", PossibleAnswers = new Answer[]
                {
                    new Answer { Text = "To seek the Holy Grail." },
                    new Answer { Text = "I seek the Grail." },
                }
            },
            new Question
            {
                Text = "What... is your favourite color?", PossibleAnswers = new Answer[]
                {
                    new Answer { Text = "Blue." },
                    new Answer { Text = "Blue. No, yel-- auuuuuuuugh!" },
                }
            },
            new Question
            {
                Text = "What... is a capital of Assyria?", PossibleAnswers = new Answer[]
                {
                    new Answer { Text = "I don't know that! Auuuuuuuugh!" },
                }
            },
            new Question
            {
                Text = "What... is the air-speed velocity of an unladen swallow?", PossibleAnswers = new Answer[]
                {
                    new Answer { Text = "What do you mean? An African or European swallow?" }
                }
            }
        };

        context.Questions.AddRange(questions);
        context.SaveChanges();

        context.SaveChanges();
    }
}