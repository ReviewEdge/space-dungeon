using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private int _damage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag) {
            case TagList.enemyTag:
                collision.GetComponent<RobotGuardScript>().TakeDamage(_damage);
                break;
            case TagList.playerTag:
                collision.GetComponent<PlayerScript>().TakeDamage(_damage);
                break;
            case TagList.wallTag:
                break;
        }
        Destroy(gameObject);
    }

    public void SetDamage(int damage) {
        _damage = damage;
        _damage += Random.Range(-5, 6);
    }
}
