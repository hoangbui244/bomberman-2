using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Bomberman2 {
    public class BombController : MonoBehaviour {
        /// <summary>
        /// Khởi tạo GameObject bombPrefab
        /// </summary>
        public GameObject bombPrefab;
        /// <summary>
        /// Khởi tạo nút đặt bomb
        /// </summary>
        public KeyCode inputKey = KeyCode.Space;
        /// <summary>
        /// Thời gian bom nổ
        /// </summary>
        public float bombFuseTime = 3f;
        /// <summary>
        /// Số lượng bomb player có thể đặt
        /// </summary>
        public int bombAmount = 1;
        /// <summary>
        /// Số lượng bomb còn lại
        /// </summary>
        private int bombsRemaining;
        /// <summary>
        /// Hàm OnEnable để kích hoạt số bomb còn lại bằng số lượng bomb
        /// </summary>
        private void OnEnable() {
            bombsRemaining = bombAmount;
        }
        /// <summary>
        /// Hàm Update để update khi player ấn nút đặt bomb
        /// </summary>
        private void Update() { 
            // Kiểm tra xem số lượng bomb còn lại > 0 và gọi hàm PlaceBomb
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
                StartCoroutine(PlaceBomb());
            }
        }
        /// <summary>
        /// Hàm PlaceBomb để xử lý đặt bomb
        /// </summary>
        private IEnumerator PlaceBomb() {
            Vector2 position = transform.position;
            // Đặt bomb sẽ vừa với ô vuông grid
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            // Gán bomb = Khởi tạo bombPrefab
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombsRemaining--;
            // Đợi thời gian để bomb nổ xong (3s)
            yield return new WaitForSeconds(bombFuseTime);
            // Xóa bomb sau khi nổ
            Destroy(bomb);
            // Trả lại bombsRemaining
            bombsRemaining++;
        }
        /// <summary>
        /// Sau khi đặt bomb và ra khỏi quả bomb thì có thể tác động physic
        /// </summary>
        private void OnTriggerExit2D(Collider2D other) {
            // Ktra điều kiện
            if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
                other.isTrigger = false;
            }
        }
    }
}