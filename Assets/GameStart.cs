using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] all_objs = GameObject.FindObjectsOfType<GameObject>();
        GameObject current = Selection.activeGameObject;


        foreach(GameObject g in all_objs)
        {
            if (g != current)
                g.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
