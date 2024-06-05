using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ��ŷ �ǳ� ���� Ŭ����
/// </summary>
public class UI_RankingPanel : MonoBehaviour {
    //������ �� ��ŷ ��������
    private UI_RankingContentPanel[] contentPanels;
    //��ŷ ������
    private Dictionary<string, object> rankingData;

    /// <summary>
    /// ��ŷ�ǳ� Ȱ��ȭ ��
    /// �� �������鿡 ������ ����
    /// </summary>
    public void Activation() {
        //���⼭ �ڷ�ƾ���� ��ŷ �ҷ�����
        //�ҷ����� �Ϸ�Ǹ� �ǳ� Activation
        gameObject.SetActive(true);

        if (contentPanels == null) {
            contentPanels = GetComponentsInChildren<UI_RankingContentPanel>();
        }

        StartCoroutine(Co_LoadRankingData());
    }

    /// <summary>
    /// ��� ��ŷ������ �ε� �� �������鿡 ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_LoadRankingData() {
        LoadRankingData();
        //��� ������ �ε���� ���
        yield return new WaitUntil(() => rankingData != null && rankingData.Count != 0);

        List<string> nameData = new List<string>(rankingData.Keys);
        List<object> scoreData = new List<object>(rankingData.Values);
        //��� ������ �ǳ� ��Ȱ��ȭ
        foreach(var item in contentPanels)
        {
            item.gameObject.SetActive(false);
        }

        //�����ϴ� ��ŷ �� ��ŭ ������ �ǳ� Ȱ��ȭ �� ���� ǥ��
        for (int i = 0; i< rankingData.Count; i++) {
            contentPanels[i].Activation(nameData[i], Convert.ToInt32(scoreData[i]), i);
        }
    }

    /// <summary>
    /// ���̾�̽����� ��� ��ŷ ���� �ε�
    /// ������������ ����
    /// </summary>
    private async void LoadRankingData() {
        rankingData = await Managers.FireStore.LoadAllDataFromDocument(Define.TAG_SCORE_DATA, Define.TAG_SCORE_DATA);
            
        if(rankingData == null || rankingData.Count == 0) {
            StopAllCoroutines();
            gameObject.SetActive(false);
            Debug.Log("��ŷ ����� �������� �ʽ��ϴ�.");
        }
        else {
            rankingData.OrderByDescending(x => x.Key);
        }
    }
}