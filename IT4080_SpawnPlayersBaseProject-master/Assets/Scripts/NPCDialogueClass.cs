using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCDialogueClass : MonoBehaviour
{
    public NPCConversation convoNPC;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(convoNPC);
        }
    }
}
