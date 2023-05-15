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
        /// Prefab
        /// </summary>
        public BombExplosion explosionPrefab;
        /// <summary>
        /// Thời gian hiệu ứng nổ tồn tại
        /// </summary>
        public float explosionDuration = 1f;
        /// <summary>
        /// Phạm vi bom nổ
        /// Khi nhặt PowerUp thì phạm vi tăng
        /// </summary>
        public int explosionRadius = 1;
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
            // Reset lại vị trí quả bomb sau khi tác động physic để kèm hiệu ứng nổ tại vị trí mới
            position = bomb.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            BombExplosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            // Xóa hiệu ứng nổ
            explosion.DestroyAfter(explosionDuration);
            // Gọi hàm Explode
            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);
            // Xóa bomb sau khi nổ
            Destroy(bomb);
            // Trả lại bombsRemaining
            bombsRemaining++;
        }
        /// <summary>
        /// Hàm Explode để xử lý phạm vi hiệu ứng bom nổ trên nhiều tiles
        /// </summary>
        private void Explode(Vector2 position, Vector2 direction, int length) {
            // Ktra nếu chiều dài <= 0  thì dừng
            if (length <= 0) {
                return;
            }
            position += direction;
            BombExplosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            // Xét điều kiện
            explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
            explosion.SetDirection(direction);
            explosion.DestroyAfter(explosionDuration);
            // Gọi hàm Explode với position mới
            Explode(position, direction, length - 1);
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