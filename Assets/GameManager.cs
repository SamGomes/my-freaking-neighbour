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

    // Start is called before the first frame update
    void Start()
    {
        globalTimer = 100;

        maxEnvElements = 1;

        spawnChoiceTimeInSeconds = 5;
        spawnProbability = 0.5f;

        float maxLifeBarSize = 88.0f;

        players = new List<Player>();
        players.Add(new Player(UILifeBarObjectP1, maxLifeBarSize));
        players.Add(new Player(UILifeBarObjectP2, maxLifeBarSize));


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

    IEnumerator SpawnEnvElements(float delay)
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
