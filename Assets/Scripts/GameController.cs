using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var playerInput in FindObjectsOfType<PlayerFunctionController>()) {
            GameObject player = Instantiate(playerPrefab);
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.Init(playerInput.GetSelectedCharacter(), playerInput.GetComponent<InputHandler>());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SpawnCoins(int amount, Vector3 pos) {
        for (int i = 0; i < amount; i++) {
            var coin = Instantiate(coinPrefab, pos, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(8, 15));
        }
    }
}
