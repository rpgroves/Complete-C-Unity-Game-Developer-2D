using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadTime = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadTime);
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else {
            nextScene = 0;
            SceneManager.LoadScene(nextScene);
        }
    }
}
