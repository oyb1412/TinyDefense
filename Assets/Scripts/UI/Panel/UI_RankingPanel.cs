using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class UI_RankingPanel : MonoBehaviour {
    private UI_RankingContentPanel[] contentPanels;
    private Dictionary<string, object> rankingData;

    public void Activation() {
        //���⼭ �ڷ�ƾ���� ��ŷ �ҷ�����
        //�ҷ����� �Ϸ�Ǹ� �ǳ� Activation
        gameObject.SetActive(true);

        if (contentPanels == null) {
            contentPanels = GetComponentsInChildren<UI_RankingContentPanel>();
        }


        StartCoroutine(Co_LoadRankingData());
    }

    private IEnumerator Co_LoadRankingData() {
        LoadRankingData();
        yield return new WaitUntil(() => rankingData != null && rankingData.Count != 0);

        List<string> nameData = new List<string>(rankingData.Keys);
        List<object> scoreData = new List<object>(rankingData.Values);

        foreach(var item in contentPanels)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i< rankingData.Count; i++) {
            contentPanels[i].Activation(nameData[i], Convert.ToInt32(scoreData[i]), i);
        }
    }

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