using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletForce = 20f;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private float _delayToReturnBullet;
    [SerializeField] private AudioSource _machineGunSound;

    private float _nextFireTime = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightControl) && Time.time >= _nextFireTime)
        {
            Shoot();
            _machineGunSound.Play();

            _nextFireTime = Time.time + 1f / _fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = _bulletPool.GetBulletFromPool(_firePoint.position, _firePoint.rotation, _bulletForce);

        StartCoroutine(ReturnBulletToPoolAfterDelay(bullet));
    }

    private IEnumerator ReturnBulletToPoolAfterDelay(GameObject bullet)
    {
        yield return new WaitForSeconds(_delayToReturnBullet);

        _bulletPool.ReturnBulletToPool(bullet);
    }
}
