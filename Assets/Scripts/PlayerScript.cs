using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public Camera _mainCamera;
    const float _SPEED = 5;
    public float moveSpeed; //speed var
    public float roll; //roll distance
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rayGun = GetComponent<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
            _rayGun.ShootRayGun(mouseLocation);
            //_laserSword.SwingLaserSword(mouseLocation);
        }
    }
    void FixedUpdate()
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


        //check for shift key to roll player
        if (Input.GetKeyDown(KeyCode.LeftShift) && movement.x != 0)
        {
            // dodge/roll player left/right
            _rbody.velocity = new Vector2((x * moveSpeed) * roll, _rbody.velocity.y);
            // play roll sprite animation
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && movement.y != 0)
        {
            // dodge/roll player up/down
            _rbody.velocity = new Vector2(_rbody.velocity.x, (x * moveSpeed) * roll);
            // play roll sprite animation
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.enemyTag))
        {
            //Take some damage from running into Guard?
            //Or Taking melee damage can be here
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.healthpackTag))
        {
            RestoreHealth(25);
            Destroy(collision.gameObject);  
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        print("Oww! My health is now " + health);
    }

    public void RestoreHealth(int hitpoints)
    {
        health += hitpoints;
        print("HP Restored; health is now " + health);
    }
}
