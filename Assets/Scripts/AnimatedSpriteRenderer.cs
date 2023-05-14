using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    /// <summary>
    /// Khai báo
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// idleSprites
    /// </summary>
    public Sprite idleSprites;
    /// <summary>
    /// array of Sprites
    /// </summary>
    public Sprite[] animationSprites;
    /// <summary>
    /// có thể thay đổi giá trị
    /// </summary>
    public float animationTime = 0.25f;
    /// <summary>
    /// giá trị để check xem đang là frame nào chạy
    /// </summary>
    private int animationFrame;
    /// <summary>
    /// check xem should it loop over and over again
    /// </summary>
    public bool loop = true;
    /// <summary>
    /// switch to the idle
    /// </summary>
    public bool idle = true;
    /// <summary>
    /// Hàm Awake
    /// GetComponent của SpriteRenderer
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Điều khiển hiển thị sprite
    /// </summary>
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    /// <summary>
    /// Hàm Start
    /// </summary>
    private void Start()
    {
        /// <summary>
        /// Điều khiển hiển thị sprite
        /// 0.25f default => 1s 4 khung hình
        /// </summary>
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    private void NextFrame()
    {

    }
}
