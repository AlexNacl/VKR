using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public int SceneNum;
    public Animator transition;

    public void LoadScene(){
        StartCoroutine(LoadLevel(SceneNum));
    }

    IEnumerator LoadLevel(int Index){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
