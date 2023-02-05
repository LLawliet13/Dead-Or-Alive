using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnLine : MonoBehaviour
{
    TrailRenderer trailRenderer;
    public Vector3 EndPosition;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        //trailRenderer.startColor = new Color(1, 0, 0, 0.7f);
        //trailRenderer.endColor = new Color(1, 0, 0, 0.7f);
        //Destroy(gameObject, 3f);

    }
    float speed = 3.4f;
    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime * speed);
        if (transform.position.x == EndPosition.x && transform.position.y == EndPosition.y)
            Destroy(gameObject);
        transform.position = EndPosition;

    }
}
