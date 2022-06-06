using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLenght = 80;
    public int numberOfTiles = 5;
    public Transform playerTransform;
    private void Start()
    {

        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));

            }
        }
    }
    private void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfTiles * tileLenght))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }
   /* private void OnTriggerEnter(Collider other)
    {
        transform.position += new Vector3(0, 0, transform.GetChild(0).GetComponent<Renderer>().bounds.size.z * 5);
    }*/

    public void SpawnTile(int tileindex)
    {
       GameObject go = Instantiate(tilePrefabs[tileindex], transform.forward * zSpawn * 2, transform.rotation);
        zSpawn += tileLenght;
    }
    public void DestroyerRoad()
    {
        
    }
}
