using System.Collections.Generic;

namespace MSCalc.Save
{
    public interface ISaveSystem
    {
        void SaveState(string input, List<string> history);

        CalcState LoadState();

        void ClearState();
    }

    public class CalcState
    {
        public string Input;
        public List<string> History = new List<string>();
    }
}