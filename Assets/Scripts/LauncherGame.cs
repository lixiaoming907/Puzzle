using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherGame : MonoBehaviour
{

    public GameObject bestScorePanel;
    public Text bestScoreTxt;

	// Use this for initialization
	void Start () {

#if UNITY_STANDALONE_WIN
	    int heigth = Screen.height;
	    int widht = (int)(heigth*0.5625f);
        Screen.SetResolution(widht, heigth, false);
#endif

        DataManager.GetInstace.ReadScores();
	    if (DataManager.GetInstace.hasBestScore == true)
	    {
            bestScorePanel.SetActive(true);
	        bestScoreTxt.text = DataManager.GetInstace.GetBestScore();
	    }
	    else
	    {
	        bestScorePanel.SetActive(false);
	    }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
