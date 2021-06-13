using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimation;
    [SerializeField] private float transitionTime = 1f;

    [FMODUnity.EventRef] public string LevelCompleteEvent = "";

    void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transitionAnimation.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextScene"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(LevelCompleteEvent, transform.position);
            NextLevel();
        }
        else if (collision.gameObject.CompareTag("Restart"))
            RestartLevel();
    }
}
