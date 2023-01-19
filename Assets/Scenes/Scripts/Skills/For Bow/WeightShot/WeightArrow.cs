using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightArrow : MonoBehaviour
{
    bool fire;
    Vector3 baseScale;
    Vector3 moveVector;
    float timeExist;
    [SerializeField]
    float moveSpeed;
    GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        timeExist = 1;
        fire = false;
        nextTimeToScaleUp = Time.time;
        baseScale = transform.localScale;
    }
    public void Fire(GameObject character)
    {
        this.character = character;
        fire = true;
    }
    float nextTimeToScaleUp;
    // Update is called once per frame
    void Update()
    {
        if(fire == true)
        {
            CalculateMoveVector();
            transform.position += moveVector * moveSpeed * Time.deltaTime;
            Debug.Log("WeightBow Fired");
        }
        else
        {
            //tang kich co bow khi hold
            if(transform.localScale.x / baseScale.x < 5&&Time.time > nextTimeToScaleUp)
            {
                timeExist = timeExist*1.2f;
                transform.localScale = transform.localScale * 1.2f;
                nextTimeToScaleUp = Time.time + 1;
            }
        }
    }
    public void CalculateMoveVector()
    {
        moveVector = (character.transform.Find("Weapon").transform.position - character.transform.position).normalized;
    }
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //mui ten bi pha huy sau 1 khoang thoi gian
        Destroy(gameObject,timeExist);
    }
}
