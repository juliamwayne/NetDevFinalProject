using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState { STORY, STARTBATTLE, PLAYERTURN, ENEMYTURN, WON, LOST }

public class CombatManagerHandler : NetworkBehaviour
{
    public BattleState state;

    GameObject goPlayer;
    GameObject goEnemy;
    GameObject goCombatMenu;

    public TMPro.TMP_Text txtComabt;
    public TMPro.TMP_Text txtCombatActions;
    TMPro.TMP_Text txtPlayerNumber;

    public NetworkVariable<bool> isCombat = new NetworkVariable<bool>();

    int playerIndex;
    int unitCount; //Counts the number of units that have went in combat.
    public int playerCombatActions = 2;
    public int counterBtnClick = 0;
    int playernum = 0;

    ulong uCurrentPlayerUnit;
    ulong uNextPlayer;

    Button btnAttack;
    Button btnSpecial;
    Button btnBlock;
    public Button btnPlayer0Turn;
    public Button btnPlayer1Turn;
    public Button btnEnemyTurn;

    bool isTurnOver = false;

    public List<int> listPlayerUnits = new List<int>();

    private void Start()
    {
        state = BattleState.STORY;
        goCombatMenu = GameObject.Find("Canvas/CombatMenu");
        goCombatMenu.SetActive(false);

        btnPlayer0Turn.onClick.AddListener(OnButtonClickedPlayer0);
        btnPlayer1Turn.onClick.AddListener(OnButtonClickedPlayer1);
        btnEnemyTurn.onClick.AddListener(OnButtonClickedEnemy1);

        /*
        btnAttack.onClick.AddListener(OnButtonClicked);
        btnSpecial.onClick.AddListener(OnButtonClicked);
        btnBlock.onClick.AddListener(OnButtonClicked);
        */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCombat.Value = !isCombat.Value;
            //TODO: Client can't hit C and activate combat menu.
        }

        if (isCombat.Value == true)
        {
            LaunchCombatState();
            //StartCoroutine(LaunchCombatState());
        }
        /*
        else
        {
            EndCombat();
        }
        */
    }

    void LaunchCombatState()
    {
        //This coroutine should be completed once every unit has gone in combat.

        state = BattleState.STARTBATTLE;

        goCombatMenu.SetActive(true);

        //StartCoroutine(PlayerTurn(GetCurrentPlayerTurn()));
        //PlayerTurn(GetCurrentPlayerTurn());

        uCurrentPlayerUnit = 0;
        PlayerTurn(uCurrentPlayerUnit);

        //yield return StartCoroutine(PlayerTurn());

        //TODO: Check if players or enemy is dead.

        //yield return StartCoroutine(EnemyTurn());

        //EndCombat();
    }

    void EndCombat()
    {
        goCombatMenu.SetActive(false);
    }

    void PlayerTurn(ulong uCurrentPlayer)
    {
        state = BattleState.PLAYERTURN;

        uCurrentPlayerUnit = uCurrentPlayer;
        //ulong player = uCurrentPlayer;

        /*
        if (player == NetworkManager.Singleton.LocalClientId)
        {
            playerCombatActions = 2;

            txtComabt.text = ($"Your turn!");
            txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
        }
        else
        {
            txtComabt.text = ($"Waiting on Player #{uCurrentPlayer}");
            txtCombatActions.text = ($"Combat Actions: 0");
        }
        */

        //yield return new WaitUntil(() => counterBtnClick == 2);

        txtComabt.text = ($"---");
        txtCombatActions.text = ($"---");

        StartCoroutine(Player0Turn());
    }

    void NextCombatUnitTurn()
    {
        uNextPlayer = uCurrentPlayerUnit + 1;

        //if ((int)uNextPlayer < GetPlayerIndex())
        if((int)uNextPlayer < 2)
        {
            Debug.Log("Next player turn: " + uNextPlayer); //TODO: Infinite. Won't leave this.
            PlayerTurn(uNextPlayer);
            //StartCoroutine(PlayerTurn(uNextPlayer));
            return;
        }
        else
        {
            Debug.Log("Enemy turn!");
            EnemyTurn();
            return;
        }

    }

    IEnumerator Player0Turn()
    {
        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            playerCombatActions = 2;

            txtComabt.text = ($"Your turn!");
            txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
        }
        else
        {
            txtComabt.text = ($"Waiting on Player #0");
            txtCombatActions.text = ($"Combat Actions: 0");
        }

        yield return new WaitForSeconds(1f);
        //yield return new WaitUntil(() => counterBtnClick == 1);
    }

    IEnumerator Player1Turn()
    {
        if (NetworkManager.Singleton.LocalClientId == 1)
        {
            playerCombatActions = 2;

            txtComabt.text = ($"Your turn!");
            txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
        }
        else
        {
            txtComabt.text = ($"Waiting on Player #1");
            txtCombatActions.text = ($"Combat Actions: 0");
        }

        yield return new WaitForSeconds(1f);
        //yield return new WaitUntil(() => counterBtnClick == 1);

    }

    IEnumerator Enemy1Turn()
    {
        txtComabt.text = ($"Enemy Turn");
        txtCombatActions.text = ($"Combat Actions: 0");

        yield return new WaitForSeconds(1f);
        //yield return new WaitUntil(() => counterBtnClick == 1);
    }

    //IEnumerator EnemyTurn()
    void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;

        Debug.Log("Enemy turn!");
        txtComabt.text = ($"Enemy turn.");
        txtCombatActions.text = ($"Combat Actions: 0");
    }

    void WinCombat()
    {
        state = BattleState.WON;
    }

    void LoseCombat()
    {
        state = BattleState.LOST;
    }

    int GetPlayerIndex()
    {
        //playerIndex = GameData.Instance.FindPlayerIndex(NetworkManager.LocalClientId);
        int playerIndex = 2; //TODO: Make dynamic.

        for (int i = 0; i < playerIndex; i++)
        {
            listPlayerUnits.Add(i);
        }

        return playerIndex; 
    }

    ulong GetCurrentPlayerTurn()
    {
        /*
        foreach (int player in listPlayerUnits)
        {
            return player;
        }
        */
        return 0; //TODO: Make dynamic.
    }

    void OnButtonClickedPlayer0()
    {
        StartCoroutine(Player0Turn());

        StopCoroutine(Player1Turn());
        StopCoroutine(Enemy1Turn());
    }

    void OnButtonClickedPlayer1()
    {
        StartCoroutine(Player1Turn());

        StopCoroutine(Player0Turn());
        StopCoroutine(Enemy1Turn());
    }

    void OnButtonClickedEnemy1()
    {
        StartCoroutine(Enemy1Turn());

        StopCoroutine(Player0Turn());
        StopCoroutine(Player1Turn());
    }

    /*
    void OnButtonClicked() 
    {
        if (uCurrentPlayerUnit != NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Not your turn!");
            return;
        }
        if (state != BattleState.PLAYERTURN)
        {
            Debug.Log("Not your turn!");
            return;
        }
        if (counterBtnClick == 2)
        {
            NextCombatUnitTurn();
            return;
        }

        counterBtnClick++;
        playerCombatActions--;

        txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
        txtComabt.text = ($"Player {uCurrentPlayerUnit} used Attack!");
    }
    */

    void CombatDialogueUpdates(string message)
    {
        txtComabt.text = ($"Player {uCurrentPlayerUnit} used {message}!");
    }

    void CombatActionsUpdates(int actions)
    {
        txtCombatActions.text = ($"Combat Actions: {actions}");
    }

    IEnumerator SetCurrentPlayerUnitVariable(ulong player)
    {
        uCurrentPlayerUnit = player;

        yield return new WaitForSeconds(1f);
    }

    IEnumerator SetCombatActionsVariable(int actions)
    {
        playerCombatActions = actions;

        yield return new WaitForSeconds(1f);
    }

    IEnumerator IsTurnOver()
    {
        //yield return new WaitWhile(() => playerCombatActions > 0);
        yield return new WaitUntil(() => playerCombatActions == 0);

        //NextCombatUnitTurn(uCurrentPlayerUnit);
    }

}



/*
   // ----------------------------------------------------------------------------------
   // ----- CHEATING: HARD CODING VALUES! -----
   // ----------------------------------------------------------------------------------

   IEnumerator Player0Turn()
   //void Player0Turn()
   {
       if (NetworkManager.Singleton.LocalClientId == 0)
       {
           playerCombatActions = 2;

           txtComabt.text = ($"Your turn!");
           txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
       }
       else
       {
           txtComabt.text = ($"Waiting on Player #0");
           txtCombatActions.text = ($"Combat Actions: 0");
       }

       //yield return new WaitUntil(() => counterBtnClick == 2);
       yield return new WaitForSeconds(10f);

       //counterBtnClick = 0;
       //StartCoroutine(Player1Turn());
   }

   IEnumerator Player1Turn()
   //void Player1Turn()
   {
       //StopCoroutine(Player0Turn());

       if (NetworkManager.Singleton.LocalClientId == 1)
       {
           playerCombatActions = 2;

           txtComabt.text = ($"Your turn!");
           txtCombatActions.text = ($"Combat Actions: " + playerCombatActions);
       }
       else
       {
           txtComabt.text = ($"Waiting on Player #1");
           txtCombatActions.text = ($"Combat Actions: 0");
       }

       //yield return new WaitUntil(() => counterBtnClick == 2);
       yield return new WaitForSeconds(10f);

       //StartCoroutine(Enemy1Turn());
   }

   IEnumerator Enemy1Turn()
   //void Enemy1Turn()
   {
       //StopCoroutine(Player1Turn());

       state = BattleState.ENEMYTURN;

       txtComabt.text = ($"Enemy turn.");
       txtCombatActions.text = ($"Combat Actions: 0");

       yield return new WaitForSeconds(10f);

       //EndCombat();
   }

   // ----------------------------------------------------------------------------------
   // ----- CHEATING: HARD CODING VALUES! -----
   // ----------------------------------------------------------------------------------
   */


/*
    void OnButtonClickedPlayer0()
    {
        StartCoroutine(Player0Turn());

        StopCoroutine(Player1Turn());
        StopCoroutine(Enemy1Turn());
    }

    void OnButtonClickedPlayer1()
    {
        StartCoroutine(Player1Turn());

        StopCoroutine(Player0Turn());
        StopCoroutine(Enemy1Turn());
    }

    void OnButtonClickedEnemy1()
    {
        StartCoroutine(Enemy1Turn());

        StopCoroutine(Player0Turn());
        StopCoroutine(Player1Turn());
    }
    */



/*
    void OnAttackClicked() 
    {
        //TODO: Every time you hit this button, it starts with actions = 2, then goes to actions = 1. Then when you hit it again it's actions = 2.

        if (uCurrentPlayerUnit != NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Not your turn!");
            return;
        }

        if (playerCombatActions == 0)
        {
            NextCombatUnitTurn();
            return;
        }

        CombatDialogueUpdates("Attack");

        playerCombatActions -= 1;
        
        //StartCoroutine(IsTurnOver());
    }

    void OnSpecialClicked()
    {
        if (uCurrentPlayerUnit != NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Not your turn!");
            return;
        }

        if (playerCombatActions == 0)
        {
            NextCombatUnitTurn();
            return;
        }

        Debug.Log("Special!");
        playerCombatActions -= 1;

        //StartCoroutine(IsTurnOver());
    }

    void OnBlockClicked()
    {
        if (uCurrentPlayerUnit != NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("Not your turn!");
            return;
        }

        if (playerCombatActions == 0)
        {
            NextCombatUnitTurn();
            return;
        }

        Debug.Log("Block!");
        playerCombatActions -= 1;

        //StartCoroutine(IsTurnOver());
    }
    */


/*
    IEnumerator DetermineUnitTurn(int playernum)
    {
        if (playernum == 0)
        {
            StopCoroutine(Player1Turn());
            StopCoroutine(Enemy1Turn());

            StartCoroutine(Player0Turn());
        }
        else if (playernum == 1)
        {
            StopCoroutine(Player0Turn());
            StopCoroutine(Enemy1Turn());

            StartCoroutine(Player1Turn());
        }
        else
        {
            StopCoroutine(Player0Turn());
            StopCoroutine(Player1Turn());

            StartCoroutine(Enemy1Turn());
        }

        yield return new WaitForSeconds(10f);
    }
    */