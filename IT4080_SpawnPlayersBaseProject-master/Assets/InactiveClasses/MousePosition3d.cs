using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class MousePosition3d 
    {
        //In this video, it shows how to have the mouse interact with buildings or just the ground. I'm actually not sure what I prefer.
        //https://www.youtube.com/watch?v=0jTPKz3ga4w

        private Camera mainCam;

        Ray ray;

        public void MousePosition3dManager()
        {
            //Search the Hierarchy for the main camera. Attach to it. Then every time the mouse moves, call the coroutine.

            mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
            //Or mainCam = Camera.main;

            ray = mainCam.ScreenPointToRay(Input.mousePosition);

            //GetInputMousePosition();
            //RayHitMouseClick();
        }

        void GetInputMousePosition()
        { 
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                //transform.position = raycastHit.point; //In the video, a sphere is following the mouse.
                Debug.Log(Input.mousePosition);
            }

        }

        void RayHitMouseClick()
        {
            //if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit raycastHit))
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //If you click the left mouse button AND the camera ray is hitting an object.

                    //Debug.Log("Clicked on " + raycastHit.transform.name);
                    Debug.Log("You clicked on something!");
                }
            }
        }
    }
}
