using System.Collections.Generic;
using UnityEngine;

namespace MSCalc.Save
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string PlayerPrefsKey = "CalculatorState";

        public void SaveState(string input, List<string> history)
        {
            var state = new CalcState
            {
                Input = input ?? string.Empty,
                History = history != null ? new List<string>(history) : new List<string>()
            };

            string json = JsonUtility.ToJson(state);
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();
        }

        public CalcState LoadState()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKey))
                return null;

            string json = PlayerPrefs.GetString(PlayerPrefsKey);
            if (string.IsNullOrEmpty(json))
                return null;

            try
            {
                return JsonUtility.FromJson<CalcState>(json);
            }
            catch
            {
                ClearState();
                return null;
            }
        }

        public void ClearState()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
            PlayerPrefs.Save();
        }
    }
}