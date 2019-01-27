using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{

    public Button yourButton;

    void Start()
    {
        var btn = transform.GetComponentInChildren<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("FHV_UIScene");
    }

}
