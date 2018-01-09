using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnsEvent : MonoBehaviour
{

    public GameObject diffcualtChoosePanel;
    public GameObject loadGamePanel;
    public GameObject toggleBlackImagePrefab;
    public GameObject blackToWhiteImagePrefab;
    public GameObject whiteToBlackImagePrefab;
    public ChooseDiffcualtAnimation chooseDiffcualtAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoBack()
    {
        GameController._instance.GoBackToLauncherScene();
    }

    public void StartGame()
    {
        diffcualtChoosePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChooseSample(bool choose)
    {
        DataManager.GetInstace.difficultLevel = 1;
        LoadGame();
    }

    public void ChooseGeneral(bool choose)
    {
        DataManager.GetInstace.difficultLevel = 2;
        LoadGame();
    }

    public void ChooseDiffcualt(bool choose)
    {
        DataManager.GetInstace.difficultLevel = 3;
        LoadGame();
    }

    void LoadGame()
    {
        GameObject go = Instantiate(toggleBlackImagePrefab, transform.parent.parent);
        //go.transform.SetParent(transform.parent.parent);
        go.transform.localScale = Vector3.one;
        Invoke("ActiveLoadGameScenePanel", 0.5f);
        StartCoroutine(AudioChanged());
    }

    private IEnumerator AudioChanged()
    {
        while (true)
        {
            Camera.main.GetComponent<AudioSource>().volume = Mathf.MoveTowards  (Camera.main.GetComponent<AudioSource>().volume, 0, 0.01f);

            yield return 0;
        }
    }

    private void ActiveLoadGameScenePanel()
    {
        loadGamePanel.SetActive(true);
    }

    public void CloseDiffcualtChoosePanel()
    {
        chooseDiffcualtAnim.CloseThis();
        Invoke("ClosePanel", 0.5f);
    }

    void ClosePanel()
    {
        diffcualtChoosePanel.SetActive(false);
    }
}
