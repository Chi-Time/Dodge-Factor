using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider), typeof (AudioSource))]
public class ScoreTrigger : MonoBehaviour
{
    [Tooltip ("The various sounds to use each time the play hits a score trigger.")]
    [SerializeField] private AudioClip[] _TriggerSounds = null;

    /// Reference to the score trigger's audiosource component.
    private AudioSource _AudioSource = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _AudioSource = GetComponent<AudioSource> ();
        GetComponent<Collider> ().isTrigger = true;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _AudioSource.playOnAwake = false;
        _AudioSource.spatialBlend = 0.0f;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Player"))
            IncreaseScore ();
    }

    private void IncreaseScore ()
    {
        PlayCollectionSound ();
        GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().Score++;
    }

    private void PlayCollectionSound ()
    {
        if(_TriggerSounds != null)
        {
            var clip = _TriggerSounds[Random.Range (0, _TriggerSounds.Length)];
            _AudioSource.PlayOneShot (clip);
        }
    }
}
