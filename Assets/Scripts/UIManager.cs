using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager _instance;

    public GameObject imagePrefab;
    public GameObject transformPrefab;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public Transform[] animationPosesTrans;
    public Transform imagesParent;
    public Transform transformParent;
    public Texture gameImage;

    public string[] imagesPath;

    private UISprite[] images;
    private Transform[] spritesTransform;
    private UISprite selectedSprite;

    public delegate void AnimationIsDone();

    public AnimationIsDone animationIsDone;

    void Awake()
    {
        _instance = this;

        DataManager.GetInstace.AllSpriteIsDone += AllImageIsDone;
        DataManager.GetInstace.SpriteMoveIsDone += SpriteMoveIsDone;
    }

    // Use this for initialization
    void Start()
    {

    }

    public void Init(int difficalLevel, int horizontalNum, int verticalNum)
    {
        images = new UISprite[horizontalNum * verticalNum];
        spritesTransform = new Transform[horizontalNum * verticalNum];
        int imageIndex = Random.Range(0, imagesPath.Length);
        gameImage = Resources.Load<Texture>(imagesPath[imageIndex]);
        SpriteEditor(horizontalNum, verticalNum);
    }

    void SpriteEditor(int horizontalNum, int verticalNum)
    {
        CreateImages(horizontalNum, verticalNum);

        float perPixelX = 1.0f/horizontalNum;
        float perPixelY = 1.0f/verticalNum;

        int curHorizontalIndex = 0;
        int curVerticalIndex = 1;
        for (int i = 0; i < images.Length; i++)
        {
            float x = perPixelX*curHorizontalIndex;
            float y = 1 - perPixelY*curVerticalIndex;

            curHorizontalIndex++;
            curHorizontalIndex %= horizontalNum;
            if (curHorizontalIndex == 0)
            {
                curVerticalIndex ++;
            }
            images[i].Init(i, x, y, perPixelX, perPixelY, gameImage);
        }

    }

    void CreateImages(int horizontalNum, int verticalNum)
    {
        int rectWidth = 900/horizontalNum;
        int rectHeight = 900/verticalNum;
        imagePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
        float wdith = imagePrefab.GetComponent<RectTransform>().rect.width * GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor * (Screen.width * 1.0f/1080.0f);
        float height = imagePrefab.GetComponent<RectTransform>().rect.height * GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor * (Screen.height * 1.0f / 1920.0f);


        float start_x = Screen.width/2.0f - (horizontalNum* wdith) /2 + wdith/2;
        float start_y = Screen.height/2.0f + (verticalNum* height / 2) - height / 2;

        //Debug.Log(Camera.main.ScreenToWorldPoint(imagesParent.position));

        int curIndex_X = 0;
        int curIndex_Y = 0;

        float x = start_x;
        float y = start_y;

        for (int i = 0; i < horizontalNum * verticalNum; i++)
        {
            x = start_x + wdith * curIndex_X;
            y = start_y - height * curIndex_Y;
            curIndex_X++;
            curIndex_X %= horizontalNum;

            if (curIndex_X == 0)
                curIndex_Y ++;


            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 90));
            GameObject go = Instantiate(imagePrefab, imagesParent) as GameObject;
            //go.transform.SetParent(imagesParent);
            go.transform.position = worldPos;
            go.transform.localScale = Vector3.one;

            GameObject t = Instantiate(transformPrefab) as GameObject;
            t.transform.SetParent(transformParent);
            t.transform.position = worldPos;
            t.transform.localScale = Vector3.one;

            spritesTransform[i] = t.transform;

            images[i] = go.GetComponent<UISprite>();
        }
    }

    void SpriteMoveIsDone(SripteData[] sriptesdatas)
    {
        images[sriptesdatas[0].realIndex].Move(spritesTransform[sriptesdatas[0].curIndex], sriptesdatas[0].curIndex, animationIsDone);
        images[sriptesdatas[1].realIndex].Move(spritesTransform[sriptesdatas[1].curIndex], sriptesdatas[1].curIndex, animationIsDone);

        images[sriptesdatas[0].realIndex].StopAllAnimation();
        images[sriptesdatas[1].realIndex].StopAllAnimation();
        //UISprite temp = images[sriptesdatas[0].curIndex];
        //images[sriptesdatas[0].curIndex] = images[sriptesdatas[1].curIndex];
        //images[sriptesdatas[1].curIndex] = temp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AllImageIsDone(SripteData[] sriptesDatas)
    {
        StartCoroutine(PlayInitAnimation(sriptesDatas));
    }

    private IEnumerator PlayInitAnimation(SripteData[] sriptesDatas)
    {
        int index = 0;
        for (int i = 0; i < images.Length; i++)
        {
            index ++;
            index %= animationPosesTrans.Length;
            images[i].Move(animationPosesTrans[index], 1, null);
        }
        yield return new WaitForSeconds(2);

        for (int i = 0; i < images.Length; i++)
        {
            images[sriptesDatas[i].realIndex].Move(spritesTransform[sriptesDatas[i].curIndex], sriptesDatas[i].curIndex, animationIsDone);
        }
    }

    public void ClickImage(UISprite uiSprite)
    {
        if (!GameController._instance.canMove)
            return;

        if (selectedSprite != null && uiSprite != null && uiSprite == selectedSprite)
        {
            selectedSprite = null;
            return;
        }

        if (selectedSprite == null)
        {
            selectedSprite = uiSprite;
        }
        else
        {
            DataManager.GetInstace.ExchangeData(selectedSprite.m_Index, uiSprite.m_Index);
            //Debug.Log("交换" + selectedSprite.m_Index + " " + uiSprite.m_Index);
            selectedSprite = null;
            GameController._instance.canMove = false;
        }
    }

    public void GameWin(string lastTime)
    {
        RemoveDelegate();
        WinPanel.transform.GetChild(2).GetComponent<Text>().text = lastTime;
        Invoke("ShowWin",0.5f);
    }

    void ShowWin()
    {
        WinPanel.SetActive(true);
    }

    public void GameOver()
    {
        RemoveDelegate();
        Invoke("ShowLose", 0.5f);
    }

    void ShowLose()
    {
        LosePanel.SetActive(true);
    }

    public void RemoveDelegate()
    {
        DataManager.GetInstace.AllSpriteIsDone -= AllImageIsDone;
        DataManager.GetInstace.SpriteMoveIsDone -= SpriteMoveIsDone;
    }
}
