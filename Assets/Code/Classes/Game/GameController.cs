using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ObstacleSpawner))]
public class GameController : MonoBehaviour
{
    /// The current state that the game is in.
    public static GameStates CurrentState = GameStates.Menu;

    /// The player's total score in-game so far.
    public int Score { get { return _Score; } set { ChangeScore (value); } }

    [Tooltip ("The player's total score in-game so far.")]
    [SerializeField] private int _Score = 0;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        this.tag = "GameController";
        EventManager.OnStateChanged += ChangeState;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        Score = 0;
        EventManager.ChangeState (GameStates.Menu);
    }

    private void ChangeState (GameStates state)
    {
        CurrentState = state;

        switch(state)
        {
            case GameStates.Menu:
                Time.timeScale = 0.0f;
                ReturnToMenu ();
                break;
            case GameStates.Game:
                Time.timeScale = 1.0f;
                StartGame ();
                break;
            case GameStates.Pause:
                Time.timeScale = 0.0f;
                break;
            case GameStates.Finish:
                Time.timeScale = 0.0f;
                GameOver ();
                break;
            case GameStates.Restart:
                Time.timeScale = 0.0f;
                RestartGame ();
                break;
        }
    }

    private void ReturnToMenu ()
    {
        Cursor.visible = true;
        GetComponent<ObstacleSpawner> ().ResetSpawner ();
        GameObject.FindGameObjectWithTag ("Player").transform.position = Vector3.zero;

        if (PlayerPrefs.GetInt ("Hi-Score") < _Score)
            PlayerPrefs.SetInt ("Hi-Score", _Score);

        Score = 0;
    }

    private void StartGame ()
    {
        Cursor.visible = false;
    }

    private void GameOver ()
    {
        Cursor.visible = true;
    }

    private void RestartGame ()
    {
        Cursor.visible = false;

        GetComponent<ObstacleSpawner> ().ResetSpawner ();
        GameObject.FindGameObjectWithTag ("Player").transform.position = Vector3.zero;

        if (PlayerPrefs.GetInt ("Hi-Score") < _Score)
            PlayerPrefs.SetInt ("Hi-Score", _Score);

        Score = 0;

        EventManager.ChangeState (GameStates.Game);
    }

    private void ChangeScore (int score)
    {
        _Score = score;

        EventManager.UpdateScore (_Score);
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= ChangeState;
    }
}
