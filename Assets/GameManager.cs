using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        maxEnvElements = 1;

        spawnChoiceTimeInSeconds = 5;
        spawnProbability = 0.5f;

        players = new List<Player>();
        players.Add(new Player());
        players.Add(new Player());

        currEnvElements = new List<EnvironmentElement>();
        possibleEnvElements = new List<EnvironmentElement>();
        Vector3 initialPos = new Vector3(-1.16f, 2.05f, -0.58f);
        Vector3 cameraOrientation = -camera.transform.forward;
        Vector3 orientation = new Vector3(cameraOrientation.x, 0, cameraOrientation.z);
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Car, carPrefab, initialPos, orientation, 40.0f));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Girl, girlPrefab, initialPos, orientation, 20.0f));
        possibleEnvElements.Add(new EnvironmentElement(EnvElementType.Swagger, guyPrefab, initialPos, orientation, 20.0f));

        StartCoroutine(SpawnEnvElements(spawnChoiceTimeInSeconds));
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("passou");
        }
    }

    EnvironmentElement ChooseNewEnvElement()
    {
        int possibleEnvElementsSize = possibleEnvElements.Count;
        int randIndex = Random.Range(0, possibleEnvElementsSize);
        return possibleEnvElements[randIndex];
    }
}
