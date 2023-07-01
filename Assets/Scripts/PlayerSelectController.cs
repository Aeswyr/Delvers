using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerSelectController : MonoBehaviour
{
    private InputHandler input;
    private bool ready = false;

    [SerializeField] private Image portrait;
    [SerializeField] private List<Sprite> portraits;
    [SerializeField] private TextMeshProUGUI text;
    private int index;
    private float prevDir;

    // Update is called once per frame
    void FixedUpdate()
    {


        if (input.secondary.pressed) {
            if (ready) {
                ready = false;
                SelectMenuController.Instance.PingReady(ready);
                text.text = "[A] to Ready";
                text.color = Color.white;
            } else { //disconnect
                SelectMenuController.Instance.DeregisterPlayer(input.transform.GetComponent<PlayerFunctionController>());
                Destroy(input.gameObject);
                Destroy(gameObject);
            }
                 
        }

        if (ready)
            return;

        if (input.jump.pressed) {
            ready = true;
            SelectMenuController.Instance.PingReady(ready);
            text.text = "Ready!";
            text.color = Color.green;
        }

        if (input.move.pressed && prevDir == 0) {
            if (input.dir.x > 0)
                index = (index + 1) % portraits.Count;
            else if (input.dir.x < 0)
                index = (index - 1 + portraits.Count) % portraits.Count;

            portrait.sprite = portraits[index];

            input.GetComponent<PlayerFunctionController>().SetSelectedCharacter((PlayerCharacter)index);
        }

        prevDir = input.dir.x;
    }

    public bool IsReady() {
        return ready;
    }

    public void Init(InputHandler input) {
        this.input = input;
    }
}
