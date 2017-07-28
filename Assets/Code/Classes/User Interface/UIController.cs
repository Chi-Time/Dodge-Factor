using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIController : MonoBehaviour
{
    [Tooltip ("")]
    public MenuScreenController _MenuScreen = null;
    [Tooltip ("")]
    public GameScreenController _GameScreen = null;
    [Tooltip ("")]
    public PauseScreenController _PauseScreen = null;
    [Tooltip ("")]
    public FinishScreenController _FinishScreen = null;

    private EventSystem _EventSystem = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _MenuScreen = GetComponentInChildren<MenuScreenController> ();
        _GameScreen = GetComponentInChildren<GameScreenController> ();
        _PauseScreen = GetComponentInChildren<PauseScreenController> ();
        _FinishScreen = GetComponentInChildren<FinishScreenController> ();
        _EventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
        
        EventManager.OnStateChanged += ChangeState;
    }

    private void ChangeState (GameStates state)
    {
        switch(state)
        {
            case GameStates.Menu:
                ChangeScreen (_MenuScreen.gameObject);
                _EventSystem.SetSelectedGameObject (GameObject.Find ("Start Game"));
                break;
            case GameStates.Game:
                ChangeScreen (_GameScreen.gameObject);
                break;
            case GameStates.Pause:
                ChangeScreen (_PauseScreen.gameObject);
                _EventSystem.SetSelectedGameObject (GameObject.Find ("Return Button"));
                break;
            case GameStates.Finish:
                ChangeScreen (_FinishScreen.gameObject);
                _EventSystem.SetSelectedGameObject (GameObject.Find ("Restart Button"));
                break;
        }
    }

    private void ChangeScreen (GameObject screen)
    {
        _MenuScreen.gameObject.SetActive (false);
        _GameScreen.gameObject.SetActive (false);
        _PauseScreen.gameObject.SetActive (false);
        _FinishScreen.gameObject.SetActive (false);

        screen.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= ChangeState;
    }
}
