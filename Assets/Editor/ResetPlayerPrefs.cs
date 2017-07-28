using UnityEngine;
using UnityEditor;

public class ResetPlayerPrefs : EditorWindow 
{
    [MenuItem ("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs ()
    { 
        PlayerPrefs.DeleteAll();
    }
}
