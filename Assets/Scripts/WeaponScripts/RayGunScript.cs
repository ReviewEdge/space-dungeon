using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunScript : MonoBehaviour
{
    public BulletScript _bulletPrefab;
    private Transform _entityLocation;
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
        if (Time.time - _lastShot < 1)
        {
            return;
        }

        Vector3 desiredVector = (aimingAt + (-_entityLocation.position)).normalized;

        BulletScript bullet = Instantiate(_bulletPrefab, new Vector2(_entityLocation.position.x, _entityLocation.position.y), Quaternion.identity);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(desiredVector * _bulletSpeed);
        _lastShot = Time.time;
    }
}