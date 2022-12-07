using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class CombatGridMono : MonoBehaviour //This needs to be connected to some gameobject in or out of the scene.
    {
        void Start()
        {
            CombatGrid combatGrid = new CombatGrid();
            combatGrid.GridCaller();
        }

        void Update()
        {

        }
    }
}
