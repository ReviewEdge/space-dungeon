using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunScript : MonoBehaviour
{
    public BulletScript _bulletPrefab;
    public int _bulletSpeed;
    private Transform _entityLocation;
    private Camera _mainCamera;
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _entityLocation = GetComponent<Transform>();
        _mainCamera = Camera.main;
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            OnClick();
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnClick() {
        SpawnBullet();
    }

    private void SpawnBullet() {
        BulletScript bullet = Instantiate(_bulletPrefab, new Vector3(_entityLocation.position.x, _entityLocation.position.y, 0), Quaternion.identity);

        Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0, -_mainCamera.transform.position.z);
        Vector3 entityLocation = _transform.position;
        Vector3 desiredVector = (mouseLocation + (-entityLocation)).normalized;

        bullet.GetComponent<Rigidbody2D>().AddForce(desiredVector * _bulletSpeed);
    }
}
