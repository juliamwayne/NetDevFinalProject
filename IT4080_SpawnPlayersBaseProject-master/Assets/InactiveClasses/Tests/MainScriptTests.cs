using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ScriptAssembly;
using System;
using System.Linq;

namespace Tests
{
    public class MainScriptTests
    {
        [Test]
        public void CombatGrid_test()
        {
            var combatGrid = new CombatGrid();

            //combatGrid.TestGrid();
            //TestContext.WriteLine(combatGrid.GenerateGridOnMap());
        }

        /*
            [TestCase(3.05f)]
            [TestCase(6.10f)]
            [TestCase(9.14f)]
            public void CraneBoomHandler_added_too_much(float addedPiece)
            {
                //Test passes if the new boom length is less than the max boom length!

                var boomHandler = new CraneBoomHandler();
                boomHandler.AddExtensionPiece(12.19f);
                boomHandler.AddExtensionPiece(12.19f);

                boomHandler.AddExtensionPiece(addedPiece);
                float totalBoomLength = boomHandler.CalculateTotalBoomLength();

                Assert.LessOrEqual(totalBoomLength, boomHandler.MAB_max_length);
                //TestContext.WriteLine("Boom Length: " + totalBoomLength);
                //TestContext.WriteLine("MAX LENGTH: " + boomHandler.MAB_max_length);
            }
         */
    }
}
