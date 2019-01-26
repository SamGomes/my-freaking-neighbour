using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AttackType
{
    Aerial,
    Verbal,
    Bird, //Manguito
    Noise
}



public class AttackManager : MonoBehaviour
{
    private AttackType activeAttackR;
    private AttackType activeAttackL;
    
    public Player playerLeft;
    public Player playerRight;

    public GameObject spriteAerialLeft;
    public GameObject spriteAerialRight;
    
    public GameObject spriteAerialFailLeft;
    public GameObject spriteAerialFailRight;
    
    public GameObject spriteVisualRight;
    public GameObject spriteVisualLeft;

    public int successProbability;

    public bool success;
    public bool isPlayL;
    public bool isPlayR;

    public int AerialDamage;
    public int VerbalDamage;
    public int BirdDamage;
    public int NoiseDamage;

    // Start is called before the first frame update
    void Start()
    {
        AerialDamage = VerbalDamage = BirdDamage = NoiseDamage = 10;
        successProbability = 80;
        success = true;
    }

    // Update is called once per frame
    void Update()
    {

        Animator svlAnimator = spriteVisualLeft.GetComponent<Animator>();
        Animator svrAnimator = spriteVisualRight.GetComponent<Animator>();
        Animator salAnimator = spriteAerialLeft.GetComponent<Animator>();
        Animator sarAnimator = spriteAerialRight.GetComponent<Animator>();
        Animator saflAnimator = spriteAerialFailLeft.GetComponent<Animator>();
        Animator safrAnimator = spriteAerialFailRight.GetComponent<Animator>();

        bool isEndSVL = svlAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");       
        bool isEndSVR = svrAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");                
        bool isEndSAL = salAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");
        bool isEndSAR = sarAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");
        bool isEndSAFL = saflAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");
        bool isEndSAFR = safrAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

        isPlayL = spriteVisualLeft.activeSelf || spriteAerialLeft.activeSelf || spriteAerialFailLeft.activeSelf;

        isPlayR = spriteVisualRight.activeSelf || spriteAerialRight.activeSelf || spriteAerialFailRight.activeSelf;
        

        if (spriteAerialFailLeft.activeSelf && isEndSAFL)
        {
            spriteAerialFailLeft.SetActive(false);        
        }

        if (spriteAerialFailRight.activeSelf && isEndSAFR)
        {
            spriteAerialFailRight.SetActive(false);
        }
        
     
        if (spriteAerialLeft.activeSelf && isEndSAL)
        {
            spriteAerialLeft.SetActive(false);
            playerRight.removeReputation(AerialDamage);
        }

        if (spriteAerialRight.activeSelf && isEndSAR)
        {
            spriteAerialRight.SetActive(false);
            playerLeft.removeReputation(VerbalDamage);
        }


        if (spriteVisualLeft.activeSelf && isEndSVL)
        {
            spriteVisualLeft.SetActive(false);
            playerRight.removeReputation(VerbalDamage);
        }

        if (spriteVisualRight.activeSelf && isEndSVR)
        {
            spriteVisualRight.SetActive(false);
            playerLeft.removeReputation(VerbalDamage);
        }
                    
       
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
        }

        if (!isPlayL)
        {            
            if (Input.GetKeyDown(KeyCode.A))
            {
                print("A key was pressed");
                spriteVisualLeft.SetActive(true);
                activeAttackL = AttackType.Verbal;
                success = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                print("W key was pressed");
              
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                print("S key was pressed");
               
                success = isSuccess();
                if (success)
                {
                    spriteAerialLeft.SetActive(true);
                }
                else
                {
                    spriteAerialFailLeft.SetActive(true);
                }
               

                activeAttackL = AttackType.Aerial;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                print("D key was pressed");
            }
            
        }


        if (!isPlayR)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                print("J key was pressed");
                spriteVisualRight.SetActive(true);
                activeAttackR = AttackType.Verbal;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                print("K key was pressed");
                success = isSuccess();

                if (success)
                {
                    spriteAerialRight.SetActive(true);
                }
                else
                {
                    spriteAerialFailRight.SetActive(true);
                }

                activeAttackR = AttackType.Aerial;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                print("I key was pressed");
              
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                print("L key was pressed");
            }
        }
       

    }
    private bool isSuccess()
    {
        int randNumber = Random.Range(0, 100);
        print("randSuccess = " + randNumber);
        return (randNumber < successProbability);
    }


}
