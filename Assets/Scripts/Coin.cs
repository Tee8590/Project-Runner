using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float turnSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Obstacles>() !=null)
        {
            Destroy(gameObject);
            return;
        }
        if(other.gameObject.name == "Kill")
        {
            Debug.Log("Coin Killed");
            Destroy(gameObject);
        }
        if(other.gameObject.name != "Player")
        {
            return;
        }

        GameManager.instance.IncreaseScore();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
