using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class TurnBasedCombatManager
    {
        //This class will be the general combat manager that every character has.
        //Basically, I want every character to have the logic to: calculate turn order, pause to wait for turn,
        // react if they get damaged, react if they get healed, react if they get knocked down, start going on turn,
        // and be connected to the "AI" for NPCs.
        // I should have many scripts for NPCs that the AI controls. As in, how they walk, how party members
        // follow the player, how NPCs can be talked to, how enemies can react and start combat if the player gets
        // too close, how NPCs and enemies walk around in a set path when the scene they're in loads.

        /*
        Here is the rundown of combat:
        - Combat = yes. Combat begins.
        - Determine which characters are in the combat. (In DOS2, some villagers run away from the combat.)
        - Determine turn order from highest to lowest X stat.
        - Characters freeze in place if it is not their turn.
        - Camera follows only the player.
        - A character can choose to move, use abilities, or attack on their turn.
        - Character health bars react to damage and healing even if they're frozen.
        - Characters have animated reaction to if they are attacked while they're frozen.
        - If a character dies, they leave combat.
        - The player can choose to skip their turn. NPCs can skip their turn if they can't move, use an ability, or attack.
        - The combat ends if the player beats the enemies, or if the enemies beat the player.
        - If the player and the party are all dead, then the game resets to the last time they saved the game.
        */

        void GetMousePositionAndPress () //own class
        {

        }

        void MoveUnitsOnGrid () //own class
        {

        }


    }
}
