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
    // Start is called before the first frame update
    void Start()
    {
        _entityLocation = transform;
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

        Vector3 directionalVector = (aimingAt + (-_entityLocation.position)).normalized;
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_entityLocation.position + directionalVector, 1f, _foesLayer);
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
