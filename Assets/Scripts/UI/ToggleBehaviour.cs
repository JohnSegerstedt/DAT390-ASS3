using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleBehaviour : MonoBehaviour
{
    public Action OnClicked;
    private Button mButton;
    private Image mImage;
    private TextMeshProUGUI mText;
    public Sprite Sprite {
        get => mImage.sprite;
        set { mImage.sprite = value; }
    }
    public string Cost {
        get => mText.text; set { mText.text = value; }
    }
    public void Click()
    {
        OnClicked?.Invoke();
    }
    public void SelectedChanged(bool selected)
    {
        mButton.interactable = !selected;
    }
    private void Awake()
    {
        mButton = GetComponent<Button>();
        mImage = GetComponentInChildren<Image>();
        mText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
