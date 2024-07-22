using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private TMP_Text Out_OptionText;
    [SerializeField] private Image selector;

    private void Awake()
    {
        selector.gameObject.SetActive(false);
    }

    public void SetOption(string _text, bool _selected)
    {
        Out_OptionText.text = _text;
        selector.gameObject.SetActive(_selected);
    }

    public void SetSelected(bool _selected)
    {
        selector.gameObject.SetActive(_selected);
    }

    public void Clear()
    {
        Out_OptionText.text = "";
        selector.gameObject.SetActive(false);
    }
}
