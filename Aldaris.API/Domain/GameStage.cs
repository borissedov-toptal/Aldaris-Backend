namespace Aldaris.API.Domain;

public enum GameStage
{
    NewGame = 1,
    InProgress = 2,
    Suggesting = 3,
    Completed = 4,
    UnableToSuggest = 5,
    ResolvePending = 6,
    Resolved = 7,
    Cancelled = 8
}