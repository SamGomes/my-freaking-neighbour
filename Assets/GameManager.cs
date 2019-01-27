using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text UITimer;
    private int globalTimer;

    public Collider changeEnvTrigger;

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

    List<EnvironmentElement> possibleEnvElements;
    int maxEnvElements;


    EnvironmentElement activeEnvElement;
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


        Global._players.Add(new Player(this, player1Sprite, UILifeBarObjectP1, maxLifeBarSize, spriteAerialLeft, spriteAerialFailLeft, spriteVerbalLeft, spriteBirdLeft, spriteNoiseLeft, 80));
        Global._players.Add(new Player(this, player2Sprite, UILifeBarObjectP2, maxLifeBarSize, spriteAerialRight, spriteAerialFailRight, spriteVerbalRight, spriteBirdRight, spriteNoiseRight, 80));
        
        possibleEnvElements = new List<EnvironmentElement>();
        Vector3 initialPos = new Vector3(-1.16f, 2.05f, -0.58f);
        Vector3 cameraOrientation = -camera.transform.forward;
        Vector3 orientation = new Vector3(cameraOrientation.x, 0, cameraOrientation.z);
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Car, carPrefab, new Vector3(-1.16f, 1.45f, -0.58f), orientation, 40.0f,"Car"));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Girl, girlPrefab, initialPos, orientation, 20.0f, "Girl"));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Swagger, guyPrefab, initialPos, orientation, 20.0f,"Guy"));


        activeEnvElement = ChooseNewEnvElement();
        activeEnvElement.Spawn();
        
        StartCoroutine(DecreaseGlobalTimer(1));
    }
  
    // Update is called once per frame
    void Update()
    {
        Player player1 = Global._players[0];
        Player player2 = Global._players[1];
        

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
            else
            {
                SceneManager.LoadScene("FHV_UIScene");
            }
        }
    }

  

    EnvironmentElement ChooseNewEnvElement()
    {
        int possibleEnvElementsSize = possibleEnvElements.Count;
        int randIndex = Random.Range(0, possibleEnvElementsSize);
        return possibleEnvElements[randIndex];
    }

    void OnTriggerEnter(Collider collider)
    {
        activeEnvElement.Unspawn();
        activeEnvElement = ChooseNewEnvElement();
        activeEnvElement.Spawn();
    }
}
