using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NumPadScript : MonoBehaviour
{
    public GeneralManagerScript generalManagerScript;
    public GameObject cellDoor;
    public PrisonerScript prisoner;
    private bool _isHacking;
    // Start is called before the first frame update
    void Start()
    {
        _isHacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHacking && !Input.GetKey(KeyCode.E)) {
            _isHacking = false;
        }
    }

    private void OnStartHack() {
        Invoke("OnFinishHack", 2);
    }

    private void OnFinishHack() {
        if (!_isHacking) {
            return;
        }
        Destroy(cellDoor);
        generalManagerScript.FreePrisoner();
        prisoner.Freed();
    }
}
