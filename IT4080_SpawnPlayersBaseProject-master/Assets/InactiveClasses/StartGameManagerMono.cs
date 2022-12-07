using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class StartGameManagerMono : MonoBehaviour
    {
        //This is the manager that will call scripts that need to be called that a mono will not call. Ex: MousePosition3d needs this to work.

        //ClassRefHandler classRefHandler = new ClassRefHandler();

        void Start()
        {
            
        }

        void Update()
        {
            //classRefHandler.CallMousePosition3D();
            //This should be a coroutine. If I want to make a coroutine for anything, then it's easier to add a mono script to
            // the specific GO. Because it's more important to keep all code seperate to do one thing.
        }
    }
}

