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

    /// <summary>
    /// bgm �ʱ�ȭ
    /// </summary>
    private void InitBgm() {
        GameObject bgmObject = new GameObject("BgmPlayer");  //bgm player ����
        bgmObject.transform.parent = transform;  //�θ� manager�� ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();  //����� �ҽ� �߰�
        bgmPlayer.playOnAwake = false;  //��� ��� ����
        bgmPlayer.loop = true;  //loop ����
        bgmPlayer.volume = Define.BGM_DEFAULT_VOLUME;  //�⺻ ���� ����

        bgmClips = new Dictionary<Define.BGMType, AudioClip>();
        foreach (Define.BGMType bgmType in System.Enum.GetValues(typeof(Define.BGMType))) {
            string path = Define.BGM_PATH[(int)bgmType];
            AudioClip clip = Resources.Load<AudioClip>(path);
            bgmClips[bgmType] = clip;
        }
    }

    /// <summary>
    /// sfx �ʱ�ȭ
    /// </summary>
    private void InitSfx() {
        GameObject sfxObject = new GameObject("SfxPlayer");  //sfx player ����
        sfxObject.transform.parent = transform; //�θ� manager�� ����
        sfxPlayers = new AudioSource[Define.SFX_DEFAULT_CHANNELS];  //ä�� �� ��ŭ player ����

        for (int i = 0; i < sfxPlayers.Length; i++) {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();  //�� player�� ����� �ҽ� �߰�
            sfxPlayers[i].playOnAwake = false;  //��� ��� ����
            sfxPlayers[i].volume = Define.SFX_DEFAULT_VOLUME;  //�⺻ ���� ����
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
    /// <param name="islive">��� or ����</param>
    /// <param name="bgm">����� bgm Ÿ��</param>
    public void SetBgm(bool islive, Define.BGMType bgm) {
        bgmPlayer.Stop();  //� ��Ȳ�������� ������ bgm ����
        if (bgmClips.TryGetValue(bgm, out AudioClip clip)) {
            bgmPlayer.clip = clip;  //Ŭ�� ��ü
            if (islive)
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
                break;
            }
        }
    }
}
