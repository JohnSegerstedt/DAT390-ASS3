using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleBehaviour : MonoBehaviour
{
    public Action OnClicked;
    private Button mButton;
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
    }
}
