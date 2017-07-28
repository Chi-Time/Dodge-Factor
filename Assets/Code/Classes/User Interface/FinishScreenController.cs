using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishScreenController : MonoBehaviour
{
    [Tooltip ("The label for the game over screen title.")]
    [SerializeField] private Text _GameOverLabel = null;
    [Tooltip ("The label for displaying the final score.")]
    [SerializeField] private Text _FinalScoreLabel = null;
    [Tooltip ("The label for displaying the previous hi-score.")]
    [SerializeField] private Text _HiScoreLabel = null;

    private void OnEnable ()
    {
        _FinalScoreLabel.text = "Final Score: " + GetScore ().ToString ();
        _HiScoreLabel.text = "Hi-Score: " + PlayerPrefs.GetInt ("Hi-Score").ToString ();

        if (PlayerPrefs.GetInt ("Hi-Score") < GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().Score)
            _GameOverLabel.text = "Game Over\nNew Hi-Score!";
        else
            _GameOverLabel.text = "Game Over";

        SetScoreLabels ();
        SetGameOverLabel ();
    }

    private void SetScoreLabels ()
    {
        
    }

    private void SetGameOverLabel ()
    {
        
    }

    private int GetScore ()
    {
        return GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().Score;
    }

    public void RestartGame ()
    {
        EventManager.ChangeState (GameStates.Restart);
    }

    public void ReturnToMenu ()
    {
        EventManager.ChangeState (GameStates.Menu);
    }
}
