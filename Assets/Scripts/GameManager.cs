using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public static GameManager instance;
    public TextMeshProUGUI scoreText;

    //public List<GameObject> coins;
    //public List<GameObject> spawnReadyCoin;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("null instance");
    }
    private void Start()
    {
        //coins = new List<GameObject>();
        //spawnReadyCoin = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        //RemoveCoins();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }
    //void RemoveCoins()
    //{
    //    coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));
    //    for (int i = coins.Count - 1; i >= 0; i--) // Iterate backwards
    //    {
           
    //        if (coins[i].transform.position.z < -10f) // Check world position
    //        {
    //            GameObject expiredCoin = coins[i];
    //            spawnReadyCoin.Add(expiredCoin);
    //            coins.RemoveAt(i); // Remove from list
    //            Destroy(expiredCoin); // Destroy GameObject
    //        }
    //    }
    //}
}
