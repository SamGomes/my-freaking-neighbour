using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    GameManager gameManager;

    public int reputation;
    private bool canAttack;

    private RectTransform UILifeBarPoleRT;
    private RectTransform UILifeBarEndRT;
    private float maxLifeBarSize;

    //my sprite
    GameObject myGameObject;

    //attack sprites
    GameObject aerialAttackSprite;
    GameObject aerialAttackFailSprite;
    GameObject verbalAttackSprite;
    GameObject birdAttackSprite; 
    GameObject noiseAttackSprite;

    bool successfulLastAttack;
    int successAttackProbability;

    bool isAttacking;

    public Player (GameManager gameManager, GameObject myGameObject, GameObject UILifeBarObject, float maxLifeBarSize, 
        GameObject aerialAttackSprite, GameObject aerialAttackFailSprite, GameObject verbalAttackSprite, GameObject birdAttackSprite, GameObject noiseAttackSprite,
        int successAttackProbability)
    {

        this.aerialAttackSprite = aerialAttackSprite;
        this.aerialAttackFailSprite = aerialAttackFailSprite;
        this.verbalAttackSprite = verbalAttackSprite;
        this.birdAttackSprite = birdAttackSprite;
        this.noiseAttackSprite = noiseAttackSprite;

        this.reputation = 100;
        this.canAttack = true;

        this.UILifeBarPoleRT = UILifeBarObject.GetComponentsInChildren<RectTransform>()[2]; //get pole
        this.UILifeBarEndRT = UILifeBarObject.GetComponentsInChildren<RectTransform>()[3]; //get pole
        this.maxLifeBarSize = maxLifeBarSize;

        this.successfulLastAttack = false;
        this.successAttackProbability = successAttackProbability;

        this.isAttacking = false;

        this.gameManager = gameManager;

        this.myGameObject = myGameObject;


    }
    
    public void removeReputation(int remove)
    {
        this.reputation -= remove;
        if ( this.reputation < 0 ) { this.reputation = 0; }
        ChangeBarLife(this.reputation);
    }

    public void addReputation(int add)
    {
        this.reputation += add;
        if(this.reputation > 100) { this.reputation = 100; }
        ChangeBarLife(this.reputation);
    }

    public void ChangeBarLife(float life)
    {
        UILifeBarPoleRT.sizeDelta = new Vector2(life/100 * maxLifeBarSize, UILifeBarPoleRT.rect.height);
        UILifeBarEndRT.anchoredPosition = new Vector2(UILifeBarPoleRT.anchoredPosition.x + UILifeBarPoleRT.sizeDelta.x, UILifeBarPoleRT.anchoredPosition.y);
    }


    IEnumerator FinishAttack(GameObject attackSprite, int damage, Player target)
    {
        //if (!isAttacking)
        {
            //isAttacking = true;
            myGameObject.SetActive(false);
            Animator animator = attackSprite.GetComponent<Animator>();
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("end"))
            {
                yield return null;
            }
            attackSprite.SetActive(false);
            myGameObject.SetActive(true);
            target.removeReputation(damage);
            isAttacking = false;
        }
    }

    //attack methods
    public void PerformAerialAttack(Player target)
    {
        bool success = IsSuccess();

        if (!isAttacking)
        {
            isAttacking = true;
            if (success)
            {
                this.aerialAttackSprite.SetActive(true);
                gameManager.StartCoroutine(FinishAttack(aerialAttackSprite, 10, target));
            }
            else
            {
                this.aerialAttackFailSprite.SetActive(true);
                gameManager.StartCoroutine(FinishAttack(aerialAttackFailSprite, 0, target));
            }
            
        }
         
        
    }
    public void PerformVerbalAttack(Player target)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            this.verbalAttackSprite.SetActive(true);
            gameManager.StartCoroutine(FinishAttack(verbalAttackSprite, 10, target));
        }

       

    }
    public void PerformBirdAttack(Player target)
    {

        if (!isAttacking)
        {
            isAttacking = true;
            this.birdAttackSprite.SetActive(true);
            gameManager.StartCoroutine(FinishAttack(birdAttackSprite, 10, target));
        }            

     
    }
    public void PerformNoiseAttack(Player target)
    {

        if (!isAttacking)
        {
            isAttacking = true;
            this.noiseAttackSprite.SetActive(true);
            gameManager.StartCoroutine(FinishAttack(noiseAttackSprite, 10, target));
        }
      
    }

    private bool IsSuccess()
    {
        int randNumber = Random.Range(0, 100);
        return (randNumber < successAttackProbability);
    }
}
