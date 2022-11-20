using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    Camera _mainCamera;
    Transform _transform;
    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
    }

    private void LookAt() {
        Vector3 _mouseCoordinates = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 desiredVector = _mouseCoordinates - _transform.position;
        float degress = Mathf.Rad2Deg * Mathf.Atan2(desiredVector.y, desiredVector.x);
        _transform.eulerAngles = new Vector3(0, 0, degress);
        _transform.localPosition = desiredVector.normalized;
        if (desiredVector.x <= 0)
        {
            _spriteRenderer.flipY = true;
        }
        else
        {
            _spriteRenderer.flipY = false;
        }
    }
}
