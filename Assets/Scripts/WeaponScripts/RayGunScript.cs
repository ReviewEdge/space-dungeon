using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunScript : MonoBehaviour
{
    public BulletScript _bulletPrefab;
    private Transform _entityLocation;
    public int damage;
    public float attackSpeed;
    private float _timeBtwAttack;
    private int _bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _entityLocation = transform;
        _bulletSpeed = 400;
    }

    // Update is called once per frame
    void Update()
    {
        _timeBtwAttack -= Time.deltaTime;
    }

    public void ShootRayGun(Vector3 aimingAt) {
        if (_timeBtwAttack > 0) {
            return;
        }

        Vector3 directionalVector = (aimingAt + (-_entityLocation.position)).normalized;

        BulletScript bullet = Instantiate(_bulletPrefab, new Vector2(_entityLocation.position.x, _entityLocation.position.y), Quaternion.identity);

        bullet.SetDamage(damage);
        bullet.GetComponent<Rigidbody2D>().AddForce(directionalVector * _bulletSpeed);
        
        //bullet.GetComponent<Rigidbody2D>().AddForce(directionalVector * _bulletSpeed);

        _timeBtwAttack = attackSpeed;
    }
}
