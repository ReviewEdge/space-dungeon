using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject _player;
    Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _target = _player.transform;

        transform.position = new Vector3 (_target.position.x, _target.position.y, -10);
    }
}
