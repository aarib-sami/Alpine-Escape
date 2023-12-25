using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    [SerializeField] GameObject scoreReferal;
   
    void Start()
    {
       
    }

    
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "High Score:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        
        int Currentscore = scoreReferal.GetComponent<DestroyerAndScore>().score;
       
      //  PlayerPrefs.SetInt("HighScore", Currentscore);
        
        if (Currentscore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Currentscore);
        }
    }
}
