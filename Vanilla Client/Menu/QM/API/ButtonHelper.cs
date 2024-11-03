// /*
//  *
//  * VanillaClient - ButtonHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

namespace Vanilla.Menu.QM.API
{
    internal class ButtonHelper
    {
        private int counterX = 1; // Start at 1
        private int counterY = 0; // Start at 0

        internal int NextXValue
        {
            get
            {
                var currentXValue = counterX; // Capture current value of CounterX
                counterX++; // Increment CounterX

                // Reset CounterX and increment CounterY if it exceeds 4
                if (counterX > 4)
                {
                    counterX = 1; // Reset to 1 when it exceeds 4
                    counterY++; // Increment CounterY
                    return currentXValue--;
                }

                return currentXValue; // Return the captured value
            }
        }

        internal int NextYValue
        {
            get
            {
                if (counterY > 3)
                {
                    counterY = 0; // Reset CounterY when it exceeds 3
                }

                return counterY; // Return the current value of CounterY
            }
        }
    }
}
