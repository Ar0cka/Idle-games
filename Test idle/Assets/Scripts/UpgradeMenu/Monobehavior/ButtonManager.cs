using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UpgradeMenu.Monobehavior
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttonList;
        private List<ButtonsData> _dataList = new List<ButtonsData>();

        private void Awake()
        {
            LoadButtonStates();
        }

        public void SaveButtonStates()
        {
            _dataList.Clear();

            foreach (var button in _buttonList)
            {
                var state = new ButtonsData()
                {
                    isInteractable = button.interactable,
                    name = button.name
                };
                
                _dataList.Add(state);
            }
            
            PlayerPrefs.SetInt("ButtonCount", _dataList.Count);
            for (int i = 0; i < _dataList.Count; i++)
            {
                PlayerPrefs.SetString($"Button_{i}_name", _dataList[i].name);
                PlayerPrefs.SetInt($"Button_{i}_Interactable", _dataList[i].isInteractable ? 1 : 0);
            }
            PlayerPrefs.Save();
        }

        public void LoadButtonStates()
        {
            int buttonCount = PlayerPrefs.GetInt("ButtonCount", 0);
            _dataList.Clear();

            for (int i = 0; i < buttonCount; i++)
            {
                string name = PlayerPrefs.GetString($"Button_{i}_name");
                bool isInteractable = PlayerPrefs.GetInt($"Button_{i}_Interactable") == 1;
                _dataList.Add(new ButtonsData(){name = name, isInteractable = isInteractable});
            }

            foreach (var data in _dataList)
            {
                var button = _buttonList.Find(b => b.name == data.name);
                if (button != null)
                    button.interactable = data.isInteractable;
            }
        }
    }
}