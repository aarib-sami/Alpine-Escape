using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject TitleMove;
    private void Update()
    {
       
        
        
        if (Input.GetMouseButtonDown(0) && TitleMove.GetComponent<TitleMove>().moveDone == true)
        {
            SceneManager.LoadScene("CoreGame");
            
        }
    }
}
