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

    private List<GameObject> currentTile;
    private List<GameObject> tilePool;
    private List<GameObject> currentObstacles;

    [SerializeField]
    private Camera mainCam;

    void Start()
    {
        currentTile      = new List<GameObject>();

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
        currentTile.Add(prevTile);
        currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);
    }
    public void MoveTile()
    {
        foreach (GameObject tile in currentTile)
        {
            tile.transform.Translate(-tile.transform.forward * Time.deltaTime * moveSpeed, Space.World);
           if(mainCam.WorldToViewportPoint(tile.transform.position).z < 0)
           {
                GameObject tileTmp = tile;
                int currentIndex = (currentTile.IndexOf(tile));
                currentTile.RemoveAt(currentIndex);
                Vector3 newPos = new Vector3(0, 0, 20);
                tileTmp.transform.position = currentTile[currentTile.Count - 1].transform.position + newPos ;
                currentTile.Add(tileTmp);
                
              
              
            }
        } 
    }
}
