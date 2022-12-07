using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

    public class WraithNPCDialogue : MonoBehaviour
    {
        public NPCConversation convoWraithNPC;

        BattleState state;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ConversationManager.Instance.StartConversation(convoWraithNPC);
            }
        }

        void QuestYesNo()
        {
            bool isOnQuest = ConversationManager.Instance.GetBool("isOnQuest");

            if (isOnQuest == true)
            {
                ConversationManager.Instance.SetBool("isQuest", true);
                Debug.Log("Quest active!");
            }
            else
            {
                ConversationManager.Instance.SetBool("isQuest", false);
                Debug.Log("Quest inactive!");
            }
        }

    }
