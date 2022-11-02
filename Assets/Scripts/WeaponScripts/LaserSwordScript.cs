using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwordScript : MonoBehaviour
{
    public int _damage;
    public float attackSpeed;
    public LayerMask _foesLayer;
    private float _timeBtwAttack;
    private Transform _entityLocation;

    public GameObject SwordSwipe8; 
    AudioSource _audioSource;
    public AudioClip SwordSwingNoise;

    // Start is called before the first frame update
    void Start()
    {
        _entityLocation = transform;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeBtwAttack -= Time.deltaTime;
    }

    public void SwingLaserSword(Vector3 aimingAt) {
        if (_timeBtwAttack > 0)
        {
            return;
        }

        if (SwordSwipe8 != null)
        {
            SwordSwipe8.GetComponent<Animator>().Play("Base Layer.SwordSwipe8");
        }

        _audioSource.PlayOneShot(SwordSwingNoise);
        

        Vector3 directionalVector = (aimingAt + (-_entityLocation.position)).normalized;
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_entityLocation.position + directionalVector, .6f, _foesLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            if (enemiesToDamage[i].tag == TagList.enemyTag)
            {
                enemiesToDamage[i].GetComponent<RobotGuardScript>().TakeDamage(_damage);
            }
            else if (enemiesToDamage[i].tag == TagList.playerTag)
            {
                enemiesToDamage[i].GetComponent<PlayerScript>().TakeDamage(_damage);
            }
        }

        _timeBtwAttack = attackSpeed;
    }
}
