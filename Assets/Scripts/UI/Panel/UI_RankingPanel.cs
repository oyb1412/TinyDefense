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
    private Dictionary<string, object> rankingData = new Dictionary<string, object>();

    /// <summary>
    /// ��ŷ�ǳ� Ȱ��ȭ ��
    /// �� �������鿡 ������ ����
    /// </summary>
    public void Activation() {
        //���⼭ �ڷ�ƾ���� ��ŷ �ҷ�����
        //�ҷ����� �Ϸ�Ǹ� �ǳ� Activation

        if (contentPanels == null) {
            contentPanels = GetComponentsInChildren<UI_RankingContentPanel>();
        }
        gameObject.SetActive(true);

        StartCoroutine(Co_LoadRankingData());


    }


    /// <summary>
    /// ���̾�̽����� ��� ��ŷ ���� �ε�
    /// ������������ ����
    /// </summary>
    private IEnumerator Co_LoadRankingData() {
        var task =  Managers.FireStore.LoadAllDataFromDocument(Managers.Data.DefineData.TAG_SCORE_DATA, Managers.Data.DefineData.TAG_SCORE_DATA);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null) {
            Debug.LogError("Error loading ranking data: " + task.Exception);
            yield break;
        }

        rankingData = task.Result;

        if (rankingData == null || rankingData.Count == 0) {
            StopAllCoroutines();
            gameObject.SetActive(false);
            DebugWrapper.Log("��ŷ ����� �������� �ʽ��ϴ�.");
        } 
        else {
            Dictionary<string, int> score = new Dictionary<string, int>();

            foreach(var item in rankingData) {
                int value = Convert.ToInt32(item.Value);
                score[item.Key] = value;
            }

            score = score.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            DebugWrapper.Log("��ŷ ���� �Ϸ�.");

            List<string> nameData = new List<string>(score.Keys);
            List<int> scoreData = new List<int>(score.Values);

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

            yield return null;
    }
}