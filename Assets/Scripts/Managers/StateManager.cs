using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{

    CreepBaseState[] PriorityStates;
    CreepBaseState[] AbleToTriggerWithOtherStates;
    private bool IsPriorityStateFree;
    private UnityEvent ExistState;
    // Start is called before the first frame update
    void Start()
    {
        IsPriorityStateFree = true;
        SignUpState();
        if (ExistState == null)
        {
            ExistState = new UnityEvent();
            ExistState.AddListener(doExitState);
        }

    }
    private EnemyStatus enemyStatus;
    private void Awake()
    {
        enemyStatus = GetComponent<CreepStatus>();
        if (enemyStatus == null)
            throw new System.Exception("No Enemy Status Attached");
    }
    private void SignUpState()
    {
        int[] stateIndexs = enemyStatus.BaseStats.state_ids;
        //nhan dien cac state cua quai
        CreepBaseState[] allStates = gameObject.GetComponents<CreepBaseState>().Where(s =>
        {
            bool match = false;
            foreach (var state in stateIndexs)
                if (state == s.state_id)
                    return true;
            return match;
        }
        ).ToArray();

        AbleToTriggerWithOtherStates = allStates.Where(c => c.AbleToTriggerWithOther).ToArray();
        //sap xep theo do uu tien
        PriorityStates = allStates.Where(c => !c.AbleToTriggerWithOther).OrderBy<CreepBaseState, int>(c => c.priority).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAvailableState();
    }

    private void doExitState()
    {
        IsPriorityStateFree = true;
    }
    private void CheckAvailableState()
    {
        //thuc thi cac state luon co the trigger
        foreach (CreepBaseState state in AbleToTriggerWithOtherStates)
            if (state.EnterState())
            {
                state.UpdateState();
            }

        if (IsPriorityStateFree)
            foreach (CreepBaseState state in PriorityStates)
            {
                if (state.EnterState())
                {
                    state.UpdateState();
                    state.DoExitState = ExistState;
                    IsPriorityStateFree = false;
                    break;
                }
            }
    }
}
