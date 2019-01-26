using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvElementType
{
    Car,
    Girl,
    Swagger
}

public class EnvironmentElement : MonoBehaviour
{
    private GameObject elementGameObject;
    private Vector3 initialPos;
    private Vector3 initialOrientation;
    private EnvElementType type;
    private float speed;

    public EnvironmentElement(EnvElementType type, GameObject elementGameObjectPrefab, Vector3 initialPos, Vector3 initialOrientation, float speed)
    {
        this.elementGameObject = Instantiate(elementGameObjectPrefab);

        this.initialPos = initialPos;
        this.initialOrientation = initialOrientation;

        this.type = type;
        this.speed = speed;

        elementGameObject.SetActive(false);
        
    }

    public void Spawn()
    {
        elementGameObject.SetActive(true);
        elementGameObject.transform.position = initialPos;

        Rigidbody elementGameObjectRigidbody = elementGameObject.GetComponent<Rigidbody>();
        elementGameObjectRigidbody.freezeRotation = true;
        elementGameObjectRigidbody.AddForce(initialOrientation * speed);
    }

    public void Unspawn()
    {
        elementGameObject.SetActive(false);
    }
    
}
