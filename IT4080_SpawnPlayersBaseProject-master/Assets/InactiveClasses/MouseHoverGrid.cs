using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class MouseHoverGrid
    {
        Color mouseOverColor = Color.blue;
        Color originalColor = Color.red;

        MeshRenderer meshRendererGridCell;

        GameObject goGridCell;

        public Vector3 vecMouseWorldPos;

        Plane plane = new Plane(Vector3.up, 0);

        //ClassRefHandler classRefHandler = new ClassRefHandler();

        public void MouseHoverGridManager()
        {
            goGridCell = GameObject.Find("CubeCell"); //GO needs to be in scene already.

            meshRendererGridCell = goGridCell.GetComponent<MeshRenderer>();

            //GetMouseWorldPosition();

            //classRefHandler.CallMousePosition3D();
        }

        /*
        void GetMouseWorldPosition()
        {
            float distance;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out distance))
            {
                vecMouseWorldPos = ray.GetPoint(distance);
                Debug.Log(distance);
            }
        }
        */

        void OnMouseOverGridCell()
        {
            //If ray from MousePosition3d hits goGridCell, then change the color. If it leaves the goGridCell, change color back.

            meshRendererGridCell.material.color = mouseOverColor;
        }

        void OnMouseExitGridCell()
        {
            meshRendererGridCell.material.color = originalColor;
        }

        void CellMeshChange()
        {
            //The player moves the mouse and an individual cell changes color.

            //Fetch the mesh renderer component from the GameObject
            //meshRendererGridCell = GetComponent<MeshRenderer>();
            //Fetch the original color of the GameObject
            originalColor = meshRendererGridCell.material.color;
        }

        void SelectGridCell() //Different class for the logic of combat, like how you can't select something that's too far away.
        {
            //The logic of keeping the information of which cell the player clicked.

            //Future methods will do this: You hover over a square and it changes color. You click it. If that is the right amount
            // of squares you can move to, then the current party member walks to that square through the grid.
        }
    }
}
