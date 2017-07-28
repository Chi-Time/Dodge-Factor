public delegate void StateChanged (GameStates state);
public delegate void ScoreUpdated (int score);

public static class EventManager
{
    /// Event listener for when game states are changed.
    public static event StateChanged OnStateChanged;
    /// Event listener for when the global score is updated and changed.
    public static event ScoreUpdated OnScoreUpdated;

    /// <summary>
    /// Changes the current game state.
    /// </summary>
    /// <param name="state">The new state for the game to switch to.</param>
    public static void ChangeState (GameStates state)
    {
        OnStateChanged (state);
    }

    /// <summary>
    /// Updates the current score.
    /// </summary>
    /// <param name="score">The new score to update.</param>
    public static void UpdateScore (int score)
    {
        OnScoreUpdated (score);
    }
}
