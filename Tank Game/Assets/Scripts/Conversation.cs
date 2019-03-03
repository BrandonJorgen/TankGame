using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation
{
    public string Name;
    
    [TextArea(3, 5)]
    public string[] Sentences;
    
}
