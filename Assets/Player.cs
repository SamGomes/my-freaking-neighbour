using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum AttackType
{
    Aerial,
    AerialFail,
    Verbal,
    Bird, //Manguito
    Noise,
    None
}




public class Player
{

    AttackType currAttackType;

    GameManager gameManagerRef;

    public int reputation;
    private bool canAttack;

    private RectTransform UILifeBarPoleRT;
    private RectTransform UILifeBarEndRT;
    private float maxLifeBarSize;

    //my sprite
    GameObject myGameObject;

    //attack sprites
    List<GameObject> aerialAttackSprites;
    List<GameObject> aerialAttackFailSprites;
    GameObject verbalAttackSprite;
    GameObject birdAttackSprite; 
    GameObject noiseAttackSprite;

    bool successfulLastAttack;
    int successAttackProbability;

    bool isAttacking;

    public AudioClip som;
    public AudioSource audioSource;

    public Player (GameManager gameManagerRef, GameObject myGameObject, GameObject UILifeBarObject, float maxLifeBarSize,
        List<GameObject> aerialAttackSprites, List<GameObject> aerialAttackFailSprites, GameObject verbalAttackSprite, GameObject birdAttackSprite, GameObject noiseAttackSprite,
        int successAttackProbability)
    {
        this.currAttackType = AttackType.None;

        this.aerialAttackSprites = aerialAttackSprites;
        this.aerialAttackFailSprites = aerialAttackFailSprites;
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
      

        this.gameManagerRef = gameManagerRef;

        this.myGameObject = myGameObject;



    }

    void Start()
    {
        //audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        
    }

    public void RemoveReputation(int remove)
    {
        GameObject sound = GameObject.FindGameObjectWithTag("Audio");
        AudioSource aux2 = sound.GetComponent<AudioSource>();
        var aux = Resources.Load("Damage1") as AudioClip;
        aux2.PlayOneShot(aux);
            
        this.reputation -= remove;
        if ( this.reputation < 0 ) {
            this.reputation = 0;
            SceneManager.LoadScene("End");
        }
        ChangeBarLife(this.reputation);
    }

    public void AddReputation(int add)
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
        //isAttacking = true;
        //myGameObject.SetActive(false);
        Animator animator = attackSprite.GetComponent<Animator>();
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            yield return null;
        }
        attackSprite.SetActive(false);
        myGameObject.SetActive(true);
        target.RemoveReputation(damage);

        if (currAttackType == AttackType.Aerial || currAttackType == AttackType.AerialFail)
        {
            GameObject sound1 = GameObject.FindGameObjectWithTag("Audio");
            AudioSource aux1 = sound1.GetComponent<AudioSource>();
            var aux3 = Resources.Load("Bottle") as AudioClip;
            aux1.PlayOneShot(aux3);
        }

        this.currAttackType = AttackType.None;
    }
    


    //attack methods
    public void PerformAerialAttack(Player target)
    {
        if (this.currAttackType == AttackType.None)
        {

            bool success = IsSuccess();
            if (success)
            {
                this.currAttackType = AttackType.Aerial;
                GameObject randSprite = this.aerialAttackSprites[Random.Range(0,aerialAttackSprites.Count)];
                randSprite.SetActive(true);
             
                if(target.currAttackType == AttackType.Bird)
                {
                    gameManagerRef.StartCoroutine(FinishAttack(randSprite, 0, target));
                }
                else
                    gameManagerRef.StartCoroutine(FinishAttack(randSprite, 10, target));
            }
            else
            {
                EnvironmentElement currEnvElement = gameManagerRef.getActiveEnvElement();
                this.currAttackType = AttackType.AerialFail;
                GameObject randSprite = this.aerialAttackFailSprites[Random.Range(0, aerialAttackFailSprites.Count)];
                randSprite.SetActive(true);
                gameManagerRef.StartCoroutine(FinishAttack(randSprite, 0, target));

                if (currEnvElement.GetType() == EnvElementType.Car || currEnvElement.GetType() == EnvElementType.Girl || currEnvElement.GetType() == EnvElementType.Swagger)
                {
                    this.RemoveReputation(10);
                }
            }

        }
         
        
    }
    public void PerformVerbalAttack(Player target)
    {
        if (this.currAttackType == AttackType.None)
        {
            EnvironmentElement currEnvElement = gameManagerRef.getActiveEnvElement();
            GameObject sound = GameObject.FindGameObjectWithTag("Audio");
            AudioSource aux1 = sound.GetComponent<AudioSource>();
            var aux = Resources.Load("Verbal Insult") as AudioClip;
            aux1.PlayOneShot(aux);
        
            int damage = 10;
            if (currEnvElement.GetType() == EnvElementType.Car || target.currAttackType == AttackType.Noise)
            {
                damage = 0;
            }
            else if (currEnvElement.GetType() == EnvElementType.Girl)
            {
                this.RemoveReputation(10);
            }
            else if(currEnvElement.GetType() == EnvElementType.Swagger)
            {
                this.AddReputation(10);
                damage = 10;
            }

            this.currAttackType = AttackType.Verbal;
            this.verbalAttackSprite.SetActive(true);
            gameManagerRef.StartCoroutine(FinishAttack(verbalAttackSprite, damage, target));
        }

       

    }
    public void PerformBirdAttack(Player target)
    {
       

        if (this.currAttackType == AttackType.None)
        {
            EnvironmentElement currEnvElement = gameManagerRef.getActiveEnvElement();
            GameObject sound = GameObject.FindGameObjectWithTag("Audio");
            AudioSource aux1 = sound.GetComponent<AudioSource>();
            var aux = Resources.Load("Bird Insult") as AudioClip;
            aux1.PlayOneShot(aux);

            int damage = 10;
            
            if (currEnvElement.GetType() == EnvElementType.Car || target.currAttackType == AttackType.Noise)
            {
                damage = 0;
            }
            else if(currEnvElement.GetType() == EnvElementType.Girl)
            {
                this.RemoveReputation(10);
            }
            else if(currEnvElement.GetType() == EnvElementType.Swagger)
            {
                this.AddReputation(10);
                damage = 10;
            }


            this.currAttackType = AttackType.Bird;
            this.birdAttackSprite.SetActive(true);
            gameManagerRef.StartCoroutine(FinishAttack(birdAttackSprite, damage, target));
        }            

     
    }
    public void PerformNoiseAttack(Player target)
    {
       

        if (this.currAttackType == AttackType.None)
        {
            EnvironmentElement currEnvElement = gameManagerRef.getActiveEnvElement();
            GameObject sound = GameObject.FindGameObjectWithTag("Audio");
            AudioSource aux1 = sound.GetComponent<AudioSource>();
            var aux = Resources.Load("Loud Noise") as AudioClip;
            aux1.PlayOneShot(aux);

            int damage = 10;

            if (target.currAttackType == AttackType.Aerial || currEnvElement.GetType() == EnvElementType.Car)
            {
                damage = 0;
            }
            else if(currEnvElement.GetType() == EnvElementType.Girl)
            {
                this.AddReputation(10);
                damage = 10;
            }
            else if(currEnvElement.GetType() == EnvElementType.Swagger)
            {
                this.RemoveReputation(10);
                damage = 0;
            }

            this.currAttackType = AttackType.Noise;
            isAttacking = true;
            this.noiseAttackSprite.SetActive(true);
            gameManagerRef.StartCoroutine(FinishAttack(noiseAttackSprite, damage, target));
        }
      
    }

    private bool IsSuccess()
    {
        int randNumber = Random.Range(0, 100);
        return (randNumber < successAttackProbability);
    }
}
