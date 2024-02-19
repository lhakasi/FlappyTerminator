using System.Collections;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private EnemyPool _pool;

    private bool _isSpawning;

    private void Start()
    {
        _isSpawning = true;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        while (_isSpawning)
        {
            float spawnPositionY = Random.Range(_upperBound, _lowerBound);
            Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

            Helicopter helicopter = _pool.GetObject();

            helicopter.transform.position = spawnPoint;

            yield return delay;
        }
    }
}
