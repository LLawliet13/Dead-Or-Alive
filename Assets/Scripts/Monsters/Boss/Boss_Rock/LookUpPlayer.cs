using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monsters.Boss.Boss_Rock
{
    internal class LookUpPlayer : BaseSkillBoss
    {
        private void Start()
        {
            firstTimeUse = Time.time;
        }
        public override bool AbleToTrigger()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return false;
            return true;
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player.transform.position.x > transform.position.x && isLeft)
            {
                Flip();
            }

            if (player.transform.position.x < transform.position.x && !isLeft)
            {
                Flip();
            }
        }
        bool isLeft = true;
        private void Flip()
        {
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.Find("Head").transform.localScale;
            theScale.x *= -1;
            transform.Find("Head").transform.localScale = theScale;
            isLeft = !isLeft;
        }
        protected override void SetAtkSkill()
        {
            // khong co tac dong toi atkSkill
        }
    }
}
