using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private string nextSceneName;

    private void Start() 
    {
        StartCoroutine(WaitBeforeSceneTransition());
    }

    private IEnumerator WaitBeforeSceneTransition()
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(nextSceneName);
    }
}
