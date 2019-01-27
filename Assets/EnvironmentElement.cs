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
    private string som;

    public EnvironmentElement(EnvElementType type, GameObject elementGameObjectPrefab, Vector3 initialPos, Vector3 initialOrientation, float speed, string som)
    {
        this.elementGameObject = Instantiate(elementGameObjectPrefab);

        this.initialPos = initialPos;
        this.initialOrientation = initialOrientation;

        this.type = type;
        this.speed = speed;

        this.som = som;

        elementGameObject.SetActive(false);
        
    }

    public void Spawn()
    {
        elementGameObject.SetActive(true);
        elementGameObject.transform.position = initialPos;

        Rigidbody elementGameObjectRigidbody = elementGameObject.GetComponent<Rigidbody>();
        elementGameObjectRigidbody.freezeRotation = true;
        elementGameObjectRigidbody.AddForce(initialOrientation * speed);

        GameObject sound = GameObject.FindGameObjectWithTag("Audio");
        AudioSource aux1 = sound.GetComponent<AudioSource>();
        var aux = Resources.Load(this.som) as AudioClip;
        aux1.PlayOneShot(aux);

    }

    public void Unspawn()
    {
        elementGameObject.SetActive(false);
    }
    
    public EnvElementType GetType()
    {
        return type;
    }

}
