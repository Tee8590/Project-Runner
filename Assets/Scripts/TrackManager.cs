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

    //private List<GameObject> currentTile;
    private Queue<GameObject> currentTile = new Queue<GameObject>();
    private List<GameObject> tilePool;
    private List<GameObject> currentObstacles;

    [SerializeField]
    private Camera mainCam;

    void Start()
    {
        currentTile      = new Queue<GameObject>();

        currentObstacles = new List<GameObject>();

        tilePool = new List<GameObject>();

        Random.InitState(System.DateTime.Now.Millisecond);

        for(int i =0; i<tilesStartCount; i++)
        {
            SpawnTiles(tiles.GetComponent<Tile>(), false);
        }
    }

    void Update()
    {

        MoveTile();


    }
   public void SpawnTiles(Tile tile, bool SpawnObstacles)
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

            if (mainCam.WorldToViewportPoint(tile.transform.position).z < 0)
            {
                // Move the tile to the end of the queue
                Vector3 newPos = new Vector3(0, 0, 20);
                tile.transform.position = currentTile.Last().transform.position + newPos; //Places tile at the end of queue
            }
            currentTile.Enqueue(tile); //Adds the tile back to the queue
        }
    }
}
