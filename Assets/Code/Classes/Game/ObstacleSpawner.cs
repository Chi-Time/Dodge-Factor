using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    /// The global speed at which obstacles fall down.
    public float Speed { get { return _Speed;} }

    [Tooltip ("The range at which to generate a delay between each obstacle spawn.")]
    [SerializeField] private float _MinSpawnDelay = 0.5f, _MaxSpawnDelay = 0.75f;
    [Tooltip ("The min and max variation on the x axis for each obstacle spawn.")]
    [SerializeField] private float _XMin = -2.0f, _XMax = 2.0f;
    [Tooltip ("The speed at which the obstacles fall down the screen.")]
    [SerializeField] private float _Speed = 500.0f;
    [Tooltip ("The object pool for this controller's obstacles.")]
    [SerializeField] private Pool _Pool = new Pool ();

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Pool.Intialise ("Obstacle pool", "Obstacle");
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        StartCoroutine ("Spawn");
    }

    private IEnumerator Spawn ()
    {
        yield return new WaitForSeconds (GetDelay ());

        SpawnObstacle ();

        StopCoroutine ("Spawn");
        StartCoroutine ("Spawn");
    }

    private float GetDelay ()
    {
        return Random.Range (_MinSpawnDelay, _MaxSpawnDelay);
    }

    private void SpawnObstacle ()
    {
        var ob = _Pool.RetrieveFromPool (true);
        ob.transform.position = GetPosition ();
    }

    private Vector3 GetPosition ()
    {
        return new Vector3 (
            Random.Range (_XMin, _XMax),
            25.0f,
            0.0f
        );
    }

    public void ResetSpawner ()
    {
        StopCoroutine ("Spawn");
        _Pool.ResetPools ();
        StartCoroutine ("Spawn");
    }
}
