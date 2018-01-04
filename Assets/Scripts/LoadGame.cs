using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {

    public Slider slider;
    public Image image;
    public GameObject whiteToBlackImagePrefab;

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
        StartCoroutine(LoadGameSceneAsync());
    }

    // Update is called once per frame
    void Update()
    {
        //alphe = Mathf.MoveTowards(alphe, 1.0f, 0.1f);
        //image.color = new Color(1,1,1, alphe);
    }

    private IEnumerator LoadGameSceneAsync()
    {
        yield return new WaitForSeconds(1);
        float progress = 0;
        float _progress = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
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
        Instantiate(whiteToBlackImagePrefab, transform.parent);
        yield return new WaitForSeconds(0.5f);
        operation.allowSceneActivation = true;
    }
}
