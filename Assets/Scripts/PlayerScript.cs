using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public float _speed;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D _rbody;
    }

    void FixedUpdate()
    {
        float x = _speed * Input.GetAxis("Horizontal");
        float y = _speed * Input.GetAxis("Vertical");

        _rbody.velocity = new Vector2(x, y);
    }
}
