using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody), typeof (Collider))]
public class Obstacle : MonoBehaviour, IPoolable
{
    [Tooltip ("The point on the y axis at which to cull this object.")]
    [SerializeField] private float _CullingRange = -2.0f;

    /// Reference to the object pool this belongs to.
    private Pool _Pool = null;
    /// The speed at which the obstacle falls down the map.
    private float _Speed = 0.0f;
    /// Reference to the player's rigidbody component.
    private Rigidbody _Rigidbody = null;
    /// Reference to the players transform component.
    private Transform _Transform = null;
    /// Reference to the spawner this came from.
    private ObstacleSpawner _Spawner = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Rigidbody = GetComponent<Rigidbody> ();
        _Transform = GetComponent<Transform> ();
        _Spawner = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ObstacleSpawner> ();
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _Rigidbody.useGravity = false;
        _Rigidbody.isKinematic = false;
    }

    public void SetPool (Pool pool)
    {
        _Pool = pool;
    }

    private void OnEnable ()
    {
        _Speed = _Spawner.Speed;
    }

    private void Update ()
    {
        CheckPosition ();
    }
        
    private void CheckPosition ()
    {
        if (_Transform.position.y <= _CullingRange)
            Cull ();
    }

    private void FixedUpdate ()
    {
        Move ();
    }

    private void Move ()
    {
        _Rigidbody.velocity = Vector3.down * _Speed * Time.fixedDeltaTime;
    }

    public void Cull ()
    {
        _Pool.ReturnToPool (this.gameObject);
    }
}