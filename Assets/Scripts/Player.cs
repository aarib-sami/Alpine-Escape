using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
   
    [Header("Jumping")]
    [SerializeField]  bool isGrounded;
    private bool isJumping;
    private bool charging;
    [SerializeField] public float jumpTime;
    [SerializeField] public float jumpForce;
    [SerializeField] public float checkRadius;
    [SerializeField] Sprite[] JumpPosition;
   
    [Header("Mobile Support")]
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 startTouch, swipeDelta;    
    
    [Header("Death")]
    [SerializeField] public bool hasDied = false;
    
    [Header("Components")]
    [SerializeField] GameObject particles;
    [SerializeField] public Transform feetPos;
    [SerializeField] public LayerMask whatIsGround;
    private Rigidbody2D rb;

    [Header("Other")]
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject scoreObject;
    [SerializeField] GameObject swipeManager;
    [SerializeField] GameObject destroyer;
    [SerializeField] ObstacleScript obstacle;
    private bool isDraging = false;
    bool isRotated;
    private Shake shake;

    float lerpDuration;

    Coroutine lastRoutine = null;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOver.SetActive(false);
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    void Update()
    {

     // method calling 
     TouchCharge();
     SpaceCharge();
     SpaceJump();
     TouchJump();
     SpacePlayerDown();
     //TouchPlayerDown();
     //Jumpsprite();
     Particles();
     StartCoroutine(Jumpsprite());
     //other
     isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }


         
    
    
    // methods  
  
    void SpaceJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {

            charging = false;      
            GetComponent<SpriteRenderer>().sprite = JumpPosition[1];
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce * jumpTime;          
                lerpDuration = (float)(-rb.velocity.y / -9.81);
                StartCoroutine(ShakeCamera());
            }

            lastRoutine = StartCoroutine(Rotate360());
            jumpTime = 0f;
        }

    }


    IEnumerator Rotate360()
    {
        float startRot = 0;
        float targetRot = 360;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        while (timeElapsed < lerpDuration)
        {
            float degrees = Mathf.Lerp(startRot, targetRot, timeElapsed / lerpDuration);
            transform.rotation = startRotation * Quaternion.Euler(0, 0, degrees);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // back to the original rotation after rotating 360 degrees!
        transform.rotation = startRotation;
    }





    void TouchJump()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                GetComponent<SpriteRenderer>().sprite = JumpPosition[1];
                charging = false;
                if (isGrounded == true)
                {
                    rb.velocity = Vector2.up * jumpForce * jumpTime;
                }
                jumpTime = 0f;
            }
                    
        }
    }
           
    void TouchCharge()
            {
                 
                if (Input.touchCount > 0)
                {

                    Touch first = Input.GetTouch(0);

                    if (first.phase == TouchPhase.Stationary || Input.GetKey(KeyCode.Space))
                    {
                        if (isGrounded == true)
                        {
                            GetComponent<SpriteRenderer>().sprite = JumpPosition[0];
                        }
                        charging = true;

                            if (charging == true)
                            {
                                jumpTime += Time.deltaTime;
                                if (jumpTime >= 0.15f)
                                {
                                    jumpTime = 0.15f;
                                }
                            }
                    }
                    
                }
            }
            
    void SpaceCharge()
    {

        if (Input.GetKey(KeyCode.Space))
        {
                if (isGrounded == true)
                {
                    GetComponent<SpriteRenderer>().sprite = JumpPosition[0];
                } 
                charging = true;
                if (charging == true)
                {
                    jumpTime += Time.deltaTime;
                    if (jumpTime >= 0.15f)
                    {
                        jumpTime = 0.15f;
                    }
                }                    
        }
    }
           
    void SpacePlayerDown()
    {
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded == false)
        {
        swipeDown = true;
        }
        if (swipeDown == true && isGrounded == false)
        {
        rb.gravityScale = 20;
            StopCoroutine(lastRoutine);
            GetComponent<SpriteRenderer>().sprite = JumpPosition[1];
            StartCoroutine(RotateTo0());           


        }
            else
            {
                swipeDown = false;
                rb.gravityScale = 2;
            }
    }

    IEnumerator RotateTo0()
    {
        float startRot = 0;
        float targetRot = 360-transform.eulerAngles.z;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation;
        while (timeElapsed < 0.5f)
        {
            float degrees = Mathf.Lerp(startRot, targetRot, timeElapsed / 0.01f);
            transform.rotation = startRotation * Quaternion.Euler(0, 0, degrees);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // back to the original rotation after rotating 360 degrees!
        //transform.rotation = startRotation;
    }




    void TouchPlayerDown()
    {
    #region Mobile Input
    if (Input.touches.Length > 0)
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            tap = true;
            isDraging = true;
            startTouch = Input.touches[0].position;
        }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
    }
#endregion

                swipeDelta = Vector2.zero;
                if (isDraging)
                {
                    if (Input.touches.Length < 0)
                        swipeDelta = Input.touches[0].position - startTouch;
                    else if (Input.GetMouseButton(0))
                        swipeDelta = (Vector2)Input.mousePosition - startTouch;
                }

        
                    if (swipeDelta.magnitude > 25)
                    {
                                
                        float x = swipeDelta.x;
                        float y = swipeDelta.y;
                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                                    
                            if (x < 0)
                                swipeLeft = true;
                            else
                                swipeRight = true;
                        }
                        else
                        {
                                    
                            if (y < 0)
                                swipeDown = true;

                            else
                                swipeUp = true;
                        }

                        Reset();
                    }

                        if(swipeDown == true && isGrounded == false)
                        {
                                
                        rb.gravityScale = 20;
                        }
                        else
                        {
                        swipeDown = false;
                        rb.gravityScale = 2;
                        }
            
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
   
    {
        if (other.CompareTag("Enemy"))      
        {
            hasDied = true;
            gameObject.SetActive(false);
            gameOver.SetActive(true);
            particles.SetActive(false);
        }

    }   



    void Particles()
    {
        if (isGrounded == true)
        {
            particles.SetActive(true);
                      
        }            
        else
        {
            particles.SetActive(false);
                        
        }
    }

    IEnumerator Jumpsprite()
    {
    
        while (gameObject.transform.position.y > -2.46)
        {
            GetComponent<SpriteRenderer>().sprite = JumpPosition[2];
            yield return null;
        
            if (isGrounded == true && Input.GetKey(KeyCode.Space) == false)
            {
            GetComponent<SpriteRenderer>().sprite = JumpPosition[1];
            }

        }
       
    }


    IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => isGrounded == true);
        shake.CamShake();
    }


}


