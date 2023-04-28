using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ControlCena : MonoBehaviour
{
    ControlCena instanceScene;
    private void Awake()
    {
        if (instanceScene != this && instanceScene != null) Destroy(this.gameObject);
        else instanceScene = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       CallBackScene();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   

    public void CallScene(string name)
    {
        //SceneManager.LoadScene(name);
        SceneManager.LoadScene(0);
    }

    public void CallSceneConfig()
    {
        SceneManager.LoadScene(2);
    }

    public void CallBackScene()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    //testes
    #region

    #endregion

}
