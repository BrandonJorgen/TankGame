using UnityEngine;

[System.Serializable]
public class Conversation
{
    public string Name;
    
    [TextArea(3, 5)]
    public string[] Sentences;
    
    [Header("Other Settings")]
    public bool IsCutscene;
    [Tooltip("Set to 1 to start after level fade in")]
    public float Delay;
}
