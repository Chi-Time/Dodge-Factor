using UnityEngine;
using System.Collections;

public class PauseScreenController : MonoBehaviour
{
    public void ReturnToGame ()
    {
        EventManager.ChangeState (GameStates.Game);
    }
}
