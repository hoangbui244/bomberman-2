using UnityEngine;

public class MovementController : MonoBehaviour
{
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
    /// Hàm Awake sử dụng để khởi tạo các thành phần của đối tượng trước khi bất kỳ phương thức nào khác của đối tượng được gọi.
    /// </summary>
    private void Awake()
    {
        /// <summary>
        /// Tìm kiếm GameObject mà đoạn script này chạy, tìm theo loại được nhập trong ngoặc của GetComponent
        /// </summary>
        rigidbody = GetComponent<Rigidbody2D>();        
    }
    /// <summary>
    /// Hàm Update được gọi mỗi khung hình của scene
    /// </summary>
    private void Update()
    {
        /// <summary>
        /// Kiểm tra mỗi khung hình có phím nào được nhấn
        /// </summary>
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right);
        }
        else
        {
            SetDirection(Vector2.zero);
        }
    }
    /// <summary>
    /// Hàm FixedUpdate xử lý các hoạt động vật lý và chuyển động của các đối tượng trong trò chơi mỗi khung hình
    /// để đảm bảo tính đồng bộ và độ chính xác của các hoạt động.
    /// </summary>
    private void FixedUpdate()
    {
        /// <summary>
        /// Vị trí sau mỗi khung hình là vị trí của rigidbody
        /// Sự chuyển đổi = hướng di chuyển * tốc độ * tgian
        /// Nếu ở hàm Update thì dùng DeltaTime còn FixedUpdate thì dùng fixedDeltaTime
        /// </summary>
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        /// <summary>
        /// Vị trí rigidbody = vị trí nhận được ở trên + độ chuyển dịch
        /// </summary>
        rigidbody.MovePosition(position + translation);
    }
    /// <summary>
    /// Hàm SetDirection để update direction mới
    /// </summary>
    private void SetDirection(Vector2 newDirection)
    {
        /// <summary>
        /// Gán newDirection
        /// </summary>
        direction = newDirection;
    }
}
