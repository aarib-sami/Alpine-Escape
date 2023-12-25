using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScriptGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject scoreReferal;
    [SerializeField] GameObject player;
    [SerializeField] int score;
    
    private void Update()
    {
        score = scoreReferal.GetComponent<DestroyerAndScore>().score;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score:" + score.ToString();

        if (player.GetComponent<Player>().hasDied == true)
        {
            gameObject.SetActive(true);
        }
    }

}






