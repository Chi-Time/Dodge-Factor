using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScreenController : MonoBehaviour
{
    [Tooltip ("The label for displaying the player's score throughout the game.")]
    [SerializeField] private Text _ScoreLabel = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        EventManager.OnScoreUpdated += UpdateScore;
    }

    private void UpdateScore (int score)
    {
        _ScoreLabel.text = "Score: " + score.ToString ();
    }

    public void PauseGame ()
    {
        EventManager.ChangeState (GameStates.Pause);
    }

    private void OnDestroy ()
    {
        EventManager.OnScoreUpdated -= UpdateScore;
    }
}
