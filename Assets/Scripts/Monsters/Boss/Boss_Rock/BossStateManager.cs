using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

namespace Assets.Scripts.Monsters.Boss.Boss_Rock
{
    public class BossStateManager : BaseStateManager
    {
        private BaseSkillBoss[] priorityStates;
        private BaseSkillBoss[] parallelStates;
        private UnityEvent<UnityAction> exitParallelState;
        private UnityEvent<UnityAction> exitPriorityState;
        private UnityEvent updatePriorityStates;
        private UnityEvent updateParallelStates;
        private BossStatus bossStatus;
        private void Awake()
        {
            bossStatus = GetComponent<BossStatus>();
            if (bossStatus == null)
                throw new System.Exception("No Boss Status Attached");
        }
        // Start is called before the first frame update
        void Start()
        {
            SignUpState();

            if (updateParallelStates == null)
                updateParallelStates = new UnityEvent();
            if (updatePriorityStates == null)
                updatePriorityStates = new UnityEvent();

            if (exitPriorityState == null)
            {
                exitPriorityState = new UnityEvent<UnityAction>();
                exitPriorityState.AddListener(doExitPriorityState);
            }
            if (exitParallelState == null)
            {
                exitParallelState = new UnityEvent<UnityAction>();
                exitParallelState.AddListener(doExitParallelState);
            }
        }

        // Update is called once per frame
        void Update()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) RemoveAllState();
            else
            {
                CheckAvailableState();
                updatePriorityStates.Invoke();
                updateParallelStates.Invoke();
            }
        }
        protected override void RemoveAllState()
        {
            updatePriorityStates.RemoveAllListeners();
            updateParallelStates.RemoveAllListeners();
            daDangki = 0;
        }
        protected override void SignUpState()
        {
            int[] stateIndexs = bossStatus.BaseStats.state_ids;
            //nhan dien cac state cua quai
            BaseSkillBoss[] allStates = gameObject.GetComponents<BaseSkillBoss>().Where(s =>
            {
                bool match = false;
                foreach (var state in stateIndexs)
                    if (state == s.state_id)
                        return true;
                return match;
            }
            ).ToArray();

            parallelStates = allStates.Where(c => c.AbleToTriggerWithOther).ToArray();
            //sap xep theo thoi gian hoi chieu giam dan
            priorityStates = allStates.Where(c => !c.AbleToTriggerWithOther).OrderByDescending<BaseSkillBoss, float>(c => c.CD_Skill()).ToArray();
        }

        private void doExitParallelState(UnityAction functionName)
        {
            updateParallelStates.RemoveListener(functionName);
        }
        private void doExitPriorityState(UnityAction functionName)
        {
            daDangki--;
            updatePriorityStates.RemoveListener(functionName);
        }
        int daDangki = 0;
        protected override void CheckAvailableState()
        {
            //thuc thi cac state luon co the trigger
            foreach (BaseSkillBoss state in parallelStates)
                if (state.EnterState())
                {
                    //phong truong hop state nay da duoc sign up truoc do va chua ket thuc
                    updateParallelStates.RemoveListener(state.UpdateState);
                    updateParallelStates.AddListener(state.UpdateState);
                    state.DoExitState = exitParallelState;
                }
            //khi khong co state nao dang chay thi tim kiem state kha dung
            if (daDangki == 0)
                foreach (BaseSkillBoss state in priorityStates)
                {
                    if (state.EnterState())
                    {
                        daDangki++;
                        updatePriorityStates.AddListener(state.UpdateState);
                        state.DoExitState = exitPriorityState;
                        break;
                    }
                }
        }
    }
}
