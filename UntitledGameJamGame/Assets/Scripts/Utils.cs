using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public static Utils instance;

    public void Awake()
    {
        if (Utils.instance != null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } 
        else Destroy(gameObject);
        
    }

    public static void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public static void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static IEnumerator DelayedReturnToMenu()
    {
        yield return new WaitForSecondsRealtime(6);
        ReturnToMenu();
    }

}
