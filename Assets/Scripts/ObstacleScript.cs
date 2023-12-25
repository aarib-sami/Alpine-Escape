using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

   

    

}
    