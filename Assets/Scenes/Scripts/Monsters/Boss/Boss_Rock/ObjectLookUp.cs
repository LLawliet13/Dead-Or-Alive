using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//thanh phan gan vao doi tuong co kha nang lao toi hoac ban gi do toi nhan vat ma phai canh bao
public class ObjectLookUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        lookAtGameObject = null;
    }
    //public GameObject WarnLine;
    // Update is called once per frame
    [HideInInspector]
    public GameObject lookAtGameObject;
    void Update()
    {
        if (lookAtGameObject == null) lookAtGameObject = GameObject.FindGameObjectWithTag("Player");
        else
            transform.LookAt(lookAtGameObject.transform);
    }
    //public LayerMask layerMask;
    public GameObject WarningLine;
    float speed = 8;
    public void DrawLineWarning(Vector3 t)
    {
        //Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        //Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 30f, layerMask);
        //if (hit.transform.CompareTag("Player"))
        //{
        //    GameObject warnLine = Instantiate(WarningLine, NewPosition, transform.rotation);
        //    warnLine.GetComponent<WarnLine>().EndPosition = hit.point;
        //}
        Vector3 target = t;
        target = (target - transform.position) * 7 / 5 + transform.position;
        if (Vector3.Distance(traitPosition, target) <= 1)
        {
            Action.Invoke(target);
            ResetTrait();
        }
        else
        {
            traitPosition = Vector3.Lerp(traitPosition, target, Time.deltaTime * speed);
            GameObject warnLine = Instantiate(WarningLine, transform.position, transform.rotation);
            warnLine.GetComponent<WarnLine>().EndPosition = traitPosition;
        }


    }
    Vector3 traitPosition;
    public UnityEvent<Vector3> Action;
    void ResetTrait()
    {
        traitPosition = transform.position;
    }
}
