using DR_Tic_Tac_Toe.Enums;

namespace DR_Tic_Tac_Toe.Utils
{
    public static class Gameutils
    {
        /// <summary>
        /// Sets 1 or 2 on a field of the board from DB
        /// </summary>
        /// <param name="field">From 1 to 9 (from "User's perspective")</param>
        /// <param name="boardState">Board from the DB, e.g. 120000000</param>
        /// <param name="iconValue">1 or 2 - states for the DB, throws an error on state 0 (Empty)</param>
        /// <returns></returns>
        public static string SetValueOnAField(int field, string boardState, GameIcons? iconValue = null)
        {
            // TODO: Validate that a field where you're setting a value is empty

            char value;
            if (iconValue == null) value = ((int)GameIcons.X).ToString()[0];
            else if (iconValue == GameIcons.Empty) throw new Exception("Cannot set empty value on a field");
            else value = ((int)iconValue).ToString()[0];

            var boardStateArray = boardState.ToCharArray();

            boardStateArray[field - 1] = value;

            return new string(boardStateArray);
        }
    }
}
