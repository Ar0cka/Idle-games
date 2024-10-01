using BattlePhase.Systems.BattleSystems.PlayerAttackSystem;
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.System;
using DefaultNamespace.Battle.System.BattleSystem.BlockSystems;
using DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.ControlPhase.System.ControlEnemyHP;
using DefaultNamespace.ControlPhase.System.DestroyButton;
using DefaultNamespace.MonsterSpawn.Events;
using DefaultNamespace.Player.System;
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
        private EcsWorld world;
        private EcsSystems systems;

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            
            systems.ConvertScene();
            
            AddBattlePhaseInjections();
            AddBattlePhaseSystems();
            AddBattlePhaseOneFrames();
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        #region BattleRegion

        private void AddBattlePhaseInjections()
        {
        
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
            
            // Системы, связанные с битвой
            systems.Add(new PlayerAttackEnemySystem());
            systems.Add(new EnemyAttackSystem());
            systems.Add(new EnemyTakeDamageSystem());
            systems.Add(new SerializeMonsterDataSystem());
            
            //Уничтножение монстра
            systems.Add(new DestroyMonsterButtonSystem());
            systems.Add(new DestroyMonster());
            systems.Add(new RespawnMonsterSystem());
            systems.Add(new BLockSpawnSystem());
            
            // Системы, связанные со спавном монстра
            systems.Add(new TakeMonsterFromListSystem());
            systems.Add(new SpawnMonsterSystem());
            systems.Add(new SendSerializeEventSystem());
            
            systems.Add(new OnHpBarEnemySystem());
            systems.Add(new HideHpBarEnemySystem());
            
            // Системы которые относятся к battle phase
            systems.Add(new PlayerTakeDamageSystem());
            systems.Add(new PlayerBlockAttackSystem());
            systems.Add(new EnemyBlockSystem());
            systems.Add(new MonsterUpdateHpBarSystem());
            systems.Add(new PlayerUpdateHpBarSystem());
            
            // Управление концом битвы
            systems.Add(new RunFromBattleSystem());
            systems.Add(new EndFightControlSystem());

            systems.Add(new HealHeroButtonSystem());
            systems.Add(new HideHealButtonSystem());
            systems.Add(new CooldownHealButtonSystem());
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