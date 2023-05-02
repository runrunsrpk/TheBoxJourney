using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumGates.TestBattle;

namespace NumGates
{
    public static class TheBoxCalculator
    {
        #region Position Calculator

        private const int min_character_amount = 5;
        private const int max_character_amount = 9;
        private const float max_screen_horizontal = 4.5f;

        public static Vector3 GetCharacterPositionCenterPivot(int index, int amount, BattleGroup group)
        {
            Vector3 result = Vector3.zero;

            // Calculate space between each characters.
            int divisor = (amount > max_character_amount) ? max_character_amount : amount;
            float characterSpace = max_screen_horizontal / divisor;
            
            // Calculate character layer, less index will stand behind.
            int characterLayer = (amount - index) + 1;

            // Calculate character position by their group.
            switch (group)
            {
                case BattleGroup.Ally:
                    {
                        result = new Vector3(-index * characterSpace, 0, characterLayer);
                        break;
                    }

                case BattleGroup.Enemy:
                    {
                        result = new Vector3(index * characterSpace, 0, characterLayer);
                        break;
                    }
            }

            return result;
        }

        public static Vector3 GetCharacterPositionFrontPivot(int index, int amount, BattleGroup group)
        {
            Vector3 result = Vector3.zero;

            // Calculate space between each characters.
            int divisor = (amount > min_character_amount) ? amount : min_character_amount;
            float characterSpace = max_screen_horizontal / divisor;

            // Calculate character layer, less index will stand behind.
            int characterLayer = (amount - index) + 1;

            // Calculate character position by their group.
            switch (group)
            {
                case BattleGroup.Ally:
                    {
                        result = new Vector3(-index * characterSpace, 0, characterLayer);
                        break;
                    }

                case BattleGroup.Enemy:
                    {
                        result = new Vector3(index * characterSpace, 0, characterLayer);
                        break;
                    }
            }

            return result;
        }

        #endregion
    }
}

