using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.lockedroom.Games.Bomberman2 {
    public class EnemyBehavior : MonoBehaviour {
        /// <summary>
        /// Khởi tạo biến public rigidbody
        /// {get; private set;} nghĩa là các thành phần khác ngoài lớp không thể gán trực tiếp giá trị cho rigidbody
        /// mà chỉ có thể đọc giá trị hiện tại của nó.
        /// </summary>
        public new Rigidbody2D rigidbody { get; private set; }
        /// <summary>
        /// Hướng mặc định cho di chuyển là xuống
        /// </summary>
        private Vector2 direction = Vector2.down;
        /// <summary>
        /// Khởi tạo tốc độ di chuyển mặc định là 2f
        /// </summary>
        public float speed = 2f;
        public SpriteRenderer spriteRenderer;
        /// <summary>
        /// Nhận component
        /// </summary>
        private void Awake() {
            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        /// <summary>
        /// Update vận tốc di chuyển của enemy
        /// </summary>
        private void Update() {
            rigidbody.velocity = direction * speed;
        }
        /// <summary>
        /// So sánh nếu gặp tường thì đổi hướng ngẫu nhiên
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.CompareTag("Wall")) {
                ChangeDirectionRandomly();
            }
        }
        /// <summary>
        /// Hàm dùng để đổi hướng ngẫu nhiên
        /// </summary>
        private void ChangeDirectionRandomly() {
            Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            int randomIndex = Random.Range(0, possibleDirections.Length);
            direction = possibleDirections[randomIndex];
        }
    }
}
