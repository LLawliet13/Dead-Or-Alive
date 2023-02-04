using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearMovement : MonoBehaviour
{
    protected float target_x;
    protected float target_y;
    protected Bounds bounds;
    protected float min_x;
    protected float max_x;
    protected float min_y;
    protected float max_y;

    [SerializeField]
    int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        target_x = gameObject.transform.position.x;
        target_y = gameObject.transform.position.y;
        bounds = ScreenHelper.OrthographicBounds(Camera.main);
        min_x = bounds.min.x;
        max_x = bounds.max.x;
        min_y = bounds.min.y;
        max_y = bounds.max.y;
    }

    protected bool isMoving()
    {
        return gameObject.transform.position.x != target_x || gameObject.transform.position.y != target_y;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("RunSkillSpearManipulation"))
        {
            if (isMoving())
            {
                Vector2 targetPoint = new Vector2();
                if (PlayerPrefs.HasKey("Turnback"))
                {
                    GameObject spearHole = GameObject.FindGameObjectWithTag("SpearHole");
                    targetPoint = new Vector2(spearHole.transform.position.x, spearHole.transform.position.y);
                    if (gameObject.transform.position == spearHole.transform.position)
                    {
                        Destroy(gameObject);
                        PlayerPrefs.DeleteKey("Turnback");
                    }
                }
                else
                {
                    targetPoint = new Vector2(target_x, target_y);
                }
                Vector2 direction = targetPoint - (Vector2)gameObject.transform.position;
                direction = direction.normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, speed * Time.deltaTime);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPoint, speed * Time.deltaTime);
            }
            else
            {
                target_x = Random.Range(min_x, max_x);
                target_y = Random.Range(min_y, max_y);
            }
        }
        
    }
}
