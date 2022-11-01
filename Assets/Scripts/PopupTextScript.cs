using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextScript : MonoBehaviour
{
    private MeshRenderer _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("thing detected");
        if (collision.tag == TagList.playerTag) {
            _text.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("hey");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagList.playerTag)
        {
            _text.enabled = false;
        }
    }

    private void ShowPopUpText()
    {

    }

    private void HidePopUpText() {
    
    }
}
