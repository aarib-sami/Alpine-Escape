using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blink : MonoBehaviour
{

    [SerializeField] Color tranparent;
    [SerializeField] private Color normal;

    private void Start()
    {
    StartCoroutine(Blinker());
    }

    IEnumerator Blinker()
    {
        
        while (true)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = tranparent;
            yield return new WaitForSeconds(0.4f);
            gameObject.GetComponent<TextMeshProUGUI>().color = normal; 
            yield return new WaitForSeconds(1);
        }
    }
}
