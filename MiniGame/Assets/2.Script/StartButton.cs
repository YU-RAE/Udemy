using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button button;
    public void OnClickButton()
    {
        SceneManager.LoadScene("game");
    }
    void Start()
    {
        button = GetComponent<Button>();

    }

    void Update()
    {
        
    }
}
