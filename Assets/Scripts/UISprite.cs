using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISprite : MonoBehaviour, IPointerClickHandler
{
    public int m_Index;

    private Text m_IndexText;
    private RawImage image;
    private Transform target = null;

    private bool isSelect = false;

    private UIManager.AnimationIsDone animationIsDone;

    public void Init(int index, float UVRect_X, float UVRect_Y, float UVRect_W, float UVRect_H, Texture gameImage)
    {
        m_Index = index;

        if (m_IndexText != null)
            m_IndexText.text = m_Index.ToString();
        if (image != null)
        {
            image.texture = gameImage;
            image.uvRect = new Rect(UVRect_X, UVRect_Y, UVRect_W, UVRect_H);
        }
    }

    public void Move(Transform target, int index, UIManager.AnimationIsDone animationIsDone)
    {
        m_Index = index;
        this.target = target;
        this.animationIsDone = animationIsDone;
    }

    void Awake()
    {
        m_IndexText = transform.GetChild(0).GetComponent<Text>();
        image = GetComponent<RawImage>();
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.3f);
        }
        if (animationIsDone != null && Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            //StartCoroutine(PlayScaleAnimation());
            animationIsDone();
        }
    }

    void ToggleSelectState()
    {
        isSelect = !isSelect;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameController._instance.canMove)
            return;
        ToggleSelectState();
        if (isSelect)
            StartCoroutine(PlayScaleAnimation());
        else
        {
            StopAllAnimation();
        }
        UIManager._instance.ClickImage(this);
    }

    public void StopAllAnimation()
    {
        StopAllCoroutines();
        isSelect = false;
        transform.localScale = Vector3.one;
    }

    IEnumerator PlayScaleAnimation()
    {
        while (true)
        {
            while (transform.localScale != new Vector3(0.8f, 0.8f, 0.8f))
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), 0.05f);
                yield return 0;
            }
            while (transform.localScale != Vector3.one)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.05f);
                yield return 0;
            }
            yield return 0;
        }
    }
}
