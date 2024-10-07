using DefaultNamespace.UpgradeMenu.Systems;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    public class ECSStartupOnUpgradeScene : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        public static EcsWorld world { get; private set; }
        private EcsSystems systems;

        private void Start()
        {
            Debug.Log("Initialize EcsWorld");
            
            world = new EcsWorld();
            
            if (world != null)
            Debug.Log($"Ecs world = {world}");
            
            systems = new EcsSystems(world);
            
            systems.ConvertScene();
            
            Inject();
            AddNewSystem();
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void AddNewSystem()
        {
            systems.Add(new UpgradeHPPlayer());
        }

        private void Inject()
        {
            systems.Inject(_playerSettings);
        }
        
        private void AddNewOneFrame()
        {
            
        }
        
        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}