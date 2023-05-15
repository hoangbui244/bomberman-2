using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    /// <summary>
    /// Thời gian để vật biến mất hoàn toàn
    /// </summary>
    public float destructionTime = 1f;
    /// <summary>
    /// Hàm Start
    /// </summary>
    private void Start() {
        Destroy(gameObject, destructionTime);
    }
}
