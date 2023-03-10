using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public interface MovementSetting 
{
    //move = + vector
    public static Vector3 CalculateMoveVector(Vector3 start, Vector3 end)
    {
        Vector3 moveVector = (end - start).normalized;
        return moveVector;
    }
    //thay doi quay dao di chuyen cua object tu tu
    public static Vector3 CalculateSlopeMoveVector(Vector3 end,GameObject MovingObject,float speedRotation)
    {
        //float speedRotation = 6;

        Vector3 diff = end - MovingObject.transform.position;
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg ;
        Quaternion q = Quaternion.AngleAxis(curAngle, Vector3.forward);
        float singleStep = speedRotation * Time.deltaTime;
        MovingObject.transform.rotation = Quaternion.Slerp(MovingObject.transform.rotation, q, singleStep);
        return (MovingObject.transform.rotation*new Vector3(1,0,0)).normalized;
    }

    public static Vector3 CalculateSlopeMoveVectorWithoutChangeRotation(Vector3 end,GameObject childRotation, GameObject MovingObject, float speedRotation)
    {
        //float speedRotation = 6;

        Vector3 diff = end - MovingObject.transform.position;
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(curAngle, Vector3.forward);
        //Quaternion localRotation = Quaternion.Inverse(MovingObject.transform.rotation) * q;
        float singleStep = speedRotation * Time.deltaTime;
        //Vector3 globalRotation = childRotation.transform.TransformDirection(childTransform.rotation.eulerAngles);
        childRotation.transform.rotation = Quaternion.Slerp(childRotation.transform.rotation, q, singleStep);
        //Debug.Log(childRotation.transform.rotation+" "+ childRotation.transform.rotation * new Vector3(1, 0, 0));
        return (childRotation.transform.rotation * new Vector3(1, 0, 0)).normalized;
    }




}
