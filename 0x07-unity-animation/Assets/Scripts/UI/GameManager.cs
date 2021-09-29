using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string PrevScene = "MainMenu";

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Load(string key)
    {
        PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(key);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPrev()
    {
        SceneManager.LoadScene(PrevScene);
    }

    public void LoadNext()
    {
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (NextScene > SceneManager.sceneCountInBuildSettings - 1)
            NextScene = 0;

        SceneManager.LoadScene(NextScene);
    }
}
