using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    MeshRenderer _popupText;
    GameObject _popupChild;

    // Start is called before the first frame update
    void Start()
    {
        _popupText = GetComponentInChildren<MeshRenderer>();
        _popupChild = gameObject.transform.Find("PopupText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // spinny spinny
        gameObject.transform.Rotate( 0, Time.deltaTime*96f, 0);
        
        // stops popup text from spinning
        _popupChild.transform.Rotate(0, -Time.deltaTime*96f, 0);
    }

    public void LateUpdate() {
        // stops popup text from spinning
        _popupChild.transform.position = gameObject.transform.position;
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
