////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;



//public enum AttackType
//{
//    Aerial,
//    Verbal,
//    Bird, //Manguito
//    Noise
//}



////public class AttackManager
////{
////    private AttackType activeAttackR;
////    private AttackType activeAttackL;

////    private Player playerLeft;
////    private Player playerRight;

////    private int successProbability;

////    public bool success;
////    public bool isPlayL;
////    public bool isPlayR;

////    private int aerialDamage;
////    private int verbalDamage;
////    private int birdDamage;
////    private int noiseDamage;


////    public AttackManager(GameObject spriteAerialLeft, GameObject spriteAerialRight, 
////        GameObject spriteAerialFailLeft, GameObject spriteAerialFailRight,
////        GameObject spriteVisualRight, GameObject spriteVisualLeft,
////        int aerialDamage, int verbalDamage, int BirdDamage, int NoiseDamage)
////    {
////        this.aerialDamage = aerialDamage;
////        this.verbalDamage = verbalDamage;
////        this.birdDamage = birdDamage;
////        this.noiseDamage = noiseDamage;

////        successProbability = 80;
////        success = true;

////    }

////    //// Update is called once per frame
////    //void Update()
////    //{

////    //    Animator svlAnimator = spriteVisualLeft.GetComponent<Animator>();
////    //    bool isEndSVL = svlAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

////    //    Animator svrAnimator = spriteVisualRight.GetComponent<Animator>();
////    //    bool isEndSVR = svrAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");


////    //    Animator salAnimator = spriteAerialLeft.GetComponent<Animator>();
////    //    bool isEndSAL = salAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

////    //    Animator sarAnimator = spriteAerialRight.GetComponent<Animator>();
////    //    bool isEndSAR = sarAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

////    //    Animator saflAnimator = spriteAerialFailLeft.GetComponent<Animator>();
////    //    bool isEndSAFL = saflAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

////    //    Animator safrAnimator = spriteAerialFailRight.GetComponent<Animator>();
////    //    bool isEndSAFR = safrAnimator.GetCurrentAnimatorStateInfo(0).IsName("end");

////    //    isPlayL = spriteVisualLeft.activeSelf || spriteAerialLeft.activeSelf || spriteAerialFailLeft.activeSelf;

////    //    isPlayR = spriteVisualRight.activeSelf || spriteAerialRight.activeSelf || spriteAerialFailRight.activeSelf;


////    //    if (spriteAerialFailLeft.activeSelf && isEndSAFL)
////    //    {
////    //        spriteAerialFailLeft.SetActive(false);        
////    //    }

////    //    if (spriteAerialFailRight.activeSelf && isEndSAFR)
////    //    {
////    //        spriteAerialFailRight.SetActive(false);
////    //    }


////    //    if (spriteAerialLeft.activeSelf && isEndSAL)
////    //    {
////    //        spriteAerialLeft.SetActive(false);
////    //        playerRight.removeReputation(aerialDamage);
////    //    }

////    //    if (spriteAerialRight.activeSelf && isEndSAR)
////    //    {
////    //        spriteAerialRight.SetActive(false);
////    //        playerLeft.removeReputation(verbalDamage);
////    //    }


////    //    if (spriteVisualLeft.activeSelf && isEndSVL)
////    //    {
////    //        spriteVisualLeft.SetActive(false);
////    //        playerRight.removeReputation(verbalDamage);
////    //    }

////    //    if (spriteVisualRight.activeSelf && isEndSVR)
////    //    {
////    //        spriteVisualRight.SetActive(false);
////    //        playerLeft.removeReputation(verbalDamage);
////    //    }


////    //    if (Input.GetKeyDown("space"))
////    //    {
////    //        print("space key was pressed");
////    //    }




////    //}





////}
