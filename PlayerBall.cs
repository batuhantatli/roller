using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerBall : MonoSingleton<PlayerBall>
{
    private GameObject player;
    private float current,target;
    private float speed =0.5f;
    
    [Header("Rotate Speed Control")]
    [SerializeField] private float baseRotateSpeed;
    private float rotateSpeed;

    [Header("Growing Control")]
    [SerializeField] private AnimationCurve curve;
    private float groweSize;
    private float currentSize;
    public float groweCounter;
    public bool sizeControl;
    [SerializeField] private float growSpeed;

    [Header("Power")]
    [SerializeField] public int power;
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private Color failColor;
    
    void Start()
    {
        currentSize = 1;
        rotateSpeed = baseRotateSpeed;
        player = transform.parent.gameObject;
        powerText.text = "" + power;
    }
    void Update()
    {
        transform.Rotate(rotateSpeed,0,0);
        BallGrowe();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Collectable"))
        {
            if(power > other.GetComponent<Collectable>().power)
            {
                player.GetComponent<PlayerController>().state = PlayerController.State.Crush;
                sizeControl = true;
                other.GetComponent<Collectable>().AfterObstacleDestroyed(other.transform,other.gameObject.GetComponent<Collectable>().crushedCollectable);
                Destroy(other.transform.parent.gameObject);
                UIExperienceBar.Instance.GainExperience(25);
            }
            else
            {
                if(other.transform.position.x < gameObject.transform.position.x)
                {
                    FailBall(-1,other.gameObject);
                }
                else if(other.transform.position.x >= gameObject.transform.position.x )
                {
                    FailBall(1,other.gameObject);
                }
            }
            
        }
        
    }
    public void BoostActive(float _speedBooster)
    {
        rotateSpeed = _speedBooster + baseRotateSpeed;
    }
    public void BoostPassive()
    {
        rotateSpeed = baseRotateSpeed;
    }
    
    public void BallGrowe()
    {
        if(currentSize >= 2) return;
        else if(sizeControl == true)
        {
        groweSize = currentSize + groweCounter;
        current = Mathf.MoveTowards(current,1,growSpeed*Time.deltaTime);
        transform.parent.localScale = Vector3.Lerp(new Vector3(currentSize,currentSize,currentSize),new Vector3(groweSize,groweSize,groweSize)  , curve.Evaluate(current));
        if(transform.parent.localScale == new Vector3(groweSize,groweSize,groweSize))
        {
            sizeControl = false;
            current = 0;
            currentSize = groweSize;
        }
        }
    }




    public void FailBall(int forceVectorSign,GameObject collectable)
    {
        player.transform.DOMoveX(0, .1f, false);
        gameObject.GetComponent<Renderer>().material.DOColor(failColor, 0.5f).From();
        UIExperienceBar.Instance.LoseExperience(10);
        collectable.GetComponent<Rigidbody>().AddForce(forceVectorSign*100,200,300);
        collectable.GetComponent<SphereCollider>().enabled = false;
        collectable.transform.DORotate(new Vector3(Random.Range(0,180),Random.Range(0,180),Random.Range(0,180)),1.5f);
    }
    public void PlayerLevelUp()
    {
        power++;
        powerText.text = "" + power;
        //....
    }
}
