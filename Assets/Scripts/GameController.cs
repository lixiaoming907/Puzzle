using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public static GameController _instance;

    public GameObject blackImage;
    public GameObject LoadSceneAsyncPanel;
    public Text countDonwTxt;
    public bool canMove = false;
    public float countDonw = 60;
    

    //public int diffcultLevel = 1;

    private bool canCountTime = true;
    private bool canCountDonw = false;
    private int horizontalNum = 5;
    private int verticalNum = 5;
    private float curTime;
    private string curTimeToString;

    private Vector3 originePos;

    void Awake()
    {
        UIManager._instance.animationIsDone += AnimationIsDone;
        _instance = this;
    }

    // Use this for initialization
	IEnumerator Start () {

        DataManager.GetInstace.Init();
	    curTime = countDonw;
	    originePos = countDonwTxt.transform.position;
        yield return new WaitForSeconds(2);
	    GameStart();
	}

    // Update is called once per frame
    void Update () {
        if (canCountDonw && canCountTime)
        {
            curTime -= Time.deltaTime;
            string hour = ((int)(curTime/60)).ToString();
            string second = ((int)(curTime%60)).ToString();
            //countDonwTxt.text = curTime.ToString("##.#");
            curTimeToString = String.Format("{0}.{1}", hour, second);
            countDonwTxt.text = curTimeToString;
            if (curTime <= 10)
            {
                countDonwTxt.color = Color.red;
                Vector3 newPos = originePos + new Vector3(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                countDonwTxt.transform.position = Vector3.MoveTowards(countDonwTxt.transform.position, newPos, 0.1f);
            }
        }
        if (curTime <= 0)
        {
            GameOver();
            countDonwTxt.text = "0.0";
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    void GameStart()
    {
        DataManager.GetInstace.RandomSpritePosition();
    }

    public void GameWin()
    {
        UIManager._instance.animationIsDone -= AnimationIsDone;
        UIManager._instance.GameWin(curTimeToString);
        DataManager.GetInstace.SaveScores(curTimeToString);
        canMove = false;
        canCountDonw = false;
        canCountTime = false;
    }

    void GameOver()
    {
        UIManager._instance.animationIsDone -= AnimationIsDone;
        canMove = false;
        canCountDonw = false;
        canCountTime = false;
        UIManager._instance.GameOver();
    }

    void AnimationIsDone()
    {
        canMove = true;
        canCountDonw = true;
    }

    public void ReTry()
    {
        SceneManager.LoadScene(1);
    }

    public void GoBackToLauncherScene()
    {
        UIManager._instance.animationIsDone -= AnimationIsDone;
        UIManager._instance.RemoveDelegate();
        Instantiate(blackImage, GameObject.Find("Canvas").transform);
        Invoke("ActiveLoadScenePanel", 0.5f);
    }

    private void ActiveLoadScenePanel()
    {
        LoadSceneAsyncPanel.SetActive(true);
    }
}
