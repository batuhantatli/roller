using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 vc;


    void Start()
    {
        vc = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y , Player.transform.position.z + vc.z );
    }
}
