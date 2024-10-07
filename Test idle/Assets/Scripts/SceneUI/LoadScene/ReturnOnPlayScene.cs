using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.UpgradeMenu.Monobehavior
{
    public class ReturnOnPlayScene : MonoBehaviour
    {
        [SerializeField] private Button _loadMainSceneButton;
        [SerializeField] private ButtonManager _buttonManager;

        private void Awake()
        {
            _loadMainSceneButton.onClick.AddListener(LoadMainSceneAction);
        }

        private void LoadMainSceneAction()
        {
            Scene sceneCurrent = SceneManager.GetSceneByName("UpgradeStatsScene");

            Debug.Log("Вызов");
            
            if (sceneCurrent.isLoaded)
            {
                _buttonManager.SaveButtonStates();
                SceneManager.UnloadSceneAsync("UpgradeStatsScene");
            }
            else
            {
                Debug.LogError("Error unload scene");
            }
            
            _loadMainSceneButton.onClick.RemoveAllListeners();
        }
    }
}