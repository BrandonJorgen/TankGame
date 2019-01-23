using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAndData : MonoBehaviour
{
    public FloatData ScoreData;
    public string String;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
    
    void Update()
    {
        text.text = String + " " + ScoreData.Value;
    }
}
