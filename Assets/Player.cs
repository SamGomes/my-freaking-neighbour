using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int reputation;
    private bool canAttack;

    private RectTransform UILifeBarPoleRT;
    private RectTransform UILifeBarEndRT;
    private float maxLifeBarSize;

    public Player (GameObject UILifeBarObject, float maxLifeBarSize)
    {
        this.reputation = 100;
        this.canAttack = true;

        this.UILifeBarPoleRT = UILifeBarObject.GetComponentsInChildren<RectTransform>()[2]; //get pole
        this.UILifeBarEndRT = UILifeBarObject.GetComponentsInChildren<RectTransform>()[3]; //get pole
        this.maxLifeBarSize = maxLifeBarSize;
    }

    void Attack(EnvironmentElement envElement)
    {

    }
    
    public void removeReputation(int remove)
    {
        this.reputation -= remove;
        if ( this.reputation < 0 ) { this.reputation = 0; }
        ChangeBarLife(this.reputation);
    }

    public void addReputation(int add)
    {
        this.reputation += add;
        if(this.reputation > 100) { this.reputation = 100; }
        ChangeBarLife(this.reputation);
    }

    public void ChangeBarLife(float life)
    {
        UILifeBarPoleRT.sizeDelta = new Vector2(life/100 * maxLifeBarSize, UILifeBarPoleRT.rect.height);
        UILifeBarEndRT.anchoredPosition = new Vector2(UILifeBarPoleRT.anchoredPosition.x + UILifeBarPoleRT.sizeDelta.x, UILifeBarPoleRT.anchoredPosition.y);
    }

}
