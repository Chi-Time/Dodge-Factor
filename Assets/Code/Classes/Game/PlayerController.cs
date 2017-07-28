using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody), typeof (Collider), typeof (AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The speed of the player's horizontal movement.")]
    [SerializeField] private float _Speed = 1100.0f;
    [Tooltip("The min and max boundaries of the players movement on either side.")]
    [SerializeField] private float _XMin = -5.0f, _XMax = 5.0f;
    [SerializeField] private AudioClip _DeathSound = null;

    /// The player's current direction and velocity.
    private Vector3 _Velocity = Vector3.zero;
    /// Reference to the player's rigidbody component.
    private Rigidbody _Rigidbody = null;
    /// Reference to the player's transform component.
    private Transform _Transform = null;
    // Reference to the player's audiosource component.
    private AudioSource _AudioSource = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        this.tag = "Player";

        _Rigidbody = GetComponent<Rigidbody> ();
        _Transform = GetComponent<Transform> ();
        _AudioSource = GetComponent<AudioSource> ();
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _Rigidbody.useGravity = false;
        _Rigidbody.isKinematic = false;
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        _AudioSource.playOnAwake = false;
        _AudioSource.spatialBlend = 0.0f;
    }

    private void Update ()
    {
        GetInput ();
        ConstrainPosition ();
    }

    private void GetInput ()
    {
        GetMovementInput ();
        GetButtonInput ();
    }

    private void GetMovementInput ()
    {
        var movInput = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, 0.0f);

        _Velocity = movInput * _Speed;

//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//        {
//            var pos = Input.GetTouch (0).position;
//
//            var worldPos = Camera.main.ScreenToWorldPoint (pos);
//
//            if (worldPos.x > 0)
//                _Velocity = Vector3.right * _Speed;
//            else if (worldPos.x < 0f)
//                _Velocity = Vector3.left * _Speed;
//        }
//
//        if (Input.GetTouch (0).phase == TouchPhase.Ended)
//            _Velocity = Vector3.zero;
    }

    private void GetButtonInput ()
    {
        if ((Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Jump")) && GameController.CurrentState == GameStates.Game)
            EventManager.ChangeState (GameStates.Pause);
        else if ((Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Jump")) && GameController.CurrentState == GameStates.Pause)
            EventManager.ChangeState (GameStates.Game);
    }

    private void ConstrainPosition ()
    {
        _Transform.position = new Vector3 (
            Mathf.Clamp (_Transform.position.x, _XMin, _XMax),
            0.0f,
            0.0f
        );
    }

    private void FixedUpdate ()
    {
        Move ();
    }

    private void Move ()
    {
        _Rigidbody.velocity = _Velocity * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter (Collision other)
    {
        EventManager.ChangeState (GameStates.Finish);

        if(_DeathSound != null)
            _AudioSource.PlayOneShot (_DeathSound);
    }
}
