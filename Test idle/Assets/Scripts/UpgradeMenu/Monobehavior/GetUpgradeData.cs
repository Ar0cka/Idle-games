using System;
using DefaultNamespace.UpgradeMenu.Event;
using Leopotam.Ecs;
using Scriptable_object.UpgradeData;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UpgradeMenu.Monobehavior
{

    public class GetUpgradeData : MonoBehaviour
    {
        
        [SerializeField] private UpgradeFloatData upgradeData;
        private bool buttonUsed = false;
        

        public void GetData()
        {
            if (ECSStartupOnUpgradeScene.world != null)
            {
                if (!buttonUsed)
                {
                    var entity = ECSStartupOnUpgradeScene.world.NewEntity();
                    entity.Get<UpStatsPlayerEvent>().upgradeEmpty = upgradeData;
                    
                    GetComponent<Button>().interactable = false;
                    
                    buttonUsed = true;
                }
            }
            else
            {
                Debug.LogError($"ecs dont find");
            }
        }
    }
}