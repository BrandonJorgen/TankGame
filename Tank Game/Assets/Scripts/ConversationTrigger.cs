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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(Dialogue.Delay);
        FindObjectOfType<ConversationManager>().StartConversation(Dialogue);
    }
}
//TODO Last time on Tankgame-Z, you were trying to add a optional delay to the conversation system