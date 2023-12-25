using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleMove : MonoBehaviour
{
    public bool moveDone = false;
    private Vector2 nextPos;


    void Update()
    {

        nextPos = new Vector2(0.0f, 3.2f);
        transform.position = Vector2.MoveTowards(transform.position, nextPos, 1.7f * Time.deltaTime);
        Invoke("makeDoneTrue", 0.9f);
    }

    void makeDoneTrue ()
    {
        moveDone = true;
    }


}
