using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rbody;
    private int x = 1;
    private int y = 0;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.velocity = new Vector2(x, y);
        RaycastHit2D findWall = Physics2D.Raycast(_rbody.position, Vector2.right * x, .001f, layerMask);
        if (findWall.collider != null) {
            x = x * -1;
        }
    }

    public void Freed() {
        Destroy(gameObject);
    }
}
