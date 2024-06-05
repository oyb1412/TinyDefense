using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 랭킹 판넬 관리 클래스
/// </summary>
public class UI_RankingPanel : MonoBehaviour {
    //하위의 각 랭킹 컨텐츠들
    private UI_RankingContentPanel[] contentPanels;
    //랭킹 데이터
    private Dictionary<string, object> rankingData;

    /// <summary>
    /// 랭킹판넬 활성화 시
    /// 각 컨텐츠들에 데이터 대입
    /// </summary>
    public void Activation() {
        //여기서 코루틴으로 랭킹 불러오기
        //불러오기 완료되면 판넬 Activation
        gameObject.SetActive(true);

        if (contentPanels == null) {
            contentPanels = GetComponentsInChildren<UI_RankingContentPanel>();
        }

        StartCoroutine(Co_LoadRankingData());
    }

    /// <summary>
    /// 모든 랭킹데이터 로드 및 컨텐츠들에 대입
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_LoadRankingData() {
        LoadRankingData();
        //모든 데이터 로드까지 대기
        yield return new WaitUntil(() => rankingData != null && rankingData.Count != 0);

        List<string> nameData = new List<string>(rankingData.Keys);
        List<object> scoreData = new List<object>(rankingData.Values);
        //모든 콘텐츠 판넬 비활성화
        foreach(var item in contentPanels)
        {
            item.gameObject.SetActive(false);
        }

        //존재하는 랭킹 수 만큼 콘텐츠 판넬 활성화 및 정보 표기
        for (int i = 0; i< rankingData.Count; i++) {
            contentPanels[i].Activation(nameData[i], Convert.ToInt32(scoreData[i]), i);
        }
    }

    /// <summary>
    /// 파이어베이스에서 모든 랭킹 정보 로드
    /// 오름차순으로 정렬
    /// </summary>
    private async void LoadRankingData() {
        rankingData = await Managers.FireStore.LoadAllDataFromDocument(Define.TAG_SCORE_DATA, Define.TAG_SCORE_DATA);
            
        if(rankingData == null || rankingData.Count == 0) {
            StopAllCoroutines();
            gameObject.SetActive(false);
            Debug.Log("랭킹 목록이 존재하지 않습니다.");
        }
        else {
            rankingData.OrderByDescending(x => x.Key);
        }
    }
}