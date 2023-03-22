using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monsters.Boss.Boss_Rock
{
    internal class BeingAttacked : BaseSkillBoss
    {
        private void Start()
        {
            firstTimeUse = Time.time;
            previousHp = bossStatus.CurrentHp;
        }
        private int previousHp;
        public override bool AbleToTrigger()
        {
            if (bossStatus.CurrentHp < previousHp)
            {
                UpdateSkillBaseOnCharacterLv();
                previousHp = bossStatus.CurrentHp;
                return true;
            }
            return false;
        }

        
        public override float CD_Skill()
        {
            return 0;
        }

        public override int LVToUse()
        {
            return 0;
        }

        public override bool RangeSkill(Vector3 position)
        {
            return true;
        }

        public override void UpdateSkillBaseOnCharacterLv()
        {
            
        }

        public override void UpdateState()
        {
            GetComponent<EnemyStatus>().beingAttackedEffect();
            ExitState();
        }
       
        protected override void SetAtkSkill()
        {
            // khong co tac dong toi atkSkill
        }
    }
}
