using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Pool
{
    /// The pool of active objects currently in scene.
    public List<GameObject> ActivePool { get { return _ActivePool; } }

    /// The pool of inactive objects currently in scene.
    public List<GameObject> InactivePool { get { return _InactivePool; } }

    [Tooltip ("The number of objects to spawn into the pool.")]
    [SerializeField] private int _PoolSize = 0;
    [Tooltip ("The prefab objects to pool.")]
    [SerializeField] private GameObject[] _Prefabs = null;
    [Tooltip ("The pool of currently active objects in scene.")]
    [SerializeField] private List<GameObject> _ActivePool = new List<GameObject> ();
    [Tooltip ("The pool of currently inactive objects in scene.")]
    [SerializeField] private List<GameObject> _InactivePool = new List<GameObject> ();

    /// The name of the current pool instance.
    private string _PoolName = "Pool";
    /// The name of object to deal with.
    private string _ObjectName = "Object";

    /// <summary>
    /// Initialises the pool for later use. (Faux Constructor.)
    /// <param name="poolName">The name of the pool in the editor.</param>
    /// <param name="objectName">The name of the object that will be pooled.</param>
    /// </summary>
    public void Intialise (string poolName, string objectName)
    {
        _PoolName = poolName;
        _ObjectName = objectName;

        GeneratePool ();
    }

    private void GeneratePool ()
    {
        for (int i = 0; i < _PoolSize; i++)
        {
            for (int j = 0; j < _Prefabs.Length; j++)
                _InactivePool.Add (SpawnPoolObject (_Prefabs[j], i));
        }
    }

    /// <summary>
    /// Spawns a defaulted projectile, set up and ready to be used.
    /// </summary>
    /// <param name="obj">The gameobject to spawn.</param>
    /// <param name="index">The current index of the object.</param>
    /// <returns></returns>
    private GameObject SpawnPoolObject (GameObject obj, int index)
    {
        var go = (GameObject)Object.Instantiate (obj, Vector3.zero, Quaternion.identity);
        go.transform.SetParent (GetPoolHolder ());
        go.name = _ObjectName + ": " + index;
        go.SetActive (false);

        var poolObj = go.GetComponent<IPoolable> ();
        poolObj.SetPool (this);

        return go;
    }

    /// <summary>
    /// Retrieves the pool holder for this pool.
    /// </summary>
    /// <returns></returns>
    private Transform GetPoolHolder ()
    {
        if (!GameObject.Find (_PoolName))
            return new GameObject (_PoolName).transform;

        return GameObject.Find (_PoolName).transform;
    }

    /// <summary>
    /// Retrieves an object from the pool ready to go.
    /// </summary>
    /// <returns>The pool object, activated and ready to use.</returns>
    /// <param name="random">If set to <c>true</c> will return a random object from the pool.</param>
    public GameObject RetrieveFromPool (bool spawnRandom)
    {
        if (_InactivePool.Count > 0)
        {
            var poolObj = GetPoolObject (spawnRandom);
            _InactivePool.Remove (poolObj);
            _ActivePool.Add (poolObj);

            poolObj.gameObject.SetActive (true);

            return poolObj;
        }

        return null;
    }

    private GameObject GetPoolObject (bool spawnRandom)
    {
        if (spawnRandom)
            return _InactivePool[Random.Range (0, _InactivePool.Count)];

        return _InactivePool[0];
    }

    /// <summary>
    /// Returns the object back to the pool and resets it.
    /// </summary>
    /// <param name="poolObj">The pool object to return.</param>
    public void ReturnToPool (GameObject poolObj)
    {
        _ActivePool.Remove (poolObj);
        _InactivePool.Add (poolObj);

        poolObj.gameObject.SetActive (false);
        poolObj.transform.position = Vector3.zero;
        poolObj.transform.rotation = Quaternion.Euler (Vector3.zero);
    }

    public void ResetPools ()
    {
        ReturnAllActiveObjects ();
        ClearActivePool ();
    }

    private void ReturnAllActiveObjects ()
    {
        for (int i = 0; i < _ActivePool.Count; i++)
        {
            var obj = _ActivePool[i];
            obj.SetActive (false);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.Euler (Vector3.zero);

            _InactivePool.Add (obj);
        }
    }

    private void ClearActivePool ()
    {
        _ActivePool.Clear ();
        _ActivePool.TrimExcess ();
    }
}
