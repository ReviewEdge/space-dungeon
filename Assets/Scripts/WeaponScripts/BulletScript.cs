using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int _damage;
    // Start is called before the first frame update
    void Start()
    {
        _damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == TagList.enemyTag) {
            collision.collider.GetComponent<RobotGuardScript>().TakeDamage(_damage);
        } else if (collision.collider.tag == TagList.playerTag) {
            collision.collider.GetComponent<PlayerScript>().TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
