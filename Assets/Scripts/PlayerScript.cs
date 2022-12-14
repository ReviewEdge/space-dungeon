using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    RayGunScript _rayGun;
    RayGunScript _sniper;
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
    const int maxMagSize = 30;

    public Weapon[] weapons= {null,null,null};
    public Weapon currentWeapon;

    SpriteRenderer _srender;

    Vector2 direction = Vector2.zero;

    AudioSource _audioSource;
    public AudioClip HealthPickupSound;
    public AudioClip WeaponPickupSound;
    public AudioClip PlayerDeathSound;
    public AudioClip TakeDamageSound;
    public AudioClip EmptyMagSound;

    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();

        _srender = gameObject.GetComponent<SpriteRenderer>();

        Transform rayGunTransform = gameObject.transform.Find("RayGun");
        _rayGun = rayGunTransform.GetComponent<RayGunScript>();

        Transform sniperTransform = gameObject.transform.Find("Sniper");
        _sniper = sniperTransform.GetComponent<RayGunScript>();

        _laserSword = GetComponent<LaserSwordScript>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _audioSource = GetComponent<AudioSource>();
        _transform = GetComponent<Transform>();
        _walkingAnim = GetComponent<Animator>();

        LoadPlayerData();
    }

    private void Update()
    {
        if (!_isDead && !_generalManager.gamePaused)
        {
            //attack
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }

            //change weapon selection
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeSelectedWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeSelectedWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeSelectedWeapon(2);
            }
        }
        else {
            //stop player movement
            _rbody.velocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        if (!_isDead)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        //Move Player
        float x = _SPEED * Input.GetAxis("Horizontal");
        float y = _SPEED * Input.GetAxis("Vertical");
        _rbody.velocity = new Vector2(x, y);

        Vector2 movement = new Vector2(x, y);
        _rbody.velocity = movement * moveSpeed;

        if (x != 0 || y != 0) {
            direction = new Vector2(x, y);
        }

        //Rotate player
        float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        _transform.eulerAngles = new Vector3(0, 0, degress - 90);
        _mainCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        _walkingAnim.SetBool("isMoving", _rbody.velocity.magnitude != 0);
    }

    private void Attack() {
        Vector3 mouseLocation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -_mainCamera.transform.position.z);
        
        switch (currentWeapon.weaponType)
        {
            case WeaponType.LaserSword:
                _laserSword.SwingLaserSword(mouseLocation);
                break;
            case WeaponType.RayGun:
                if (currentWeapon.ammo > 0)
                {
                    _rayGun.ShootRayGun(mouseLocation);
                    currentWeapon.ammo--;
                }
                else {
                    _audioSource.PlayOneShot(EmptyMagSound);
                }
                break;
            case WeaponType.Sniper:
                if (currentWeapon.ammo > 0)
                {
                    _sniper.ShootRayGun(mouseLocation);
                    currentWeapon.ammo--;
                }
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
                WeaponPickup(WeaponType.LaserSword);
                _audioSource.PlayOneShot(WeaponPickupSound);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.gunDropTag))
            {
                WeaponPickup(WeaponType.RayGun, maxMagSize);
                _audioSource.PlayOneShot(WeaponPickupSound);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag.Equals(TagList.sniperDropTag))
            {
                WeaponPickup(WeaponType.Sniper, maxMagSize);
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
        if(!_isDead)
        {
            health -= damage;

            if (health <= 0 && !_isDead) {
                health = 0;
                PlayerDeath();
            }

            _audioSource.PlayOneShot(TakeDamageSound, 2);
            StartCoroutine(AnimationColorFlash(Color.red, 0.1f));
        }
    }

    public void ChangeSelectedWeapon(int weaponIndex) {
        currentWeapon = weapons[weaponIndex];

        switch (currentWeapon.weaponType)
        {
            case WeaponType.LaserSword:
                _rayGun.GetComponent<SpriteRenderer>().enabled = false;
                _sniper.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case WeaponType.RayGun:
                _rayGun.GetComponent<SpriteRenderer>().enabled = true;
                _sniper.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case WeaponType.Sniper:
                _rayGun.GetComponent<SpriteRenderer>().enabled = false;
                _sniper.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case WeaponType.Punch:
                _rayGun.GetComponent<SpriteRenderer>().enabled = false;
                _sniper.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    public void WeaponPickup(WeaponType weaponType, int ammo = 0) {
        if ((int)weapons[(int)weaponType].weaponType == (int)weaponType) {
            weapons[(int)weaponType].ammo += ammo;
        } else { 
            weapons[(int)weaponType] = new Weapon(weaponType, ammo);
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
        _srender.enabled = false;
        _rayGun.GetComponent<SpriteRenderer>().enabled = false;
        _sniper.GetComponent<SpriteRenderer>().enabled = false;
        _audioSource.PlayOneShot(PlayerDeathSound);
        _generalManager.EnemyDeath(transform.position);

        StartCoroutine(_generalManager.GameOver());
    }

    public  void RespawnPlayer()
    {
        _isDead = false;
        _srender.enabled = true;
        ChangeSelectedWeapon(0);
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

    private void LoadPlayerData()
    {
        int health = 100;
        if (PlayerPrefs.HasKey("Health"))
        {
            health = PlayerPrefs.GetInt("Health");
        }
        this.health = health;

        for (int i = 0; i < weapons.Length; i++)
        {
            int weaponEnumNumber = 0;
            if (PlayerPrefs.HasKey("Weapon" + (i + 1)))
            {
                weaponEnumNumber = PlayerPrefs.GetInt("Weapon" + (i + 1));
            }

            int weaponAmmo = 0;
            if (PlayerPrefs.HasKey("Ammo" + (i + 1)))
            {
                weaponAmmo = PlayerPrefs.GetInt("Ammo" + (i + 1));
            }

            weapons[i] = new Weapon((WeaponType)weaponEnumNumber, weaponAmmo);
        }

        int selectedWeaponIndex = 0;
        if (PlayerPrefs.HasKey("selectedWeaponIndex"))
        {
            selectedWeaponIndex = PlayerPrefs.GetInt("selectedWeaponIndex");
        }
        ChangeSelectedWeapon(selectedWeaponIndex);
    }
}
