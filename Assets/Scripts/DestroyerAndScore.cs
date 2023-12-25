using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAndScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int score;
    [SerializeField] GameObject player;
    bool addScore = true;

    private void Update()
    {
     if(player.GetComponent<Player>().hasDied == true)
        {
            addScore = false;
        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);  
            if(addScore == true)
            {
                score++;
            }
           
        }
    }


}
