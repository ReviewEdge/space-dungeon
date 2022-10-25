using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunScript : MonoBehaviour
{
    public BulletScript _bulletPrefab;
    private Transform _entityLocation;
    public int damage;
    public float coolDownTime = 1;
    private int _bulletSpeed;
    private float _lastShot;

    // Start is called before the first frame update
    void Start()
    {
        _entityLocation = transform;
        _lastShot = Time.time;
        _bulletSpeed = 200;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

    }

    public void ShootRayGun(Vector3 aimingAt) {
        if (Time.time - _lastShot < coolDownTime)
        {
            return;
        }

        Vector3 directionalVector = (aimingAt + (-_entityLocation.position)).normalized;

        BulletScript bullet = Instantiate(_bulletPrefab, new Vector2(_entityLocation.position.x, _entityLocation.position.y), Quaternion.identity);

        bullet.SetDamage(damage);
        bullet.GetComponent<Rigidbody2D>().AddForce(directionalVector * _bulletSpeed);
        _lastShot = Time.time;
    }
}
