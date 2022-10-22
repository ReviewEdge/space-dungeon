using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _speed;
    RayGunScript _rayGun;
    public Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rayGun = GetComponent<RayGunScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
            _rayGun.ShootRayGun(mouseLocation);
        }
    }
    void FixedUpdate()
    {
        float x = _speed * Input.GetAxis("Horizontal");
        float y = _speed * Input.GetAxis("Vertical");

        _rbody.velocity = new Vector2(x, y);
    }
}
