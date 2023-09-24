using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName;
    public bool gameWin;
    public bool gameLose;
    public Animator anim;
    void Start()
    {
        anim.SetBool("Win", gameWin); // 씬을 불러올 때는 기본적으로 string으로 불러온다.
        anim.SetBool("Lose", gameLose);
    }

    void Update()
    {
        if(Input.GetButtonDown("Submit")) // submit : press any key
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
