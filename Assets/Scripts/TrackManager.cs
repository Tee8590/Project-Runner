using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
   
    [SerializeField]
    private int tilesStartCount = 10;
   
    [SerializeField]
    private List<GameObject> obstacles;

    [SerializeField]
    private GameObject tiles; //prefab
  /*  [SerializeField]
    private int poolSize;*/
    [SerializeField]
    private bool expandable;
    private float moveSpeed = 5f;
    private Vector3 currentTileLocation = Vector3.zero;
    private Vector3 currentTileDirection = Vector3.forward;
    private GameObject prevTile;
    private bool spawnObstacles = false;

    public GameObject player;

    //private List<GameObject> currentTile;
    private Queue<GameObject> currentTile = new Queue<GameObject>();
    private List<GameObject> tilePool;
    private List<GameObject> currentObstacles;
   

    [SerializeField]
    private Camera mainCam;
    private float timer = 0f;
    private int waitTime = 15;
    void Start()
    {
        currentTile      = new Queue<GameObject>();

        currentObstacles = new List<GameObject>();

        tilePool = new List<GameObject>();

        Random.InitState(System.DateTime.Now.Millisecond);

        for(int i =0; i<tilesStartCount; i++)
        {
            SpawnTiles(tiles.GetComponent<Tile>(), true);
        }
        
    }

    void Update()
    {
        //timer = Time.deltaTime;
        //if(timer>waitTime)
        //{
        //    spawnObstacles = true;
           
        //    timer = 0f;

        //}
        MoveTile();
        //spawnObstacles = false;

    }
   public void SpawnTiles(Tile tile, bool spawnObstacles)
    {
        prevTile = GameObject.Instantiate(tile.gameObject, currentTileLocation, Quaternion.identity);
        currentTile.Enqueue(prevTile);

       

        currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);
    }
    public void MoveTile()
    {
        int queueCount = currentTile.Count;

        for (int i = 0; i < queueCount; i++)
        {
            GameObject tile = currentTile.Dequeue(); //Removes the first tile
            tile.transform.Translate(-tile.transform.forward * Time.deltaTime * moveSpeed, Space.World);

           if (tile.transform.position.z <= -10f)
            {
                // Remove obstacles before moving the tile
                //DeActivatePreviousObs();

                
                // Move the tile to the end of the queue
                Vector3 newPos = new Vector3(0, 0, 20);
                tile.transform.position = currentTile.Last().transform.position + newPos; //Places tile at the end of queue

              
            }
            currentTile.Enqueue(tile); //Adds the tile back to the queue
        }
       
     }

    //public void SpawnObs()
    //{
    //    if (Random.value > 0.2f) return;
    //    GameObject lastTile = currentTile.Last();
    //    GameObject obstaclePrefab = SelectRandomGameObjectFromList(obstacles);
    //    Quaternion newObjectRotation = obstaclePrefab.gameObject.transform.rotation * 
    //                                                     Quaternion.LookRotation(currentTileDirection, Vector3.up);
    //    Vector3 spawnPosition = lastTile.transform.position + new Vector3(0, 0.5f, 0);
    //    GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, newObjectRotation);
    //    obstacle.transform.SetParent(lastTile.transform);
    //    currentObstacles.Add(obstacle);
    //}
    public GameObject SelectRandomGameObjectFromList(List<GameObject> list)
    {
        if(list.Count ==0) return null;
        return list[Random.Range(0, list.Count)];
    }
   

    //private void DeActivatePreviousObs()
    //{

    //    while(currentObstacles.Count != 0)
    //    {
    //        Debug.Log(currentObstacles.Count);
    //for(int i =0; i <currentObstacles.Count; i++)
    //{
    //    if (currentObstacles[i].gameObject.transform.position.z ==-10f)
    //    {
    //        GameObject obstacle = currentObstacles[i];
    //        currentObstacles.RemoveAt(i);
    //        Destroy(obstacle);
    //    }
    //}
    //}
    //while(currentObstacles.Count != 0)
    //{
    //    GameObject obstacle = currentObstacles[0];
    //    currentObstacles.RemoveAt(0);
    //    Destroy(obstacle);
    //}
    //}
}
