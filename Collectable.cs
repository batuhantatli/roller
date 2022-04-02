using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public GameObject crushedCollectable;
    public int power;
    public List<Texture> crushedSurfaceBaseMap = new List<Texture>();

    public void AfterObstacleDestroyed(Transform spawnPoint,GameObject crushedCollectable)
    {
        GameObject a =  Instantiate(crushedCollectable , new Vector3(spawnPoint.transform.position.x , 0.01f,spawnPoint.transform.position.z) , Quaternion.identity);
        a.GetComponent<Renderer>().material.mainTexture = crushedSurfaceBaseMap[Random.Range(0,crushedSurfaceBaseMap.Count)];
    }

}
