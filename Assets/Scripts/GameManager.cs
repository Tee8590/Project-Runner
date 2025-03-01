using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    
    public static GameManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject enemyPrefab = null;
    private int obstacleHitCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("null instance");
    }
    private void Start()
    {
       
     
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }
    public void OnPlayerHitObstacle()
    {
        obstacleHitCount++; // Increase count
        enemyPrefab.transform.position += Vector3.forward * 10f;
    }
    public void GameOver()
    {
        
        Debug.Log("GameOver");
    }

}
