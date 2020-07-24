using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidMovement : MonoBehaviour
{
    public float speedAmplitude;
    public float changeStateTimeLimit;
    public float turnTimeLimit;
    public Rigidbody2D lamp;
    public Rigidbody2D kid;
    private String state;
    private Vector2 speed;
    private float distance;
    private float walked;
    private float rotation;
    private float stateChangeTimer;
    private float turnTimer;
    private Dictionary stateMachine = new Dictionary<string, Delegate>();
    
    private void Start() 
    {
        state = "stoped";
        stateMachine["stoped"] = new Func<float>(stoppedState);
        stateMachine["walking"] = new Func<float>(walkingState);
        stateMachine["spooked"] = new Func<float>(spookedState);
        stateMachine["searching"] = new Func<float>(searchingState);
        stateMachine["stopedTransitions"] = new Func<float>(stoppedTransitions);
        stateMachine["walkingTransitions"] = new Func<float>(walkingTransitions);
        stateMachine["spookedTransitions"] = new Func<float>(spookedTransitions);
        stateMachine["searchingTransitions"] = new Func<float>(searchingTransitions);
    }

    void FixedUpdate()
    {
        stateMachine[state].DynamicInvoke(Time.deltaTime);
        stateMachine[state+"Transitions"].DynamicInvoke(Time.deltaTime);
    }


    private void stoppedState(float delta)
    {
        updateTimers(delta);
        if(turnTimer >= turnTimeLimit) 
        {
            turnLeft();
            turnTimer=0;
        }
    }

    private void updateTimers(float delta)
    {
        stateChangeTimer += delta;
        turnTimer += delta;
    }

    private void turnLeft()
    {
        rotation = (rotation + 90) % 360;
        updateLampPosition();
    }

    private void updateLampPosition()
    {
        lamp.MoveRotation(rotation);
    }

    private void walkingState(float delta)
    {
        kid.MovePosition(kid.position+(speed*delta));
        walked += speedAmplitude*delta;
    }

    private void spookedState(float delta)
    {
        
    }

    private void searchingState(float delta)
    {
        updateTimers(delta);
        if(turnTimer >= turnTimeLimit/2) 
        {
            turnRandom();
            turnTimer=0;
        }
    }

    private void turnRandom()
    {
        int i = (Math.floor(Random.Range(4)));
        if(i == 0) rotation = 0;
        else if(i == 1) rotation = 90;
        else if(i == 2) rotation = 180;
        else if(i == 3) rotation = 270;
        updateLampPosition();
    }


    private void stoppedTransitions(float delta)
    {
        if(stateChangeTimer >= changeStateTimeLimit)
        {
            destination = pickRandomSpot();
            state = "moving";
            walked = 0;
            stateChangeTimer = 0;
        }
    }

    private Vector2 pickRandomSpot()
    {
        int i = (Math.floor(Random.Range(4)));
        if(i == 0) 
        {
            speed.x = speedAmplitude;
            rotation = 0;
        }
        else if(i == 1)
        {
            speed.y = speedAmplitude;
            rotation = 90;
        } 
        else if(i == 2)
        {
            speed.x = -speedAmplitude;   
            rotation = 180;
        }
        else if(i == 3)
        {
             speed.y = -speedAmplitude;
             rotation = 270;
        }
        distance = Random.Range(10);
        updateLampPosition();
    }

    private void walkingTransitions(float delta)
    {
        if(walked >= distance)
        {
            resetSpeed();
            state = "stopped";
        }
    }

    private resetSpeed()
    {
        speed.x = 0;
        speed.y = 0;
    }

    private void spookedTransitions(float delta)
    {
        if(Abs(speed) <= 0.01)
        {
            state = "searching";
            resetTimers();
        }
    }

    private void resetTimers()
    {
        stateChangeTimer = 0;
        turnTimer = 0;
    }

    private Vector2 Abs (Vector2 v2) 
    {
        return Vector2(Mathf.Abs(v2.x), Mathf.Abs(v2.y));
    }

    private void searchingTransitions(float delta)
    {
        if(stateChangeTimer >= changeStateTimeLimit)
        {
            state = "stopped";
            resetTimers();
        }
    }

    // Triggers

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
        if(!collider.name.Equals("Hitbox") && !collider.name.Equals("Door") && !collider.name.Equals("Hole") && !collider.name.Equals("ParedeBotton") && !collider.name.Equals("ParedeUp") && !collider.name.Equals("ParedeLeft") && !collider.name.Equals("ParedeRight") && !collider.name.Equals("Spike")  )
        {

            Vector2 enemyPosition = collider.transform.gameObject.transform.position;

            float deltaX = rB.position.x - enemyPosition.x;
            float deltaY = rB.position.y - enemyPosition.y;

            rotateFromScare(deltaX, deltaY);
            moveFromScare();
            state = "spooked";
        }
    }

    private void rotateFromScare(float deltaX, float deltaY)
    {
        if(Mathf.Abs(deltaX) >= Mathf.Abs(deltaY))
        {
            if(deltaX > 0) rotation = 0;
            else rotation = 180;
        }
        else
        {
            if(deltaY > 0) rotation = 90;
            else rotation = 270;
        }
        updateLampPosition();
    }

    private void moveFromScare()
    {
        Vector2 direction;
        if(rotation == 0) direction = Vector2(1, 0);
        else if(rotation == 90) direction = Vector2(0, 1);
        else if(rotation == 180) direction = Vector2(-1, 0);
        else direction = Vector2(0, -1);
        kid.velocity = new Vector2(230*direction.x, 230*direction.y);
    }

    /*
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

    int canaUglyFlag = 0;

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
        if(!collider.name.Equals("Hitbox") && !collider.name.Equals("Door") && !collider.name.Equals("Hole") && !collider.name.Equals("ParedeBotton") && !collider.name.Equals("ParedeUp") && !collider.name.Equals("ParedeLeft") && !collider.name.Equals("ParedeRight") && !collider.name.Equals("Spike")  )
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
        canaUglyFlag++;
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canaUglyFlag--;
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
            if(canaUglyFlag == 0)
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
    */
}
