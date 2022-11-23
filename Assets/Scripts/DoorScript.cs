using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    GeneralManagerScript _generalManager;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        _generalManager = FindObjectOfType<GeneralManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Open() 
    {
        isOpen = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen && collision.gameObject.tag.Equals(TagList.playerTag))
        {
            _generalManager.LoadNextLevel();
        }
    }
}
