using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        //nhan dien cac state cua quai
        CreepBaseState[] allStates = gameObject.GetComponents<CreepBaseState>();
        AbleToTriggerWithOtherStates = allStates.Where(c=>c.AbleToTriggerWithOther).ToArray();
        //sap xep theo do uu tien
        PriorityStates = allStates.Where(c => !c.AbleToTriggerWithOther).OrderBy<CreepBaseState,int>(c=>c.priority).ToArray();

        if (ExistState == null) { 
            ExistState = new UnityEvent();
            ExistState.AddListener(doExitState);
        }
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
            if(state.EnterState())
            {
                state.UpdateState();
            }

        if(IsPriorityStateFree)
        foreach(CreepBaseState state in PriorityStates)
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
