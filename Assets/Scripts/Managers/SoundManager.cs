using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 사운드 관리
/// </summary>
public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    private AudioSource bgmPlayer;  //bgm은 한 번에 하나만 출력 가능
    private AudioSource[] sfxPlayers;  //여러 sfx를 동시에 출력하기 위한 배열
    private Dictionary<Define.BGMType, AudioClip> bgmClips;  // 게임에 존재하는 모든 bgm 목록
    private Dictionary<Define.SFXType, AudioClip> sfxClips;  // 게임에 존재하는 모든 sfx 목록

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
            
    }

    private void Start() {
        InitBgm();  //bgm 초기화
        InitSfx();  //sfx 초기화
    }

    /// <summary>
    /// bgm 초기화
    /// </summary>
    private void InitBgm() {
        GameObject bgmObject = new GameObject("BgmPlayer");  //bgm player 생성
        bgmObject.transform.parent = transform;  //부모를 manager로 지정
        bgmPlayer = bgmObject.AddComponent<AudioSource>();  //오디오 소스 추가
        bgmPlayer.playOnAwake = false;  //즉시 재생 해제
        bgmPlayer.loop = true;  //loop 설정
        bgmPlayer.volume = Define.BGM_DEFAULT_VOLUME;  //기본 볼륨 설정

        bgmClips = new Dictionary<Define.BGMType, AudioClip>();
        foreach (Define.BGMType bgmType in System.Enum.GetValues(typeof(Define.BGMType))) {
            string path = Define.BGM_PATH[(int)bgmType];
            AudioClip clip = Resources.Load<AudioClip>(path);
            bgmClips[bgmType] = clip;
        }
    }

    /// <summary>
    /// sfx 초기화
    /// </summary>
    private void InitSfx() {
        GameObject sfxObject = new GameObject("SfxPlayer");  //sfx player 생성
        sfxObject.transform.parent = transform; //부모를 manager로 지정
        sfxPlayers = new AudioSource[Define.SFX_DEFAULT_CHANNELS];  //채널 수 만큼 player 생성

        for (int i = 0; i < sfxPlayers.Length; i++) {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();  //각 player에 오디오 소스 추가
            sfxPlayers[i].playOnAwake = false;  //즉시 재생 해제
            sfxPlayers[i].volume = Define.SFX_DEFAULT_VOLUME;  //기본 볼륨 설정
        }

        sfxClips = new Dictionary<Define.SFXType, AudioClip>();
        foreach (Define.SFXType sfxType in System.Enum.GetValues(typeof(Define.SFXType))) {
            string path = Define.SFX_PATH[(int)sfxType];
            AudioClip clip = Resources.Load<AudioClip>(path);
            sfxClips[sfxType] = clip;
        }
    }

    public void SetBGMMute(bool trigger) => bgmPlayer.mute = trigger;

    public void SetSFXMute(bool trigger) {
        for (int i = 0; i < sfxPlayers.Length; i++)
            sfxPlayers[i].mute = trigger;
    }

    /// <summary>
    /// bgm 볼륨 설정
    /// </summary>
    /// <param name="volume">볼륨</param>
    public void SetBgmVolume(float volume) => bgmPlayer.volume = volume;

    /// <summary>
    /// sfx 볼륨 설정
    /// </summary>
    /// <param name="volume">볼륨</param>
    public void SetSfxVolume(float volume) {
        for (int i = 0; i < sfxPlayers.Length; i++)
            sfxPlayers[i].volume = volume;
    }

    /// <summary>
    /// bgm 재생 및 정지
    /// </summary>
    /// <param name="islive">재생 or 정지</param>
    /// <param name="bgm">재생할 bgm 타입</param>
    public void SetBgm(bool islive, Define.BGMType bgm) {
        bgmPlayer.Stop();  //어떤 상황에서든지 현재의 bgm 정지
        if (bgmClips.TryGetValue(bgm, out AudioClip clip)) {
            bgmPlayer.clip = clip;  //클립 교체
            if (islive)
                bgmPlayer.Play();  //재생
        }
    }

    /// <summary>
    /// sfx 재생
    /// </summary>
    /// <param name="sfx">재생할 sfx 타입</param>
    public void PlaySfx(Define.SFXType sfx) {
        if (sfxClips.TryGetValue(sfx, out AudioClip clip)) {
            for (int i = 0; i < sfxPlayers.Length; i++) {
                if (sfxPlayers[i].isPlaying)  //플레이어중 사용 가능한 플레이어를 서치
                    continue;

                sfxPlayers[i].clip = clip;  //사용가능한 플레이어를 서치하면, 클립 설정 후 플레이
                sfxPlayers[i].Play();
                break;
            }
        }
    }
}
