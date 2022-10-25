using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotGuardScript : MonoBehaviour
{
    Rigidbody2D _rbody;

    public int _health;
    public bool hasRayGun = false;
    public bool hasLaserSword = false;

    public float _speed;
    public GameObject _player;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public GameObject _laserSwordDropPrefab;
    public GameObject _rayGunDropPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = gameObject.GetComponent<Rigidbody2D>();
        _rayGun = GetComponent<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
        _player = GameObject.FindWithTag(TagList.playerTag);
    }

    // Update is called once per frame
    void Update()
    {

        Transform target = _player.transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(hasLaserSword)
        {
            _laserSword.SwingLaserSword(target.position);
        } else if(hasRayGun)
        {
            _rayGun.ShootRayGun(target.position);
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(hasLaserSword)
        {
            Instantiate(_laserSwordDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        } else if(hasRayGun)
        {
            Instantiate(_rayGunDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        _health -= damage;
    }

}
