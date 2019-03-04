using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAndData : MonoBehaviour
{
    public FloatData ScoreData;
    public string String;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        text.text = String + " " + ScoreData.Value;
    }
}
