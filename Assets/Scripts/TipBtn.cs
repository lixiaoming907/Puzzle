using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBtn : MonoBehaviour
{
    public Transform tipImage;
    public Text tipNumTxt;
    public int tipNum = 3;

    private bool tipIsOpen = false;
    private Vector3 centerPos;
    private Vector3 buttomPos;

	// Use this for initialization
	void Start () {
	    tipImage.GetChild(0).GetComponent<RawImage>().texture = UIManager._instance.gameImage;
	    SetTipNum(DataManager.GetInstace.difficultLevel);


        centerPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 90.0f));
        buttomPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 90.0f));

	}

    void SetTipNum(int difficultLevel)
    {
        switch (difficultLevel)
        {
            case 1:
                tipNum = 1;
                break;
            case 2:
                tipNum = 2;
                break;
            case 3:
                tipNum = 3;
                break;
        }
    }

    // Update is called once per frame
	void Update () {
	    tipNumTxt.text = tipNum.ToString();

	    if (tipIsOpen)
	    {
            tipImage.transform.localScale = Vector3.MoveTowards(tipImage.transform.localScale, Vector3.one, 0.5f);
	        tipImage.transform.position = Vector3.MoveTowards(tipImage.transform.position, centerPos, 0.8f);
	    }
	    else
	    {
            tipImage.transform.localScale = Vector3.MoveTowards(tipImage.transform.localScale, Vector3.zero, 0.5f);
            tipImage.transform.position = Vector3.MoveTowards(tipImage.transform.position, buttomPos, 0.8f);
        }
	}

    public void OpenTipImage()
    {
        if (tipNum > 0)
        {
            tipIsOpen = true;
            tipNum --;
        }
    }

    public void CloseTipImage()
    {
        tipIsOpen = false;
    }
}
