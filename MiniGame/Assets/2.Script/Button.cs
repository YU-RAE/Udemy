using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    Text AnyKeyDown;
    
    //Player player;
    void Start()
    {
        AnyKeyDown = GetComponent<Text>();
        StartCoroutine(BlinkText());
        
        //player = GameObject.Find(name: "Player").GetComponent<Player>();
    }

    void Update()
    {
        LoadGame();
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            AnyKeyDown.text = "";
            yield return new WaitForSeconds(0.5f);
            AnyKeyDown.text = "Press Any Key";
            yield return new WaitForSeconds(0.3f);
        }
    }

        public void LoadGame()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("game");

        }
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Start");

    }
}
