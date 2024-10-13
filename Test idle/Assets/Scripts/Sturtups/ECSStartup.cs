using System.Collections.Generic;
using BattlePhase.Components.AttackHero;
using BattlePhase.Systems.BattleSystems.PlayerAttackSystem;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.System;
using DefaultNamespace.Battle.System.BattleSystem.BlockSystems;
using DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem;
using DefaultNamespace.Components;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.ControlPhase.System.ControlEnemyHP;
using DefaultNamespace.MonsterSpawn.Events;
using DefaultNamespace.Player.System;
using DefaultNamespace.SceneUI.Menu.Systems;
using Inventory;
using Inventory.Systems;
using Leopotam.Ecs;
using MonsterSpawn.Systems;
using MonsterSpawn.Systems.CheckStateSystem;
using MonsterSpawn.Systems.DestroyMonster;
using MonsterSpawn.Systems.RespawnSystems;
using UnityEngine;
using Voody.UniLeo;

namespace DefaultNamespace
{
    public class ECSStartup : MonoBehaviour
    {
        [SerializeField] private InventorySettings inventorySettings;
        [SerializeField] private InventoryEquip _inventoryEquip;
        [SerializeField] private PlayerSettings _playerSettings;
        private PlayerData _playerData;
        
        private EcsWorld world;
        private EcsSystems systems;

        
        
        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            
            systems.ConvertScene();

            #region injections

            AddBattlePhaseInjections();

            #endregion

            #region systems

            AddBattlePhaseSystems();

            #endregion

            #region OneFrames

            AddBattlePhaseOneFrames();

            #endregion
           
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        #region BattleRegion

        private void AddBattlePhaseInjections()
        {
            _playerData = new PlayerData(_playerSettings);
            systems.Inject(_playerData);
            systems.Inject(inventorySettings);
            systems.Inject(_inventoryEquip);
        }
    
        private void AddBattlePhaseSystems()
        {  
            systems.Add(new PlayerInitSystem());
            systems.Add(new SerializeCheckStateSystem());
    
            // Фазы битвы и интерфейс управления
            systems.Add(new BeginBattleSystem());
            systems.Add(new BeginFightControlUISystem());
            systems.Add(new OnHealButtonSystem());
            systems.Add(new SerializeAttackCooldownSystem());
            
            // Управление концом битвы
            systems.Add(new RunFromBattleSystem());
            systems.Add(new EndFightControlSystem());
            
            // Системы, связанные с битвой
            systems.Add(new AttackHero());
            systems.Add(new PlayerAttackEnemySystem());
            systems.Add(new EnemyAttackSystem());
            systems.Add(new EnemyTakeDamageSystem());
            systems.Add(new SerializeMonsterDataSystem());
            
            //Уничтножение монстра
            systems.Add(new DestroyMonsterOfTheRun());
            systems.Add(new DestroyMonster());
            systems.Add(new RespawnMonsterSystem());
            systems.Add(new BLockSpawnSystem());
            
            // Системы, связанные со спавном монстра
            systems.Add(new TakeMonsterFromListSystem());
            systems.Add(new SpawnMonsterSystem());
            systems.Add(new SendSerializeEventSystem());
            
            systems.Add(new OnHpBarEnemySystem());
            systems.Add(new HideHpBarEnemySystem());
            
            SerializeMenu(); // системы для сериализации меню игрока
            
            //Системы для работы с предметами в инвентаре
            AddNewControlInventorySystems();
            
            // Системы которые относятся к battle phase
            systems.Add(new PlayerTakeDamageSystem());
            systems.Add(new PlayerBlockAttackSystem());
            systems.Add(new EnemyBlockSystem());
            systems.Add(new MonsterUpdateHpBarSystem());
            systems.Add(new PlayerUpdateHpBarSystem());
            
            //HealButton
            systems.Add(new HealHeroButtonSystem());
            systems.Add(new HideHealButtonSystem());
            systems.Add(new CooldownHealButtonSystem());
        }

        private void AddNewControlInventorySystems()
        {
            systems.Add(new ControlSlotStates());
            systems.Add(new CheckTypeItemSystem());
            systems.Add(new TakeActionBuffItem());
            systems.Add(new TakeActionEquipItem());
            systems.Add(new HealFromPotionSystems());
            systems.Add(new ArmourTakeSystem());
            systems.Add(new ControlColectedStateItemsFromInventory());
            systems.Add(new ControlBaseStateItems());
            systems.Add(new UpdateMenuUISystem());
        }

        private void SerializeMenu()
        {
           
            systems.Add(new OpenMenuOnBattleScene());
            systems.Add(new CloseMenuSystem());
        }
        
        private void AddBattlePhaseOneFrames()
        {
            // Ивенты связанные с кнопками
            systems.OneFrame<HideBeginBattleUIEvent>();
            systems.OneFrame<HideRunFromBattleUIEvent>();
            systems.OneFrame<HideHealButtonEvent>();
            systems.OneFrame<OnButtonHealEvent>();
            
            // Ивенты связанные с атакой игрока и монстра
            systems.OneFrame<PlayerAttackEvent>();
            systems.OneFrame<MonsterAttackEvent>();
            systems.OneFrame<SerializeAttackCooldownEvent>();
            
            // Ивенты связанные с UI
            systems.OneFrame<UpdatePlayerUIEvent>();
            systems.OneFrame<UpdateMonsterUIEvent>();
            systems.OneFrame<HideHpBarEnemyEvent>();
            systems.OneFrame<OnHpBarEnemyEvent>();
            
            //Ивенты связанные со спавном и уничтожением монстра
            systems.OneFrame<ChoiceMonsterFromListEvent>();
            systems.OneFrame<DestroyEnemyEvent>();
            systems.OneFrame<DestroyMonsterOfTheRunFromBattleEvent>();
            systems.OneFrame<RespawnMonsterEvent>();
        }
        #endregion
       
        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}