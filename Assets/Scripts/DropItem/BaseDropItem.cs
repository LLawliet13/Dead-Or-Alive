using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseDropItem : MonoBehaviour
{
    /// <summary>
    /// ham trien khai khi nguoi choi cham vao vat pham
    /// </summary>
    protected abstract void Action();
    /// <summary>
    /// gia tri ma vat pham dem lai cho nguoi choi
    /// </summary>
    public int value;
    public UnityEvent<BaseDropItem> DestroyEvent;
    public Vector3 DropPlace;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Action();
        }
    }
}
