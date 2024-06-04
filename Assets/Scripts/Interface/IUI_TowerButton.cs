using UnityEngine;

public interface IUI_TowerButton {
    void Activation(Cell cell, GameObject buildEffect);

    void DeActivation();
}