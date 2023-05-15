using io.lockedroom.Games.Bomberman2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {
    /// <summary>
    /// Danh sách loại vật phẩm
    /// </summary>
    public enum ItemType {
        BlastRadius,
        ExtraBomb,
        SpeedUp,
    }
    public ItemType type;
    /// <summary>
    /// Hàm để nhặt vật phẩm
    /// </summary>
    private void OnItemPickup(GameObject player) {
        switch (type) {
            case ItemType.BlastRadius:
                //..
                break;
            case ItemType.ExtraBomb:
                //..
                break;
            case ItemType.SpeedUp:
                player.GetComponent<MovementController>().speed++;
                break;
        }
        Destroy(gameObject);
    }
    /// <summary>
    /// Hàm trigger khi tác động vào 1 item
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        // So sánh nếu vật có tag Player 
        if (other.CompareTag("Player")) {
            OnItemPickup(other.gameObject);
        }
    }
}
