using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreCounter : MonoBehaviour
{
    public static int score;
    private TextMeshProUGUI mText;
    void Start()
    {
        score = 0;
        mText = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        mText.text = score.ToString();
    }
}
