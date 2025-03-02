using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //[SerializeField]
    //private Transform startPos;
    //[SerializeField]
    //private Transform spawnPos;

    private Vector3 TileLocation;
    public GameObject coinPrefab;
    public int coinSpawnTimer = 10;
    private float timer = 0f;

    public GameObject obstaclePrefab;
    private Vector3 obsScale;
    void Start()
    {
        obsScale = obstaclePrefab.transform.localScale;
        //GameObject  renderer = gameObject.GetComponent<Renderer>(); 
        SpawnObs();
        SpawnCoins();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > coinSpawnTimer)
        {
            SpawnObs();
            SpawnCoins();
            timer = 0f;
           
        }
    }
    void SpawnCoins()
    {
        int coinToSpawn = 5;
        for(int i=0; i<coinToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab);
            temp.transform.position = RandomPointInCollider(GetComponent<Collider>());
            temp.transform.SetParent(transform);
            //coins.Add(temp);
            //Debug.Log("coins"+coins);
        }

    }
    
    Vector3 RandomPointInCollider(Collider col)
    {
        Vector3 point = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
                                     Random.Range(col.bounds.min.y, col.bounds.max.y),
                                     Random.Range(col.bounds.min.y, col.bounds.max.z));

       /* if (point != col.ClosestPoint(point))
            point = RandomPointInCollider(col);*/
        point.y = 1;
        return point;

    }
    void SpawnObs()
    {
        int obstacleIndex = Random.Range(0, 3);
        Transform spawnPoint = transform.GetChild(obstacleIndex).transform;
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        obstacle.transform.SetParent(transform, true);
    }
}
