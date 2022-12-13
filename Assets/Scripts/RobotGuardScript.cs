using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotGuardScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public int _health;
    public float _speed;
    PlayerScript _player;
    public Transform _playerTransform;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public GameObject _laserSwordDropPrefab;
    public GameObject _rayGunDropPrefab;
    public GameObject _sniperDropPrefab;
    public GameObject _healthPackDropPrefab;
    public GameObject _speedUpDropPrefab;
    [SerializeField] WeaponType weapon;
    public GameObject _floatingTextDamagePrefab;
    GeneralManagerScript _generalManager;
    AudioSource _audioSource;
    public AudioClip DeathNoise;
    public LayerMask layerMask;
    public float _chaseTime;
    float _lastSpotted;
    Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _rbody = gameObject.GetComponent<Rigidbody2D>();
        _rayGun = GetComponent<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
        _player = GameObject.FindWithTag(TagList.playerTag).GetComponent<PlayerScript>();
        _playerTransform = GameObject.FindWithTag(TagList.playerTag).GetComponent<Transform>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((HasLineOfSight() || _lastSpotted > 0) && !_player._isDead) {
            Move();
            Attack();
        }
        _lastSpotted -= Time.deltaTime;
    }
    void Move()
    {
        switch (weapon)
        {
            case WeaponType.LaserSword:
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime);
                break;
            case WeaponType.RayGun:
                transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime);
                break;
            case WeaponType.Sniper:
                //stand still and shoot
                break;
        }
    }

    void Attack()
    {
        _animator.SetBool("isAttacking", true);
        switch (weapon)
        {
            case WeaponType.LaserSword:
                if ((_rbody.position.x - _playerTransform.position.x) < 1 && (_rbody.position.y - _playerTransform.position.y) < 1)
                {
                    _laserSword.SwingLaserSword(_playerTransform.position);
                }
                break;
            case WeaponType.RayGun:
                _rayGun.ShootRayGun(_playerTransform.position);
                break;
            case WeaponType.Sniper:
                _rayGun.ShootRayGun(_playerTransform.position);
                break;
        }
    }

    public bool HasLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rbody.position, _playerTransform.position - transform.position, Mathf.Infinity, layerMask);
        if (hit.collider.tag.Equals(TagList.playerTag)) { 
            _lastSpotted = _chaseTime;
            return true;
        }
        return false;
    }

    void Die()
    {
        _generalManager.EnemyDeath(transform.position);

        int ran = Random.Range(1, 78);

        // occasionally drop health pack
        if (ran <= 11)
        {
            Instantiate(_healthPackDropPrefab, gameObject.transform.position, Quaternion.identity);
        } else if(ran >= 72)
        {
            Instantiate(_speedUpDropPrefab, gameObject.transform.position, Quaternion.identity);
        } else
        {
            switch (weapon)
            {
                case WeaponType.LaserSword:
                    Instantiate(_laserSwordDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    break;
                case WeaponType.RayGun:
                    Instantiate(_rayGunDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    break;
                case WeaponType.Sniper:
                    Instantiate(_sniperDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    break;
            }
        }

        _generalManager.IncrementScore(50);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        _health -= damage;
        GameObject floatingDamageText = Instantiate(_floatingTextDamagePrefab, _rbody.position, Quaternion.identity);
        floatingDamageText.GetComponentInChildren<TextMesh>().text = "-" + damage;
        _generalManager.IncrementScore(damage);
        if (_health <= 0)
        {
            Die();
        }
        StartCoroutine(ColorFlash(Color.red, 0.075f));
    }

    IEnumerator ColorFlash(Color32 flashColor, float flashTime)
    {
        Color32 originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = flashColor;
        yield return new WaitForSeconds(flashTime);
        GetComponent<SpriteRenderer>().color = originalColor;
    }

}
