using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ���� ����
/// </summary>
public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    private AudioSource bgmPlayer;  //bgm�� �� ���� �ϳ��� ��� ����
    private AudioSource[] sfxPlayers;  //���� sfx�� ���ÿ� ����ϱ� ���� �迭
    private Dictionary<Define.BGMType, AudioClip> bgmClips;  // ���ӿ� �����ϴ� ��� bgm ���
    private Dictionary<Define.SFXType, AudioClip> sfxClips;  // ���ӿ� �����ϴ� ��� sfx ���

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
        InitBgm();  //bgm �ʱ�ȭ
        InitSfx();  //sfx �ʱ�ȭ
    }

    private void Update() {
        if (bgmPlayer == null)
            Debug.Log("bgmplayer null");
    }

    /// <summary>
    /// bgm �ʱ�ȭ
    /// </summary>
    private void InitBgm() {
        GameObject bgmObject = new GameObject("BgmPlayer");  //bgm player ����
        bgmObject.transform.parent = transform;  //�θ� manager�� ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();  //����� �ҽ� �߰�
        bgmPlayer.playOnAwake = false;  //��� ��� ����
        bgmPlayer.loop = true;  //loop ����
        bgmPlayer.volume = Managers.Data.DefineData.BGM_DEFAULT_VOLUME;  //�⺻ ���� ����
        bgmPlayer.dopplerLevel = 0f;
        bgmPlayer.reverbZoneMix = 0f;
        bgmClips = new Dictionary<Define.BGMType, AudioClip>();

        for(int i = 0; i< (int)Define.BGMType.Count; i++) {
            string path = Managers.Data.DefineData.BGM_PATH[i];
            AudioClip clip = Resources.Load<AudioClip>(path);
            bgmClips[(Define.BGMType)i] = clip;
        }

        bgmPlayer.clip = bgmClips[Define.BGMType.Main];
    }

    /// <summary>
    /// sfx �ʱ�ȭ
    /// </summary>
    private void InitSfx() {
        GameObject sfxObject = new GameObject("SfxPlayer");  //sfx player ����
        sfxObject.transform.parent = transform; //�θ� manager�� ����
        sfxPlayers = new AudioSource[Managers.Data.DefineData.SFX_DEFAULT_CHANNELS];  //ä�� �� ��ŭ player ����

        for (int i = 0; i < sfxPlayers.Length; i++) {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();  //�� player�� ����� �ҽ� �߰�
            sfxPlayers[i].playOnAwake = false;  //��� ��� ����
            sfxPlayers[i].volume = Managers.Data.DefineData.SFX_DEFAULT_VOLUME;  //�⺻ ���� ����
            sfxPlayers[i].dopplerLevel = 0f;
            sfxPlayers[i].reverbZoneMix = 0f;
        }

        sfxClips = new Dictionary<Define.SFXType, AudioClip>();

        for(int i = 0; i< (int)Define.SFXType.Count; i++) {
            string path = Managers.Data.DefineData.SFX_PATH[i];
            AudioClip clip = Resources.Load<AudioClip>(path);
            sfxClips[(Define.SFXType)i] = clip;
        }
    }

    public bool GetBGMPlaying() {
        return bgmPlayer.isPlaying;
    }

    public void SetBGMMute(bool trigger) => bgmPlayer.mute = trigger;

    public void SetSFXMute(bool trigger) {
        for (int i = 0; i < sfxPlayers.Length; i++)
            sfxPlayers[i].mute = trigger;
    }

    public float GetBgmVolume() {
        return bgmPlayer.volume;
    }

    public float GetSFXVolume() {
        return sfxPlayers[0].volume;
    }

    /// <summary>
    /// bgm ���� ����
    /// </summary>
    /// <param name="volume">����</param>
    public void SetBgmVolume(float volume) => bgmPlayer.volume = volume;

    /// <summary>
    /// sfx ���� ����
    /// </summary>
    /// <param name="volume">����</param>
    public void SetSfxVolume(float volume) {
        for (int i = 0; i < sfxPlayers.Length; i++)
            sfxPlayers[i].volume = volume;
    }

    /// <summary>
    /// bgm ��� �� ����
    /// </summary>
    /// <param name="play">��� or ����</param>
    /// <param name="bgm">����� bgm Ÿ��</param>
    public void SetBgm(bool play, Define.BGMType bgm) {
        if(bgmPlayer.clip != null)
            bgmPlayer.Stop();  //� ��Ȳ�������� ������ bgm ����

        if (bgmClips.TryGetValue(bgm, out AudioClip clip)) {
            bgmPlayer.clip = clip;  //Ŭ�� ��ü
            if (play)
                bgmPlayer.Play();  //���
        }
    }

    /// <summary>
    /// sfx ���
    /// </summary>
    /// <param name="sfx">����� sfx Ÿ��</param>
    public void PlaySfx(Define.SFXType sfx) {
        if (sfxClips.TryGetValue(sfx, out AudioClip clip)) {
            for (int i = 0; i < sfxPlayers.Length; i++) {
                if (sfxPlayers[i].isPlaying)  //�÷��̾��� ��� ������ �÷��̾ ��ġ
                    continue;

                sfxPlayers[i].clip = clip;  //��밡���� �÷��̾ ��ġ�ϸ�, Ŭ�� ���� �� �÷���
                sfxPlayers[i].Play();
                return;
            }

            if (sfx == Define.SFXType.PowerUpSkill || sfx == Define.SFXType.StunSkill || sfx == Define.SFXType.TornadoSkill
            || sfx == Define.SFXType.GameOver) {
                int ran = Random.Range(0, sfxPlayers.Length);
                sfxPlayers[ran].clip = clip;
                sfxPlayers[ran].Play();
                return;
            }
        }
    }
}
