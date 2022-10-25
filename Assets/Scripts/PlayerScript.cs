using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    SpriteRenderer _srender;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public Camera _mainCamera;
    public int health = 100;
    public int lives = 3;
    public bool _isDead;
    public float _SPEED = 5;
    public float moveSpeed; //speed var
    public float roll; //roll distance
    public bool hasRayGun = false;
    public bool hasLaserSword = false;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _srender = GetComponent<SpriteRenderer>();
        _rayGun = GetComponent<RayGunScript>();
        _rayGun.GetComponent<RayGunScript>().coolDownTime = .3f;
        _laserSword = GetComponent<LaserSwordScript>();

        _isDead = false;
    }

    private void Update()
    {
        if(hasLaserSword)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
                _laserSword.SwingLaserSword(mouseLocation);
            }
        } else if(hasRayGun)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
                _rayGun.ShootRayGun(mouseLocation);
            }
        }

        if(health <= 0)
        {
            PlayerDeath();
        }

        while (_isDead)
        {
            _srender.enabled = false;
        }

    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerDeath()
    {
        _isDead = true;

        //implement some function that waits for a few seconds before player respawns?
        //Task.Delay(3000);

        lives--;
        _rbody.position = new Vector2(0, 0);
        health = 100;

        _isDead = false;
    }
    private void MovePlayer()
    {
        float x = _SPEED * Input.GetAxis("Horizontal");
        float y = _SPEED * Input.GetAxis("Vertical");
        _rbody.velocity = new Vector2(x, y);

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

    /*public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.enemyTag))
        {
            //Take some damage from running into Guard?
        }
    }*/
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.healthpackTag))
        {
            RestoreHealth(25);

            //destroys Healthpack after it is used
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
