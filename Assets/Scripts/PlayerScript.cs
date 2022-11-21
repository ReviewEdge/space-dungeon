using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    Animator _swordSwipe;
    Animator _walkingAnim;
    public Camera _mainCamera;
    GeneralManagerScript _generalManager;
    Transform _transform;

    public int health = 100;
    const int maxHealth = 100;
    public bool _isDead = false;
    public float _SPEED = 5;
    public float moveSpeed; //speed var
    public float roll; //roll distance
    public int magazineAmmo;
    public int remainingAmmo = 0;
    const int maxMagSize = 30;

    public TagList.weaponType weapon;
    Vector2 direction = Vector2.zero;

    AudioSource _audioSource;
    public AudioClip HealthPickupSound;
    public AudioClip WeaponPickupSound;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rayGun = GetComponentInChildren<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _audioSource = GetComponent<AudioSource>();
        _isDead = false;
        _transform = GetComponent<Transform>();
        _walkingAnim = GetComponent<Animator>();
        LoadPrevData();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
            switch (weapon)
            {
                case TagList.weaponType.LaserSword:
                    _laserSword.SwingLaserSword(mouseLocation);
                    break;
                case TagList.weaponType.RayGun:
                    _rayGun.ShootRayGun(mouseLocation);
                    remainingAmmo--;
                    if (remainingAmmo <= 0)
                    {
                        ChangeWeapon(TagList.weaponType.LaserSword);
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

        if (x != 0 || y != 0) {
            direction = new Vector2(x, y);
        }
    }
    private void ChangePlayerDirection()
    {
        float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        _transform.eulerAngles = new Vector3(0, 0, degress - 90);
        _mainCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        _walkingAnim.SetBool("isMoving", _rbody.velocity.magnitude != 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.gameObject.tag.Equals(TagList.healthpackTag))
            {
                RestoreHealth(25);
                _audioSource.PlayOneShot(HealthPickupSound);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.speedUpTag))
            {
                SpeedUp();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.swordDropTag))
            {
                ChangeWeapon(TagList.weaponType.LaserSword);
                _audioSource.PlayOneShot(WeaponPickupSound);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.gunDropTag))
            {
                ChangeWeapon(TagList.weaponType.RayGun);
                _audioSource.PlayOneShot(WeaponPickupSound);
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

        StartCoroutine(AnimationColorFlash(Color.red, 0.1f));
    }

    public void ChangeWeapon(TagList.weaponType weapon, int ammo = -1) {
        this.weapon = weapon;
        switch (weapon)
        {
            case TagList.weaponType.LaserSword:
                remainingAmmo = 0;
                _rayGun.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case TagList.weaponType.RayGun:
                if (ammo == -1)
                {
                    remainingAmmo = maxMagSize;
                }
                else 
                {
                    remainingAmmo = ammo;
                }
                _rayGun.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
    }

    IEnumerator AnimationColorFlash(Color32 flashColor, float flashTime)
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        // gets child (animation) sprite renderers
        foreach(SpriteRenderer sprite in sprites) 
        {
            sprite.color = flashColor;
        }
        yield return new WaitForSeconds(flashTime);
        foreach(SpriteRenderer sprite in sprites) 
        {
            sprite.color = Color.white;
        }
    }

    public void RestoreHealth(int hitpoints)
    {
        health += hitpoints;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        StartCoroutine(AnimationColorFlash(Color.green, 0.3f));
    }
    private void PlayerDeath()
    {
        _isDead = true;

        _generalManager.GameOver();
    }
    public  void RespawnPlayer()
    {
        _rbody.position = new Vector2(0, 0);
        health = 100;
    }

    private void SpeedUp()
    {
        _SPEED = 12;
        StartCoroutine(AnimationColorFlash(Color.yellow, 3f));
        Invoke("ResetSpeed", 3);
    }

    private void ResetSpeed()
    {
        _SPEED = 5;
    }

    private void LoadPrevData() {
        int weapon = 0;
        if (PlayerPrefs.HasKey("Weapon"))
        {
            weapon = PlayerPrefs.GetInt("Weapon");
        }
        switch (weapon)
        {
            case 0:
                ChangeWeapon(TagList.weaponType.LaserSword);
                break;
            case 1:
                int ammo = -1;
                if (PlayerPrefs.HasKey("Ammo"))
                {
                    ammo = PlayerPrefs.GetInt("Ammo");
                }

                ChangeWeapon(TagList.weaponType.RayGun, ammo);
                break;
        }

        int health = 100;
        if (PlayerPrefs.HasKey("Health"))
        {
            health = PlayerPrefs.GetInt("Health");
        }
        this.health = health;
    }
}
