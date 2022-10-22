using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    const float _SPEED = 5;
    public float moveSpeed; //speed var
    public float roll; //roll distance
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //get H and V vars
        float x = _SPEED * Input.GetAxis("Horizontal");
        float y = _SPEED * Input.GetAxis("Vertical");
        _rbody.velocity = new Vector2(x, y);

        //move player
        Vector2 movement = new Vector2(x, y);
        _rbody.velocity = movement * moveSpeed;


        //check for space key to roll player
        if (Input.GetKeyDown(KeyCode.Space) && movement.x != 0)
        {
            // dodge/roll player left/right
            _rbody.velocity = new Vector2((x * moveSpeed) * roll, _rbody.velocity.y);
            // play roll sprite animation
        }
        else if (Input.GetKeyDown(KeyCode.Space) && movement.y != 0)
        {
            // dodge/roll player up/down
            _rbody.velocity = new Vector2(_rbody.velocity.x, (x * moveSpeed) * roll);
            // play roll sprite animation
        }
    }
}
