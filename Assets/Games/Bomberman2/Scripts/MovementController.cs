﻿using UnityEngine;
namespace io.lockedroom.Games.Bomberman2 {
    public class MovementController : MonoBehaviour {
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
        /// Khởi tạo tốc độ di chuyển mặc định là 5f
        /// </summary>
        public float speed = 5f;
        /// <summary>
        /// Nhập các input điều khiển nhân vật di chuyển
        /// </summary>
        public KeyCode inputUp = KeyCode.W;
        public KeyCode inputDown = KeyCode.S;
        public KeyCode inputLeft = KeyCode.A;
        public KeyCode inputRight = KeyCode.D;
        /// <summary>
        /// SpritesRenderer các hướng
        /// </summary>
        public AnimatedSpriteRenderer spriteRendererUp;
        public AnimatedSpriteRenderer spriteRendererDown;
        public AnimatedSpriteRenderer spriteRendererLeft;
        public AnimatedSpriteRenderer spriteRendererRight;
        public AnimatedSpriteRenderer spriteRendererDeath;
        /// <summary>
        /// Dùng để ktra xem hiện tại Sprite nào đang được kích hoạt
        /// </summary>
        private AnimatedSpriteRenderer activeSpritesRenderer;
        /// <summary>
        /// Hàm Awake sử dụng để khởi tạo các thành phần của đối tượng trước khi bất kỳ phương thức nào khác của đối tượng được gọi.
        /// </summary>
        private void Awake() {
            /// Tìm kiếm GameObject mà đoạn script này chạy, tìm theo loại được nhập trong ngoặc của GetComponent
            rigidbody = GetComponent<Rigidbody2D>();
            /// GetComponent cho Sprite được kích hoạt hiện tại (mặc định là xuống)
            activeSpritesRenderer = spriteRendererDown;
        }
        /// <summary>
        /// Hàm Update được gọi mỗi khung hình của scene
        /// </summary>
        private void Update() {
            /// Kiểm tra mỗi khung hình có phím nào được nhấn và kích hoạt sprite của hướng đó
            if (Input.GetKey(inputUp)) {
                SetDirection(Vector2.up, spriteRendererUp);
            }
            else if (Input.GetKey(inputDown)) {
                SetDirection(Vector2.down, spriteRendererDown);
            }
            else if (Input.GetKey(inputLeft)) {
                SetDirection(Vector2.left, spriteRendererLeft);
            }
            else if (Input.GetKey(inputRight)) {
                SetDirection(Vector2.right, spriteRendererRight);
            }
            else {
                SetDirection(Vector2.zero, activeSpritesRenderer);
            }
        }
        /// <summary>
        /// Hàm FixedUpdate xử lý các hoạt động vật lý và chuyển động của các đối tượng trong trò chơi mỗi khung hình
        /// để đảm bảo tính đồng bộ và độ chính xác của các hoạt động.
        /// </summary>
        private void FixedUpdate() {
            /// <summary>
            /// Vị trí sau mỗi khung hình là vị trí của rigidbody
            /// Sự chuyển đổi = hướng di chuyển * tốc độ * tgian
            /// Nếu ở hàm Update thì dùng DeltaTime còn FixedUpdate thì dùng fixedDeltaTime
            /// </summary>
            Vector2 position = rigidbody.position;
            Vector2 translation = direction * speed * Time.fixedDeltaTime;
            /// Vị trí rigidbody = vị trí nhận được ở trên + độ chuyển dịch
            rigidbody.MovePosition(position + translation);
        }
        /// <summary>
        /// Hàm SetDirection để update direction mới
        /// </summary>
        private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer) {
            direction = newDirection;
            /// Enable render các sprites theo các hướng nếu thỏa mãn điều kiện
            /// Enable 1 hướng và disable các hướng còn lại
            spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
            spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
            spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
            spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;
            /// nếu sprite hiện tại là idle thì hướng đứng yên
            activeSpritesRenderer = spriteRenderer;
            activeSpritesRenderer.idle = direction == Vector2.zero;
        }
        /// <summary>
        /// Nếu  người chơi chạm vào Enemy thì thua
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Enemy")) {
                DeathSequence();
            }
        }
        /// <summary>
        /// Nếu ng chơi đi vào bom nổ thì die
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other) {
            // Ktra điều kiện với layer Explosion
            if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
                DeathSequence();
            }
        }
        /// <summary>
        /// Khi người chơi thua
        /// </summary>
        private void DeathSequence() {
            // Khi người chơi thua thì disable hành động, hoạt ảnh,...
            enabled = false;
            GetComponent<BombController>().enabled = false;
            spriteRendererDown.enabled= false;
            spriteRendererUp.enabled= false;
            spriteRendererLeft.enabled= false;
            spriteRendererRight.enabled= false;
            spriteRendererDeath.enabled= true;
            Invoke(nameof(OnDeathSequenceEnded), 1.25f);
        }
        /// <summary>
        /// Khi người chơi thua
        /// </summary>
        private void OnDeathSequenceEnded() {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().CheckWinState();
        }
    }
}