using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotGuardScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public int _health;
    public float _speed;
    public GameObject _player;
    RayGunScript _rayGun;
    LaserSwordScript _laserSword;
    public GameObject _laserSwordDropPrefab;
    public GameObject _rayGunDropPrefab;
    public GameObject _healthPackDropPrefab;
    [SerializeField] TagList.weaponType weapon;
    public GameObject _floatingTextDamagePrefab;
    GeneralManagerScript _generalManager;
    AudioSource _audioSource;
    public AudioClip DeathNoise;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = gameObject.GetComponent<Rigidbody2D>();
        _rayGun = GetComponent<RayGunScript>();
        _laserSword = GetComponent<LaserSwordScript>();
        _player = GameObject.FindWithTag(TagList.playerTag);
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = _player.transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        
        switch (weapon)
        {
            case TagList.weaponType.LaserSword:
                if ((_rbody.position.x - target.position.x) < 1 && (_rbody.position.y - target.position.y) < 1) {
                    _laserSword.SwingLaserSword(target.position);
                }
                break;
            case TagList.weaponType.RayGun:
                _rayGun.ShootRayGun(target.position);
                break;
        }
    }

    void Die()
    {
        _generalManager.EnemyDeath(transform.position);

        int ran = Random.Range(1, 7);

        // occasionally drop health pack
        if (ran == 1)
        {
            Instantiate(_healthPackDropPrefab, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            if (TagList.weaponType.LaserSword == weapon)
            {
                Instantiate(_laserSwordDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            }
            else if (TagList.weaponType.RayGun == weapon)
            {
                Instantiate(_rayGunDropPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            }
        }


        _generalManager.IncrementScore(50);

        _audioSource.volume = 0.75f;
        _audioSource.PlayOneShot(DeathNoise);
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 2);
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
    }

}
