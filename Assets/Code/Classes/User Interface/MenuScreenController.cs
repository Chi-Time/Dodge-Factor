using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    //TODO: Possible hi-scores board and credits screen.

    private AudioManager _AudioManager = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _AudioManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<AudioManager> ();
    }

    public void StartGame ()
    {
        EventManager.ChangeState (GameStates.Game);
    }

    // For standalone builds.
    public void ExitGame ()
    {
        Application.Quit ();
    }

    public void MuteMusic (bool mute)
    {
        //TODO: Link into audiomixer later on.
        _AudioManager.MuteMusic (mute);
    }

    public void MuteSFX (bool mute)
    {
        //TODO: Link into audiomixer later on.
        _AudioManager.MuteSFX (mute);
    }
}
