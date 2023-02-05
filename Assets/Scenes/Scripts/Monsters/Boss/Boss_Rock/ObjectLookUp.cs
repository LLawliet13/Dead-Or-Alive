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
        
    }
    //public GameObject WarnLine;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
    //public LayerMask layerMask;
    public GameObject WarningLine;
    float speed = 4;
    public void DrawLineWarning()
    {
        //Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        //Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 30f, layerMask);
        //if (hit.transform.CompareTag("Player"))
        //{
        //    GameObject warnLine = Instantiate(WarningLine, NewPosition, transform.rotation);
        //    warnLine.GetComponent<WarnLine>().EndPosition = hit.point;
        //}
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector3.Distance(traitPosition , playerPosition) <= 1)
        {
            Action.Invoke();
        }
        else
        {
            traitPosition = Vector3.Lerp(traitPosition, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * speed);
            GameObject warnLine = Instantiate(WarningLine, transform.position, transform.rotation);
            warnLine.GetComponent<WarnLine>().EndPosition = traitPosition;
        }
       

    }
    Vector3 traitPosition;
    public UnityEvent Action;
    void ResetTrait()
    {
        traitPosition = transform.position;
    }
}
