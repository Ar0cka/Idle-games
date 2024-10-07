using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace DefaultNamespace.UpgradeMenu.Monobehavior
{
    public class LoadUpgradeScene : MonoBehaviour
    {
        [SerializeField] private Button _loadNextSceneButton;
        [SerializeField] private string sceneName;
        
        private void Start()
        {
            _loadNextSceneButton.onClick.AddListener(LoadSceneAction);
        }
        
        private void LoadSceneAction()
        {
            LoadSceneAsync(sceneName);
        }
        
        public void LoadSceneAsync(string nameScene)
        {
            StartCoroutine(LoadSceneCoroutine(nameScene));
        }

        private IEnumerator LoadSceneCoroutine(string name)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone)
            {
                Debug.Log($"Loading progress {asyncLoad.progress * 100}");

                yield return null;
            }
        }
    }
}