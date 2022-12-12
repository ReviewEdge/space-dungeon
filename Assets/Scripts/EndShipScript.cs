using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndShipScript : MonoBehaviour
{
    Animator animator;
    GeneralManagerScript _generalManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.playerTag))
        {
            animator.SetBool("ExitAnim", true);
            /*StartCoroutine(_generalManager.Victory());*/
            _generalManager.Victory();
        }
    }
}
