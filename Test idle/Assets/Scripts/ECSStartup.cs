using BattlePhase.Systems.BattleSystems.PlayerAttackSystem;
using DefaultNamespace.Battle.Components.BattleComponents;
using DefaultNamespace.Battle.Components.Events;
using DefaultNamespace.Battle.Components.Events.AttackEvents;
using DefaultNamespace.Battle.Components.Events.BlockAttackEvents;
using DefaultNamespace.Battle.System;
using DefaultNamespace.Battle.System.BattleSystem.BlockSystems;
using DefaultNamespace.Battle.System.BattleSystems.MonstersAttackSystem;
using DefaultNamespace.BattlePhase.Components.Events.SpawnEvents;
using DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems;
using DefaultNamespace.BattlePhase.Systems.BattleSystems.SpawnSystems.RespawnSystems;
using DefaultNamespace.ControlPhase.Components.Events;
using DefaultNamespace.Player.System;
using Leopotam.Ecs;
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
            systems.Add(new SerializeCheckStateMonster());
    
            // Фазы битвы и интерфейс управления
            systems.Add(new BeginBattleSystem());
            systems.Add(new BeginFightControlUISystem());
            systems.Add(new OnHealButtonSystem());
            systems.Add(new SerializeAttackCooldownSystem());
            
            // Системы, связанные с битвой
            systems.Add(new PlayerAttackEnemySystem());
            systems.Add(new EnemyAttackSystem());
            systems.Add(new EnemyTakeDamageSystem());
            
            // Системы, связанные со спавном монстра
            systems.Add(new MonsterSerializeSystem());
            systems.Add(new RespawnSystem());
            systems.Add(new TakeMonsterSystem());
            systems.Add(new OnHpBarEnemySystem());
            systems.Add(new LoadingMonsterSystem());
            systems.Add(new SpawnMonsterSystem());
            
            // Системы которые относятся к battle phase
            systems.Add(new PlayerTakeDamageSystem());
            systems.Add(new PlayerBlockAttackSystem());
            systems.Add(new EnemyBlockSystem());
            systems.Add(new MonsterUpdateHpBarSystem());
            systems.Add(new PlayerUpdateHpBarSystem());
            
            // Управление концом битвы
            systems.Add(new RunFromBattleSystem());
            systems.Add(new EndFightControlSystem());
            systems.Add(new DestroyMonsterSystem());
            systems.Add(new HideHpBarEnemySystem());

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
            systems.OneFrame<UpdateUIEvent>();
            systems.OneFrame<HideHpBarEnemyEvent>();
            systems.OneFrame<OnHpBarEnemyEvent>();
            
            //Ивенты связанные со спавном и уничтожением монстра
            systems.OneFrame<ChoiceMonsterEvent>();
            systems.OneFrame<LoadingMonsterEvent>();
            systems.OneFrame<SpawnMonsterEvent>();
            systems.OneFrame<LoadDataMonsterEvent>();
            systems.OneFrame<DestroyMonsterEvent>();
            systems.OneFrame<RespawnEvent>();
        }
        #endregion
       
        private void OnDestroy()
        {
            systems.Destroy();
            world.Destroy();
        }
    }
}