using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotGuardScript : MonoBehaviour
{
    Rigidbody2D _rbody;

    public bool _isDead;
    public int _health;

    public float _speed;
    public GameObject _player;
    RayGunScript _rayGun;
    public Sprite _weaponDrop;

    // Start is called before the first frame update
    void Start()
    {
        _isDead = false;
        _rayGun = GetComponent<RayGunScript>();
        _player = GameObject.FindWithTag(TagList.playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        _rayGun.ShootRayGun(target.position);
        if (!_isDead)
        {
            Transform target = _player.transform;
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        _isDead = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = _weaponDrop;
    }
}
