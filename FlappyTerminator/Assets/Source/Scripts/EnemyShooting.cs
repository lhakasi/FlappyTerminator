using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletForce = 20f;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float _delayToReturnBullet;
    [SerializeField] private AudioSource _laserSound;

    private float _nextFireTime = 0f;    

    private void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _laserSound.Play();

            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = bulletPool.GetBulletFromPool(_firePoint.position, _firePoint.rotation, _bulletForce);

        StartCoroutine(ReturnBulletToPoolAfterDelay(bullet));
    }

    private IEnumerator ReturnBulletToPoolAfterDelay(GameObject bullet)
    {
        yield return new WaitForSeconds(_delayToReturnBullet);

        bulletPool.ReturnBulletToPool(bullet);
    }
}
