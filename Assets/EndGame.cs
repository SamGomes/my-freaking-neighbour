using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var aux = GameObject.FindGameObjectWithTag("P1");
        var aux1 = GameObject.FindGameObjectWithTag("P2");

        aux.SetActive(false);
        aux1.SetActive(false);


        if (Global._players.Count == 2)
        {
            var p1 = Global._players[0].reputation;
            var p2 = Global._players[1].reputation;

            if (p1 > p2)
            {
                aux.SetActive(true);
            }
            else
            {
                aux1.SetActive(true);
            }
        }
    }

}
