using UnityEngine;

/// <summary>
/// Ÿ���� ���õ� ��ưUI ���� �������̽�
/// </summary>
public interface IUI_TowerButton {
    //Ȱ��ȭ
    void Activation(Cell cell, GameObject buildEffect);
    //��Ȱ��ȭ
    void DeActivation();
}