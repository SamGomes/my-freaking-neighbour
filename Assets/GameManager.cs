using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text UITimer;
    private int globalTimer;

    public GameObject UILifeBarObjectP1;
    public GameObject UILifeBarObjectP2;

    public GameObject player1Sprite;
    public GameObject player2Sprite;

    public GameObject spriteAerialLeft;
    public GameObject spriteAerialRight;
    public GameObject spriteAerialFailLeft;
    public GameObject spriteAerialFailRight;
    public GameObject spriteVerbalRight;
    public GameObject spriteVerbalLeft;

    public GameObject spriteBirdRight;
    public GameObject spriteBirdLeft;

    public GameObject spriteNoiseRight;
    public GameObject spriteNoiseLeft;



    public GameObject carPrefab;
    public GameObject girlPrefab;
    public GameObject guyPrefab;
   

    public GameObject camera;

    float spawnChoiceTimeInSeconds;
    float spawnProbability;

    List<Player> players;
    List<EnvironmentElement> possibleEnvElements;
    int maxEnvElements;


    List<EnvironmentElement> currEnvElements;
    private bool P1Attack = true;
    private bool P2Attack = true;
    private string attP1, attP2;


    // Start is called before the first frame update
    void Start()
    {
        globalTimer = 100;

        maxEnvElements = 1;

        spawnChoiceTimeInSeconds = 5;
        spawnProbability = 0.5f;

        float maxLifeBarSize = 88.0f;

        players = new List<Player>();
        players.Add(new Player(this, player1Sprite, UILifeBarObjectP1, maxLifeBarSize, spriteAerialLeft, spriteAerialFailLeft, spriteVerbalLeft, spriteBirdLeft, spriteNoiseLeft, 80));
        players.Add(new Player(this, player2Sprite, UILifeBarObjectP2, maxLifeBarSize, spriteAerialRight, spriteAerialFailRight, spriteVerbalRight, spriteBirdRight, spriteNoiseRight, 80));
        
        
        currEnvElements = new List<EnvironmentElement>();
        possibleEnvElements = new List<EnvironmentElement>();
        Vector3 initialPos = new Vector3(-1.16f, 2.05f, -0.58f);
        Vector3 cameraOrientation = -camera.transform.forward;
        Vector3 orientation = new Vector3(cameraOrientation.x, 0, cameraOrientation.z);
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Car, carPrefab, new Vector3(-1.16f, 1.45f, -0.58f), orientation, 40.0f));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Girl, girlPrefab, initialPos, orientation, 20.0f));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Swagger, guyPrefab, initialPos, orientation, 20.0f));

        StartCoroutine(SpawnEnvElements(spawnChoiceTimeInSeconds));

        StartCoroutine(DecreaseGlobalTimer(1));
    }
  
    // Update is called once per frame
    void Update()
    {
        Player player1 = players[0];
        Player player2 = players[1];

        if (P1Attack && Input.GetKeyDown("q"))
        {
            attP1 = "A";
            P1Attack = false;
        }
        else if (P1Attack && Input.GetKeyDown("w"))
        {
            attP1 = "R";
            P1Attack = false;
        }
        else if (P1Attack && Input.GetKeyDown("e"))
        {
            attP1 = "IV";
            P1Attack = false;
        }
        else if (P1Attack && Input.GetKeyDown("r"))
        {
            attP1 = "IM";
            P1Attack = false;
        }
        else if (P2Attack && Input.GetKeyDown("h"))
        {
            attP2 = "A";
            P2Attack = false;
        }

        else if (P2Attack && Input.GetKeyDown("j"))
        {
            attP2 = "R";
            P2Attack = false;
        }
        else if (P2Attack && Input.GetKeyDown("k"))
        {
            attP2 = "IV";
            P2Attack = false;
        }
        else if (P2Attack && Input.GetKeyDown("l"))
        {
            attP2 = "IM";
            P2Attack = false;
        }

        if(!P1Attack && !P2Attack)
            envChangeRespect(attP1, attP2);


        
        if (Input.GetKeyDown(KeyCode.A))
        {
            player1.PerformAerialAttack(player2);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player1.PerformBirdAttack(player2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player1.PerformVerbalAttack(player2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            player1.PerformNoiseAttack(player2);
        }


        // Player 2 
       
        if (Input.GetKeyDown(KeyCode.J))
        {
            player2.PerformAerialAttack(player1); 
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            player2.PerformVerbalAttack(player1);

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            player2.PerformBirdAttack(player1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player2.PerformNoiseAttack(player1);
        }
        


    }

    IEnumerator DecreaseGlobalTimer(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (globalTimer > 0)
            {
                UITimer.text = (--globalTimer).ToString();
            }
        }
    }

    private void changeRespect()
    {
        if(attP1 == "A" && attP2 == "R")
        {
            //attP2 leva dano
            players[1].removeReputation(5);
        }
        else if(attP1 == "R" && attP2 == "A")
        {
            //attP1 leva dano 
            players[0].removeReputation(5);
        }
        else if (attP1 == "R" && attP2 == "IV")
        {
            //att2 leva dano
            if(currEnvElements[0].GetType() == EnvElementType.Car) { }
            else
            {
                players[1].removeReputation(5);
            }
        }
        else if (attP1 == "IV" && attP2 == "R")
        {
            //att1 leva dano
            if (currEnvElements[0].GetType() == EnvElementType.Car) { }
            else
            {
                players[0].removeReputation(5);
            }
        }
        else if (attP1 == "IV" && attP2 == "IM")
        {
            //att2 leva dano
            if (currEnvElements[0].GetType() == EnvElementType.Car) { }
            else
            {
                players[1].removeReputation(5);
            }
        }
        else if (attP1 == "IM" && attP2 == "IV")
        {
            //att1 leva dano
            if (currEnvElements[0].GetType() == EnvElementType.Car) { }
            else
            {
                players[0].removeReputation(5);
            }
        }
        else if (attP1 == "IM" && attP2 == "A")
        {
            //att2 leva dano
            players[1].removeReputation(5);
        }
        else if (attP1 == "A" && attP2 == "IM")
        {
            //att1 leva dano
            players[0].removeReputation(5);
        }
        else
        {
            // levam os dois dano
            if (currEnvElements[0].GetType() == EnvElementType.Car && (attP1 == "R" && attP2 == "R") || (attP1 == "IV" && attP2 == "IV")) { }
            else
            {
                players[0].removeReputation(5);
                players[1].removeReputation(5);
            }
        }
        P1Attack = true;
        P2Attack = true;
    }

    private void envChangeRespect(string p1, string p2)
    {
        if(currEnvElements[0].GetType() == EnvElementType.Car)
        {
            if(p1 == "A")
            {
                //probabilidade
            }
            else if(p1 == "R")
            {
                //anula
            }
            else if (p1 == "IV")
            {
                //anula
            }
            else if( p1 == "IM")
            {
                //anula
            }
            else if (p2 == "A")
            {
                //probabilidade
            }
            else if (p2 == "R")
            {
                //anula
            }
            else if (p2 == "IV")
            {
                //anula
            }
            else if (p2 == "IM")
            {
                //anula
            }

            changeRespect();
        }
        else if(currEnvElements[0].GetType() == EnvElementType.Girl)
        {
             if(p1 == "A")
            {
                //probabilidade
            }
            else if(p1 == "R")
            {
                players[0].addReputation(5);
                players[1].removeReputation(5);
            }
            else if (p1 == "IV")
            {
                players[0].removeReputation(5);
            }
            else if(p1 == "IM")
            {
                players[0].removeReputation(5);
            }
            else if (p2 == "A")
            {
                //probabilidade
            }
            else if (p2 == "R")
            {
                players[1].addReputation(5);
                players[0].removeReputation(5);
            }
            else if (p2 == "IV")
            {
                players[1].removeReputation(5);
            }
            else if (p2 == "IM")
            {
                players[1].removeReputation(5);
            }
            changeRespect();

        }
        else
        {
            if (p1 == "A")
            {
                //probabilidade
            }
            else if (p1 == "R")
            {
                players[0].removeReputation(5);
            }
            else if (p1 == "IV")
            {
                players[0].addReputation(5);
                players[1].removeReputation(5);
            }
            else if (p1 == "IM")
            {
                players[0].addReputation(5);
                players[1].removeReputation(5);
            }
            else if (p2 == "A")
            {
                //probabilidade
            }
            else if (p2 == "R")
            {
                players[1].removeReputation(5);
            }
            else if (p2 == "IV")
            {
                players[1].addReputation(5);
                players[0].removeReputation(5);
            }
            else if (p2 == "IM")
            {
                players[1].addReputation(5);
                players[0].removeReputation(5);
            }
            changeRespect();
        }
    }

    IEnumerator SpawnEnvElements(double delay)
    {
        while (true)
        {
            int possibleEnvElementsSize = currEnvElements.Count;
            if (possibleEnvElementsSize > maxEnvElements)
            {
                currEnvElements[maxEnvElements].Unspawn();
                currEnvElements.RemoveAt(maxEnvElements);
            }

            int randNumber = Random.Range(0, possibleEnvElementsSize);
            if (randNumber < spawnProbability)
            {
                EnvironmentElement newEnvElement = ChooseNewEnvElement();
                currEnvElements.Add(newEnvElement);
                newEnvElement.Spawn();
            }
            yield return new WaitForSeconds(spawnChoiceTimeInSeconds);
        }
    }

    EnvironmentElement ChooseNewEnvElement()
    {
        int possibleEnvElementsSize = possibleEnvElements.Count;
        int randIndex = Random.Range(0, possibleEnvElementsSize);
        return possibleEnvElements[randIndex];
    }
}
