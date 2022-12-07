using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class OldManNPCDialogue : MonoBehaviour
{
    public NPCConversation convoOldManNPC;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(convoOldManNPC);
        }
    }

    public bool IsPlayerOnQuest()
    {
        bool isPlayerOnQuest = ConversationManager.Instance.GetBool("isOnQuest");

        return isPlayerOnQuest;
    }

}
