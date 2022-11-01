using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NumPadScript : MonoBehaviour
{
    public GameObject cellDoor;
    public PrisonerScript prisoner;
    private bool _isHacking;
    private MeshRenderer _popupText;
    GeneralManagerScript _generalManager;
    // Start is called before the first frame update
    void Start()
    {
        _isHacking = false;
        _popupText = GetComponentInChildren<MeshRenderer>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHacking && !Input.GetKey(KeyCode.E)) {
            _isHacking = false;
        }
    }

    public void StartHack() {
        if (_isHacking) {
            return;
        }

        _isHacking = true;
        Invoke("OnFinishHack", 2);
    }

    private void OnFinishHack()
    {
        if (!_isHacking)
        {
            return;
        }

        Destroy(cellDoor);
        _generalManager.IncrementScore(1000);
        _generalManager.FreePrisoner();
        prisoner.Freed();
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