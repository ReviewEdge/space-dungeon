using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    private MeshRenderer _popupText;
    // Start is called before the first frame update
    void Start()
    {
        _popupText = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // spinny spinny
        gameObject.transform.Rotate( 0, Time.deltaTime*96f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagList.playerTag)
        {
            _popupText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagList.playerTag)
        {
            _popupText.enabled = false;
        }
    }
}
