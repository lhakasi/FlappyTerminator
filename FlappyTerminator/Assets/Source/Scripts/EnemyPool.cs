using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Helicopter _enemy;
    [SerializeField] private int _poolSize = 10;

    private Queue<Helicopter> _pool = new Queue<Helicopter>();

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Helicopter helicopter = Instantiate(_enemy);
            helicopter.transform.parent = _enemyContainer;
            helicopter.SetPool(this);
            helicopter.gameObject.SetActive(false);
            _pool.Enqueue(helicopter);
        }
    }

    public Helicopter GetObject()
    {
        if (_pool.Count > 0)
        {
            Helicopter helicopter = _pool.Dequeue();
            helicopter.gameObject.SetActive(true);

            return helicopter;
        }
        else
        {
            return null;
        }
    }

    public void PutObject(Helicopter enemy)
    {
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
    }

    public void Reset()
    {
        foreach (Helicopter enemy in _enemyContainer.GetComponentsInChildren<Helicopter>())
        {
            PutObject(enemy);
        }
    }
}
