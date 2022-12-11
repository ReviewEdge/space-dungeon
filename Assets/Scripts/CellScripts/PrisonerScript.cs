using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PrisonerScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rbody;
    private int x = 1;
    private int y = 0;
    public LayerMask layerMask;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
        direction = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        /*_rbody.velocity = direction;
        if (CheckForWall()) {
            print("turned");
            direction = new Vector2(x * -1, y);
        }*/
    }

   /* public bool CheckForWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rbody.position, direction, .6f, layerMask, -101, -99);
        return (hit.collider != null);
    }*/

    public void Freed() {
        Destroy(gameObject);
    }
}
