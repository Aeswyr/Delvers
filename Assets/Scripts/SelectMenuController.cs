using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenuController : Singleton<SelectMenuController>
{
    [SerializeField] private GameObject characterSelectPrefab;
    [SerializeField] private Transform characterHolder;

    private List<PlayerFunctionController> players = new();

    int readyCount;

    public void RegisterNewPlayer(PlayerFunctionController controller) {
        GameObject newHolder = Instantiate(characterSelectPrefab, characterHolder);

        newHolder.transform.GetComponent<PlayerSelectController>().Init(controller.transform.GetComponent<InputHandler>());
        
        players.Add(controller);
    }

    public void DeregisterPlayer(PlayerFunctionController controller) {
        players.Remove(controller);
    }

    public void PingReady(bool ready) {
        if (ready) {
            readyCount++;
            if (readyCount == players.Count)
                SceneManager.LoadScene("GameScene");
            return;
        }


    }
}
