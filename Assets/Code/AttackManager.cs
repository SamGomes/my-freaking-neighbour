using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AttackType activeAttackR;
    private AttackType activeAttackL;
    private bool isActive;

    public GameObject spriteAttack;
    public GameObject spriteVisualRight;
    public GameObject spriteVisualLeft;



    // Start is called before the first frame update
    void Start()
    {
        isActive = false;


    }

    // Update is called once per frame
    void Update()
    {

        Animator svlAnimator = spriteVisualLeft.GetComponent<Animator>();
        bool isEndSVL = svlAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

        Animator svrAnimator = spriteVisualRight.GetComponent<Animator>();
        bool isEndSVR = svrAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");
       
        bool isPlayL = spriteVisualLeft.activeSelf;

        bool isPlayR = spriteVisualRight.activeSelf;


        if (spriteVisualLeft.activeSelf && isEndSVL)
        {
            spriteVisualLeft.SetActive(false);
        }

        if (spriteVisualRight.activeSelf && isEndSVR)
        {
            spriteVisualRight.SetActive(false);
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
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                print("W key was pressed");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                print("S key was pressed");
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
        }
       

    }
}
