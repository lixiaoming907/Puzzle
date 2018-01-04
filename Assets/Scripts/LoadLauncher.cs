using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLauncher : MonoBehaviour
{

    public Slider slider;
    public Image image;

    private float alphe = 0;

    void OnEnable()
    {
        Invoke("LoadGameScene", 1);
    }

    // Use this for initialization
    void Start()
    {

    }

    void LoadGameScene()
    {
        StartCoroutine(LoadLauncherSceneAsync());
    }

    // Update is called once per frame
    void Update () {
        alphe = Mathf.MoveTowards(alphe, 1.0f, 0.1f);
        image.color = new Color(1, 1, 1, alphe);
    }

    private IEnumerator LoadLauncherSceneAsync()
    {
        yield return new WaitForSeconds(1);
        float progress = 0;
        float _progress = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        operation.allowSceneActivation = false;
        while (_progress < 1)
        {
            progress = operation.progress;
            if (_progress < progress)
            {
                _progress += 0.01f;
            }
            if (_progress >= 0.9f)
            {
                _progress += 0.01f;
            }
            slider.value = _progress;
            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        operation.allowSceneActivation = true;
    }
}
