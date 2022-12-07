using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptAssembly
{
    public class CombatGrid
    {
        int gridRow = 10;
        int gridCol = 10;

        int[,] gridArray;

        float flCellSize;

        GameObject goCubeCell;

        public List<GameObject> Tiles = new List<GameObject>();

        List<Tuple<float, float>> Grid = new List<Tuple<float, float>>();

        public void GridCaller()
        {
            PositionGridPiecesOnMap();
        }

        void PositionGridPiecesOnMap()
        {
                for (int row = 0; row < gridRow; row++)
                {
                    for (int col = 0; col < gridCol; col++)
                    {
                        InstantiateGridCell().transform.localPosition = new Vector3(row, 0, col);
                        //Note: The grid is not centered on (0,0) on the plane. I changed the plane's position to be where the grid is created.
                    }
                }
        }

        GameObject InstantiateGridCell()
        {
            GameObject goCubeCell = GameObject.Instantiate(Resources.Load("Prefabs/CubeCell")) as GameObject;

            Tiles.Add(goCubeCell);

            return goCubeCell;
        }

        void GridCombatMovement() //NEW CLASS
        {
            //The logic that characters can move from one piece on the grid to the other. Can't leave grid.
            //Player starts from a cell. The player can select 4 squares away from it in any direction.
            //Logic will stop player from selecting a cell that is more than 4 squares away. Can still hover over all cells.
        }

        public void MakeShapeInConsole()
        {
            //TIP: Each call to Debug.Log is a new line.

            for (int row = 1; row <= 5; row++) //More amount of lines vertically. Ex if 3:  "*****"
                                               //                                           "*****"
                                               //                                           "*****"
            {
                string str = "";

                for (int column = 1; column <= 5; column++) //Longer lines horizontally. Ex if 15: "***************"
                {
                    str += "*";
                }

                Debug.Log(str);
            }

        }

    } //class
} //namespace









        /*
        public void MakeGrid (int width, int height, float flCellSize)
        {
            this.width = width;
            this.height = height;
            this.flCellSize = flCellSize;

            gridArray = new int[width, height];

            //Cycle through multi dimensional array:
            for (int x = 0; x < gridArray.GetLength(0); x++) //GetLength(0) means the first dimension in the multi dimensional array.
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Console.WriteLine(x + " " + y);
                }
            }

        }

        Vector3 GetCellPosition (int xCell, int yCell) //Note that good practice is not having to put in variables to methods.
        {
            return new Vector3(xCell, yCell) * flCellSize;
        }
        */

            /*
             float currentY = 0;
             float startPosition = boomHandler.boomBotLength; //+ boomHandler.boomStartsAt;

             for (int i = 0; i < boomClones.Count; i++)
             {
                float y = startPosition + currentY + (boomClones[i].Item2) / 2;
                boomClones[i].Item1.transform.localPosition = new Vector3(0, y, 0);

                currentY += boomClones[i].Item2;
             }

             */
