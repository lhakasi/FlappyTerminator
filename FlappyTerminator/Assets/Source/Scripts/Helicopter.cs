using UnityEngine;

public class Helicopter : MonoBehaviour, IInteractable, ICharacter
{
    [SerializeField] private AudioSource _explodeSound;
    private EnemyPool _pool;

    public void Die()
    {
        _explodeSound.Play();
        _pool.PutObject(this);
    }

    public void SetPool(EnemyPool pool) => _pool = pool;
}
