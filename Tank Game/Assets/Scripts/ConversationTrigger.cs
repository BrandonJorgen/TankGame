using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationTrigger : MonoBehaviour
{
    //SET THIS SCRIPT ON WHATEVER YOU WANT TO USE TO START THE CONVERSATION
    public Conversation Dialogue;
    [Space(10)]
    public UnityEvent Portrait;

    public void TriggerConversation()
    {
        FindObjectOfType<ConversationManager>().StartConversation(Dialogue);
        Portrait.Invoke();
        //Don't forget to activate only the portrait of the person speaking and disable everyone else
    }
}
