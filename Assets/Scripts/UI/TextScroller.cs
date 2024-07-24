using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextScroller : MonoBehaviour
{
    private TMP_Text mText;


    private const float ScrollDelay = 0.05f;

    public Action OnScrollComplete;
    Coroutine ScrollingCoro;

    public bool Skip = false;

    private void Awake()
    {
        mText = GetComponent<TMP_Text>();
    }

    public bool PlayString(string text)
    {
        if (ScrollingCoro != null)
            return false;


        ScrollingCoro = StartCoroutine(ScrollText(text));
        return true;
    }

    public void SkipScroll()
    {
        Skip = true;
        
    }

    private void StopTheScrollCoroutine()
    {
        if(ScrollingCoro != null)
            StopCoroutine(ScrollingCoro);
        ScrollingCoro = null;
        Skip = false;
    }
    public IEnumerator ScrollText(string text)
    {
        mText.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            if(Skip == true)
            {
                mText.text = text;
                OnScrollComplete?.Invoke();
                StopTheScrollCoroutine();
            }
            mText.text += text[i];
            yield return new WaitForSeconds(ScrollDelay);
        }


        OnScrollComplete?.Invoke();
        ScrollingCoro = null;
    }

    public void Clear()
    {
        mText.text = "";
    }
}
