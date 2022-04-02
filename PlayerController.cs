using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State{
        Idle,
        Run,
        Crush,
    }
    private Rigidbody rb;
    private Animator animator;
    private Vector3 speedVector;
    private Vector3 crushSpeedVector;
    [SerializeField] public State state;
    [SerializeField] public float speed;
    [SerializeField] public float speedBooster;
    [SerializeField] private float boostUpTime;
    [SerializeField] private AnimationCurve boostUpCurve;
    [SerializeField] private bool boostControl; 

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        state = State.Run;
        boostControl = true;
        speedVector = new Vector3(0,0,1); //Vector3.forawrd;
        crushSpeedVector = new Vector3(0,0,0.3f);
    }

    private void Update() 
    {
        PlayerSpeed();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("CrushedCollectable"))
        {
            state = State.Run;
        }
    }

    private void PlayerSpeed()
    {
        if(state == State.Run)
        {
            rb.velocity = speedVector*speed*Time.deltaTime*speedBooster;
            Boost(boostControl);
        }
        if(state == State.Crush)
        {
            rb.velocity = crushSpeedVector * speed * speedBooster * Time.deltaTime;
        }
    }
    private void Boost(bool boostState)
    {
        if(boostState ==true)
        {
            boostUpTime += Time.deltaTime;
            animator.speed = speedBooster;
            if(boostUpTime >= 10) boostUpTime = 10;
            
        }
        else if (boostState == false)
        {
            if(speedBooster == 1)
            {
                boostControl = true;
            }
            boostUpTime -= Time.deltaTime*10;
            animator.speed = 1.3f;
            if(boostUpTime <= 0 ) boostUpTime = 0 ;
        }
        speedBooster = boostUpCurve.Evaluate(boostUpTime);
        gameObject.transform.GetChild(2).GetComponent<PlayerBall>().BoostActive(speedBooster);
    }

    private void TouchedObstacle()
    {
        boostControl = false;
        Debug.Log("touched");
    }


}
