using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager _instance;

    public static DataManager GetInstace
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    SripteData[] realDatas;//真实数据

    private int spriteNum;

    public int difficultLevel = 1;
    public int horizontalNum;
    public int verticalNum;

    public delegate void Done(SripteData[] sriptesDatas);
    public Done AllSpriteIsDone;
    public Done SpriteMoveIsDone;
    public delegate void IsWin();
    public IsWin isWin;
    public bool hasBestScore = false;

    private string bestScore;

    //导入真实数据，根据难度定义所有数据包括横向和纵向的拼图数量,定义拼图的当前数据和真实数据
    public void Init()
    {
        switch (difficultLevel)
        {
            case 1:
                horizontalNum = 4;
                verticalNum = 4;
                break;
            case 2:
                horizontalNum = 5;
                verticalNum = 5;
                break;
            case 3:
                horizontalNum = 6;
                verticalNum = 6;
                break;
        }

        spriteNum = horizontalNum * verticalNum;
        UIManager._instance.Init(difficultLevel, horizontalNum, verticalNum);
        realDatas = new SripteData[spriteNum];

        for (int i = 0; i < spriteNum; i++)
        {

            SripteData sripte = new SripteData(i, i);
            realDatas[i] = sripte;
        }
    }

    public void RandomSpritePosition() //随机打乱所有拼图
    {
        for (int i = 0; i < spriteNum; i++)
        {
            int rollIndex = Random.Range(0, spriteNum);

            SripteData temp = realDatas[i];
            realDatas[i] = realDatas[rollIndex];
            realDatas[rollIndex] = temp;

            realDatas[rollIndex].ExchangeIndex(rollIndex);
            realDatas[i].ExchangeIndex(i);
        }

        //for (int i = 0; i < realDatas.Length; i++)
        //{
        //    Debug.Log(realDatas[i].realIndex);
        //}
        //Debug.Log("===================================");

        //TODO 此处应检查是否已经赢了

        AllSpriteIsDone(realDatas);
    }

    public void ExchangeData(int fromIndex, int toIndex) //交换两个拼图的图片
    {
        SripteData[] moveSriptes = new SripteData[2];

        SripteData temp = realDatas[fromIndex];
        realDatas[fromIndex] = realDatas[toIndex];
        realDatas[toIndex] = temp;

        realDatas[fromIndex].ExchangeIndex(fromIndex);
        realDatas[toIndex].ExchangeIndex(toIndex);

        moveSriptes[0] = realDatas[fromIndex];
        moveSriptes[1] = realDatas[toIndex];

        SpriteMoveIsDone(moveSriptes);
        //for (int i = 0; i < realDatas.Length; i++)
        //{
        //    Debug.Log(realDatas[i].realIndex);
        //}
        CheckWin();
    }

    bool CheckWin() //检查是否赢了
    {
        for (int i = 0; i < spriteNum; i++)
        {
            if (realDatas[i].curIndex != realDatas[i].realIndex)
            {
                return false;
            }
        }
        Debug.Log("赢了！！！！");
        GameController._instance.GameWin();
        if(isWin != null)
            isWin();
        return true;
    }

    bool Prompt(out int fromIndex, out int toIndex) //提示功能，需要检查提示次数是否还够
    {
        fromIndex = 0;
        toIndex = 0;
        return false;
    }

    public void SaveScores(string newScore)
    {
        int mi = int.Parse(newScore.Split('.')[0]);
        int second = int.Parse(newScore.Split('.')[1]);
        int _mi;
        int _second;
        if (hasBestScore)
        {
            _mi = int.Parse(bestScore.Split('.')[0]);
            _second = int.Parse(newScore.Split('.')[1]);
            if ((mi > _mi) || (_mi == mi && second > _second))
            {
                bestScore = newScore;
                PlayerPrefs.SetString("bestScore", bestScore);
                PlayerPrefs.Save();
            }
        }
        PlayerPrefs.SetString("bestScore", newScore);
        PlayerPrefs.Save();
    }

    public void ReadScores()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            hasBestScore = true;
            bestScore = PlayerPrefs.GetString("bestScore");
        }
        else
            hasBestScore = false;
    }

    public string GetBestScore()
    {
        if(!string.IsNullOrEmpty(bestScore))
            return bestScore;
        else
        {
            return "";
        }
    }
}
