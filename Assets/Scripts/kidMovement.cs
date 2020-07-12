using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 0;
    private float waitTime;
    public float startWaitTime;
    private float rotZ;
    private float rotationTime;
    public float velociadeLoucura = 8;
    public float loucuraTimer = 2;
    private bool Lado = false;
    private bool isMoving = false;
    private bool assustado = false;

    public float startempoSusto = 0;
    private float tempoSusto;

    public Rigidbody2D rB;

    public Transform[ ] moveSpots;
    private int randomSpot;

    Vector2 movement;

    bool randomMovement = true;

    bool canaUglyFlag = true;

    void Start(){

        tempoSusto = startempoSusto;
        waitTime = startWaitTime;
        rotationTime = waitTime/2;
        randomSpot = 0;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
    	reactToCollision(collider);

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        reactToCollision(collision.collider);   
    }

    private void reactToCollision(Collider2D collider)
    {    
        if(!collider.name.Equals("Hitbox") && !collider.name.Equals("Door") && !collider.name.Equals("Hole") && !collider.name.Equals("ParedeBotton") && !collider.name.Equals("ParedeUp") && !collider.name.Equals("ParedeLeft") && !collider.name.Equals("ParedeRight"))
        {

            Vector2 enemyPosition = collider.transform.gameObject.transform.position;

            float deltaX = rB.position.x - enemyPosition.x;
            float deltaY = rB.position.y - enemyPosition.y;


            if(Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
            {
                if(deltaX > 0) moveFromScare(new Vector2(1, 0));
                else moveFromScare(new Vector2(-1, 0));
            }
            else
            {
                if(deltaY > 0) moveFromScare(new Vector2(0, 1));
                else moveFromScare(new Vector2(0, -1));
            }
        }
    }

    private void moveFromScare(Vector2 direction)
    {
        rB.velocity = new Vector2(230*direction.x, 230*direction.y);
        rB.MoveRotation(90*direction.x + (90 + 90*direction.y)*direction.y);
        randomMovement = false;
        tempoSusto = startempoSusto;
        assustado = true;
        canaUglyFlag = false;
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canaUglyFlag = true;
    }

    void Update()
    {
                if(randomMovement)
                {
                    transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, moveSpeed * Time.deltaTime);
                } else {
               if (tempoSusto <= 0){
                    randomMovement = true;
                    assustado = false;
            } else {
                 tempoSusto -= Time.deltaTime;

            } 
            }
                

                if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
                     isMoving = false;
                    if (waitTime <= 0){
                            if(randomMovement)
                            {
                               
                                randomSpot = Random.Range(0, moveSpots.Length);

                                float deltaX = rB.position.x - moveSpots[randomSpot].position.x;
                                float deltaY = rB.position.y - moveSpots[randomSpot].position.y;


                                if(Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
                                {
                                    if(deltaX > 0) rotate(new Vector2(1, 0));
                                    else rotate(new Vector2(-1, 0));
                                }
                                else
                                {
                                    if(deltaY > 0) rotate(new Vector2(0, 1));
                                    else rotate(new Vector2(0, -1));
                                }

                                waitTime = startWaitTime;
                            }
                        } else {
                            waitTime -= Time.deltaTime;

                        }
                } else {

                     isMoving = true;
                }  
    }

    private void rotate(Vector2 direction)
    {
        rB.MoveRotation(90*direction.x + (90 + 90*direction.y)*direction.y);
    }

    void FixedUpdate()
        {
            if(canaUglyFlag)
            {
                 if(!assustado){
                if(!isMoving){
                    if(Lado){
                     if (rotationTime >= 0){
                       rotZ += Time.deltaTime * rotationSpeed;
                        rotationTime -= Time.deltaTime;
                       } else {
                           Lado = !Lado;
                       }
                  }else{
                    if (rotationTime <= startWaitTime){
                       rotZ += -Time.deltaTime * rotationSpeed;
                       rotationTime += Time.deltaTime;
                    } else {
                       Lado = !Lado;
                    } 
                 }

                   transform.rotation = Quaternion.Euler(0,0,rotZ);
            }
        }else {
            if(Lado){
                     if (rotationTime >= 0){
                       rotZ += Time.deltaTime * rotationSpeed*velociadeLoucura;
                        rotationTime -= Time.deltaTime;
                       } else {
                           Lado = !Lado;
                       }
                  }else{
                    if (rotationTime <= loucuraTimer){
                       rotZ += -Time.deltaTime * rotationSpeed*velociadeLoucura;
                       rotationTime += Time.deltaTime;
                    } else {
                       Lado = !Lado;
                    } 
                 }

                   transform.rotation = Quaternion.Euler(0,0,rotZ);
            }
            }
    }
}
