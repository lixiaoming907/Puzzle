using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public RawImage image_1;
    public RawImage image_2;

    private float screenFactor;

    // Use this for initialization
    void Start ()
	{
        float realFactor = Screen.width;
        float orginFactor = 1080;
        screenFactor = realFactor/orginFactor;

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.W))
	    {
            float wdith = image_1.GetComponent<RectTransform>().rect.width * GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor * screenFactor;

            float image_1_Pos_x = Screen.width / 2 - (2 * wdith) / 2 + wdith/2;
            float image_2_Pos_x = Screen.width / 2 + (2 * wdith) / 2 - wdith/2;
            float image_Pos_y = Screen.height / 2;

            Vector3 pos_1 = Camera.main.ScreenToWorldPoint(new Vector3(image_1_Pos_x, image_Pos_y, 90));
            Vector3 pos_2 = Camera.main.ScreenToWorldPoint(new Vector3(image_2_Pos_x, image_Pos_y, 90));

            image_1.transform.position = pos_1;
            image_2.transform.position = pos_2;
        }
	}

    void OnGUI()
    {
        Vector3 screenPos1 = Camera.main.WorldToScreenPoint(image_1.transform.position);
        Vector3 screenPos2 = Camera.main.WorldToScreenPoint(image_2.transform.position);
        GUILayout.Label("Canvas的缩放系数 ： " + GameObject.Find("Canvas").GetComponent<CanvasScaler>().scaleFactor);
        GUILayout.Label("屏幕的宽 : " + Screen.width);
        GUILayout.Label("图片的宽 : " + image_1.rectTransform.rect.width);
        GUILayout.Label("图片1的屏幕坐标 ：" + screenPos1);
        GUILayout.Label("图片2的屏幕坐标 ：" + screenPos2);
    }
}
