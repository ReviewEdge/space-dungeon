using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    SpriteRenderer _srender;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public Camera _mainCamera;
    GeneralManagerScript _generalManager;

    public int health = 100;
    const int maxHealth = 100;
    public bool _isDead = false;
    public float _SPEED = 5;
    public float moveSpeed; //speed var
    public float roll; //roll distance
    public int magazineAmmo;
    public int remainingAmmo = 0;
    const int maxMagSize = 30;

    public GameObject RightAnim;
    public GameObject UpAnim;
    public GameObject LeftAnim;
    public GameObject DownAnim;

    public TagList.weaponType weapon;
    [SerializeField] TagList.directions direction;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _srender = GetComponent<SpriteRenderer>();
        _rayGun = GetComponent<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _isDead = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
            switch (weapon)
            {
                case TagList.weaponType.LaserSword:
                    remainingAmmo = 0;
                    _laserSword.SwingLaserSword(mouseLocation);
                    break;
                case TagList.weaponType.RayGun:
                    remainingAmmo--;
                    if (remainingAmmo > 0)
                    {
                        _rayGun.ShootRayGun(mouseLocation);
                    }
                    else {
                        weapon = TagList.weaponType.LaserSword;
                    }
                    break;
            }
        }

    }
    void FixedUpdate()
    {
        MovePlayer();
        ChangePlayerDirection();
    }

    private void MovePlayer()
    {
        float x = _SPEED * Input.GetAxis("Horizontal");
        float y = _SPEED * Input.GetAxis("Vertical");
        _rbody.velocity = new Vector2(x, y);

        Vector2 movement = new Vector2(x, y);
        _rbody.velocity = movement * moveSpeed;

        if (Input.GetKey(KeyCode.D))
        {
            direction = TagList.directions.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direction = TagList.directions.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = TagList.directions.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = TagList.directions.down;
        }
    }
    private void ChangePlayerDirection()
    {
        switch (direction)
        {
            case TagList.directions.right:
                RightAnim.SetActive(true);
                UpAnim.SetActive(false);
                LeftAnim.SetActive(false);
                DownAnim.SetActive(false);
                break;
            case TagList.directions.up:
                RightAnim.SetActive(false);
                UpAnim.SetActive(true);
                LeftAnim.SetActive(false);
                DownAnim.SetActive(false);
                break;
            case TagList.directions.left:
                RightAnim.SetActive(false);
                UpAnim.SetActive(false);
                LeftAnim.SetActive(true);
                DownAnim.SetActive(false);
                break;
            case TagList.directions.down:
                RightAnim.SetActive(false);
                UpAnim.SetActive(false);
                LeftAnim.SetActive(false);
                DownAnim.SetActive(true);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.gameObject.tag.Equals(TagList.healthpackTag))
            {
                RestoreHealth(25);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.swordDropTag))
            {
                weapon = TagList.weaponType.LaserSword;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.gunDropTag))
            {
                weapon = TagList.weaponType.RayGun;
                remainingAmmo = maxMagSize;

                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.NumPadTag)) {
                collision.GetComponent<NumPadScript>().StartHack();
            }
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) {
            health = 0;
            PlayerDeath();
        }
    }

    public void RestoreHealth(int hitpoints)
    {
        health += hitpoints;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void PlayerDeath()
    {
        _isDead = true;
        _generalManager.GameOver();
    }
    private void RespawnPlayer()
    {
        _rbody.position = new Vector2(0, 0);
        health = 100;
    }
}
