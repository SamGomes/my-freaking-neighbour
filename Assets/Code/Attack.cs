using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Attack : MonoBehaviour
{    
    private GameObject elementGameObject;
    public AttackType type;

    public Vector3 initialPos;
    public Vector3 mediumPos;
    public Vector3 finalPos;

    public Vector3 direction;

    public int probWorking;

    public Vector3 initialOrientation;

    public float movementSpeed = 10.0f;
    public float speed;


    public float count = 0.0f;


    public AttackType GetType() { return type; }

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = initialPos;
        //  direction = Vector3.right;
        initialOrientation = new Vector3(0,1,0);

        Rigidbody elementGameObjectRigidbody = elementGameObject.GetComponent<Rigidbody>();
        elementGameObjectRigidbody.freezeRotation = true;
        elementGameObjectRigidbody.AddForce(initialOrientation * speed);



    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
      //  transform.Translate(direction * movementSpeed * Time.deltaTime);

     /*   if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(initialPos, mediumPos, count);
            Vector3 m2 = Vector3.Lerp(mediumPos, finalPos, count);
            this.transform.Translate(Vector3.Lerp(m1, m2, count));
        }
        */

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
