using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CreepBaseState : MonoBehaviour
{
    //dinh danh state khi chon state cho creep type
    public int state_id;
    //Luon trigger khi dieu kien enter dien ra
    public bool AbleToTriggerWithOther;
    //xu ly truong hop nhieu state co kha nang trigger trong cung 1 thoi diem, se chon theo do uu tien
    public int priority;
    
    protected EnemyStatus enemyStatus;
    //ckeck dieu kien xem state nay kich hoat duoc khong
    public abstract bool EnterState();

    //thuc thi hanh dong cua state
    public abstract void UpdateState();
    //thong bao exitState
    public UnityEvent DoExitState;
    public abstract void ExitState();

    public Transform player;
    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        if (enemyStatus == null)
            throw new System.Exception("No Enemy Status Attached");
    }
}
