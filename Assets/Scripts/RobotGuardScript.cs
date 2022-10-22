using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotGuardScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _speed;
    public GameObject _player;
    RayGunScript _rayGun;

    // Start is called before the first frame update
    void Start()
    {
        _rayGun = GetComponent<RayGunScript>();
    }

    // Update is called once per frame
    void Update()
    {

        Transform target = _player.transform;
        _rayGun.ShootRayGun(target.position);
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }
}
