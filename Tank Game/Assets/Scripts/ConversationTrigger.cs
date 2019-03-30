using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationTrigger : MonoBehaviour
{
    //SET THIS SCRIPT ON WHATEVER YOU WANT TO USE TO START THE CONVERSATION
    public Conversation Dialogue;

    public void TriggerConversation()
    {
        FindObjectOfType<ConversationManager>().StartConversation(Dialogue);
    }
}