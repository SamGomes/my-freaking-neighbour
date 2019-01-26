using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType
{
    Aerial,
    Verbal,
    Bird, //Manguito
    Noise
}

public class Player
{
    GameManager gameManager;

    public int reputation;
    private bool canAttack;

    private RectTransform UILifeBarPoleRT;
    private RectTransform UILifeBarEndRT;
    private float maxLifeBarSize;

    GameObject aerialAttackSprite;
    GameObject aerialAttackFailSprite;
    GameObject verbalAttackSprite;
    GameObject birdAttackSprite; 
    GameObject noiseAttackSprite;

    bool successfulLastAttack;
    int successAttackProbability;
    public AttackType attackActivo;

    bool isAttacking;

    public Player (GameManager gameManager, GameObject UILifeBarObject, float maxLifeBarSize, 
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


   

    IEnumerator FinishAttack(GameObject attackSprite, int damage, Player target, AttackType type)
    {
        //if (!isAttacking)
        {
            //isAttacking = true;
            Animator animator = attackSprite.GetComponent<Animator>();
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("end"))
            {
                /*
                yield return new WaitForSeconds(1);
                
                if (target.isAttacking && target.attackActivo == AttackType.Noise && type == AttackType.Verbal)
                {
                    attackSprite.SetActive(false);
                    target.removeReputation(0);
                    isAttacking = false;
                }

                if (target.isAttacking && target.attackActivo == AttackType.Verbal && type == AttackType.Bird)
                {
                    attackSprite.SetActive(false);
                    target.removeReputation(0);
                    isAttacking = false;
                }


                if (target.isAttacking && target.attackActivo == AttackType.Bird && type == AttackType.Aerial)
                {
                    attackSprite.SetActive(false);
                    target.removeReputation(0);
                    isAttacking = false;
                }

                if (target.isAttacking && target.attackActivo == AttackType.Aerial && type == AttackType.Noise)
                {
                    attackSprite.SetActive(false);
                    target.removeReputation(0);
                    isAttacking = false;
                }
                */
                yield return null;
            }

            attackSprite.SetActive(false);
            target.removeReputation(damage);
            isAttacking = false;
        }
    }

    public void anular()
    {
        this.aerialAttackSprite.SetActive(false);
        this.aerialAttackFailSprite.SetActive(false);
        this.verbalAttackSprite.SetActive(false);
        this.birdAttackSprite.SetActive(false);
        this.noiseAttackSprite.SetActive(false);
        this.isAttacking = false;
    }

    public void anular(int damage)
    {
        this.aerialAttackSprite.SetActive(false);
        this.aerialAttackFailSprite.SetActive(false);
        this.verbalAttackSprite.SetActive(false);
        this.birdAttackSprite.SetActive(false);
        this.noiseAttackSprite.SetActive(false);

        this.removeReputation(damage);
        this.isAttacking = false;
    }


    IEnumerator AnularTimer(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

        }
    }

    //attack methods
    public void PerformAerialAttack(Player target, EnvElementType type)
    {
        bool success = IsSuccess();

        if (!isAttacking)
        {
            isAttacking = true;
            attackActivo = AttackType.Aerial;

            if (success)
            {
                if (target.isAttacking && target.attackActivo == AttackType.Noise)
                {
                    target.anular();
                }

                {
                    this.aerialAttackSprite.SetActive(true);
                    gameManager.StartCoroutine(FinishAttack(aerialAttackSprite, 10, target, attackActivo));
                }
            }
            else
            {
                this.aerialAttackFailSprite.SetActive(true);
                gameManager.StartCoroutine(FinishAttack(aerialAttackFailSprite, 0, target, attackActivo));
            }            
        }        
        
    }
    public void PerformVerbalAttack(Player target, EnvElementType type)
    {
        if (!isAttacking)
        {
            this.attackActivo = AttackType.Verbal;

            if (target.isAttacking && target.attackActivo == AttackType.Noise)
            {
                //Anulado
            }
            else
            {
                if (target.isAttacking && target.attackActivo == AttackType.Bird)
                {
                    target.anular();
                }

                {
                    isAttacking = true;
                    this.verbalAttackSprite.SetActive(true);
                    gameManager.StartCoroutine(FinishAttack(verbalAttackSprite, 10, target, attackActivo));
                }
            }
        }      
    }
    public void PerformBirdAttack(Player target, EnvElementType type)
    {

        if (!isAttacking)
        {
            this.attackActivo = AttackType.Bird;

           if (target.isAttacking && target.attackActivo == AttackType.Verbal)
            {

            }
            else
            {
                if (target.isAttacking && target.attackActivo == AttackType.Aerial)
                {
                    target.anular();
                }

                {
                    isAttacking = true;
                    this.birdAttackSprite.SetActive(true);
                    gameManager.StartCoroutine(FinishAttack(birdAttackSprite, 10, target, attackActivo));
                }
            }                        
        }               
    }
    public void PerformNoiseAttack(Player target, EnvElementType type)
    {

        if (!isAttacking)
        {
            this.attackActivo = AttackType.Noise;
            
            if (target.isAttacking && target.attackActivo == AttackType.Aerial)
            {
                //Anulado
            }
            else
            {
                if (target.isAttacking && target.attackActivo == AttackType.Verbal)
                {
                    target.anular();
                }
                
                isAttacking = true;
                this.noiseAttackSprite.SetActive(true);
                gameManager.StartCoroutine(FinishAttack(noiseAttackSprite, 10, target, attackActivo));
                
            }
        }      
    }

    private bool IsSuccess()
    {
        int randNumber = Random.Range(0, 100);
        return (randNumber < successAttackProbability);
    }
}
