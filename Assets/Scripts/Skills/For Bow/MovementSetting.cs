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
    public static Vector3 CalculateStraightMoveVector(GameObject MovingObject, Vector3 end)
    {
        Vector3 moveVector = (end - MovingObject.transform.position).normalized;
        float curAngle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(curAngle, Vector3.forward);
        MovingObject.transform.rotation = q;
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
    public static Vector3 CalculateCircleMoveVector(Vector3 center,Vector3 point,float speedAngle,float speedRadiusChange,float radiusLimit)
    {
        Vector3 diff = point - center;
        float curAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float nextAngle = curAngle + speedAngle ; 
        float nextRadius = Mathf.Lerp(diff.magnitude, radiusLimit, speedRadiusChange);
        return new Vector3(center.x + nextRadius * Mathf.Cos(nextAngle * Mathf.Deg2Rad), center.y + nextRadius * Mathf.Sin(nextAngle * Mathf.Deg2Rad));
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
        return (childRotation.transform.rotation * new Vector3(1, 0, 0)).normalized;
    }




}
