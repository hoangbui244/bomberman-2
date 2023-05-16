using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
namespace io.lockedroom.Games.Bomberman2 {
    public class GameManager : MonoBehaviour {
        /// <summary>
        /// GameObject người chơi
        /// </summary>
        public GameObject[] players;
        /// <summary>
        /// Hàm ktra điều kiện thắng
        /// </summary>
        public void CheckWinState() {
            int aliveCount = 0;
            foreach (GameObject player in players) {
                if (player.activeSelf) {
                    aliveCount++;
                }
            }
            if (aliveCount <= 1) {
                Invoke(nameof(NewRound), 2f);
            }
        }
        // Reset lại bàn chơi mới
        private void NewRound() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
