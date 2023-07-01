using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionController : MonoBehaviour
{
    private PlayerCharacter selectedCharacter;
    void Start() {
        SelectMenuController.Instance.RegisterNewPlayer(this);
    }

    public void SetSelectedCharacter(PlayerCharacter character) {
        this.selectedCharacter = character;
    }

    public PlayerCharacter GetSelectedCharacter() {
        return selectedCharacter;
    }
}

public enum PlayerCharacter {
    DUELIST, MAGE, WARRIOR, ARCHER
}