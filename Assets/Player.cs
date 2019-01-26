using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int reputation;
    private bool canAttack;

    public Player()
    {
        this.reputation = 100;
        this.canAttack = true;
    }

    void attack(EnvironmentElement envElement)
    {

    }

}
