using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOperator : MonoBehaviourSingleton<SceneOperator>
{
    public Animator animator;
    public void LoadNextScene()
    {
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings)
            Debug.LogError("Trying to load next scene from scene with maximal build index.");
#endif

        LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex < 1)
            Debug.LogError("Trying to load previous scene from scene with build index of 0 or less.");
#endif

        LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void ResetScene()
    {
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex < 0)
            Debug.LogError("Trying to load previous scene from scene with build index less than 0.");
#endif

        LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int sceneBuildIndex) => this.StartCoroutine(
            routine: this.LoadSceneWithDelayProcess(sceneBuildIndex: sceneBuildIndex)
        );

    [SerializeField] private float _sceneLoadDelay = 2f;
    public float _SceneLoadDelay => this._sceneLoadDelay;

    private IEnumerator LoadSceneWithDelayProcess(int sceneBuildIndex)
    {
        animator.ResetTrigger("End");
        this.animator.SetTrigger("End");

        // AudioManager.Instance.ReduceBackgroundVolume();

        yield return new WaitForSecondsRealtime(this._sceneLoadDelay);

        SceneManager.LoadScene(sceneBuildIndex: sceneBuildIndex);

        animator.ResetTrigger("Start");
        animator.SetTrigger("Start");

        // AudioManager.Instance.RestoreBackgroundVolume();
    }
}