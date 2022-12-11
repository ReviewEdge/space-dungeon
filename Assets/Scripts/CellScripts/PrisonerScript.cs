using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PrisonerScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rbody;
    private float x = 1;
    private float y = 0;
    public LayerMask layerMask;
    Transform _transform;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        direction = new Vector2(x, y);

        //Rotate prisoner to starting direction
        float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        _transform.eulerAngles = new Vector3(0, 0, degress - 90);
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.velocity = direction;
        if (CheckForWall())
        {
            //Update Direction
            x = Random.Range(0f, 1f);
            y = Random.Range(0f, 1f);
            if (Random.Range(0, 2) == 0) {
                x *= -1;
            }
            if (Random.Range(0, 2) == 0)
            {
                y *= -1;
            }
            direction = new Vector2(x, y);

            //Rotate prisoner
            float degress = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            _transform.eulerAngles = new Vector3(0, 0, degress - 90);
        }
    }

    public bool CheckForWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rbody.position, direction, 1f, layerMask, 0, 0);
        RaycastHit2D hitRight = Physics2D.Raycast(_rbody.position, direction * Vector2.right, .5f, layerMask, 0, 0);
        RaycastHit2D hitLeft = Physics2D.Raycast(_rbody.position, direction * Vector2.left, .5f, layerMask, 0, 0);
        return hit.collider != null || hitLeft.collider != null || hitRight.collider != null;
    }

    public void Freed() {
        Destroy(gameObject);
    }
}
