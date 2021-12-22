using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject loadingScreen;
    public Animator loadingScreenAnimator;
    public GameObject PersistentCamera;

    public float totalSceneProgress;

    private ProgressBar bar;

    public GameObject LoadingBar;

    public void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);

        bar = LoadingBar.GetComponent<ProgressBar>();
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SANDBOX, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadLevel2()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void MoveLevel2()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.SANDBOX));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void BackToMainMenu()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        Time.timeScale = 1f;

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.SANDBOX));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void BackToMainMenu2()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        Time.timeScale = 1f;

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.LEVEL2));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void ResetLevel1()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        Time.timeScale = 1f;

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.SANDBOX));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SANDBOX, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void ResetLevel2()
    {
        PersistentCamera.SetActive(true);
        loadingScreen.SetActive(true);

        Time.timeScale = 1f;

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.LEVEL2));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.LEVEL2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i=0; i<scenesLoading.Count;i++)
        { 
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                bar.CurrentProgress = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }

        loadingScreen.SetActive(false);
        PersistentCamera.SetActive(false);
    }
}
