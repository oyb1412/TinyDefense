using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public static class Define 
{
    #region Const
    public static readonly Color COLOR_ORANGE = new Color(1f, 0.6f, 0.2f, 1f);
    public static readonly Color COLOR_NOT = new Color(1f, 1f, 1f, 0f);

    public static readonly Vector3 ARROWHEAD_SCALE = new Vector3(.2f, .2f, 1f);

    public static readonly float ABILITY_ENEMY_MOVESPEED_MINUS = .9f;
    public static readonly float ABILITY_POISON_DEFAULT_DAMAGE = .2f;
    public static readonly float ABILITY_POISON_DEFAULT_TIME = 2f;
    public static readonly float ABILITY_STUN_DEFAULT_TIME = .5f;
    public static readonly float ABILITY_STUN_DEFAULT_CHANCE = 0.2f;
    public static readonly float ABILITY_PROJECTILE_DEFAULT_CHANCE = 0.2f;
    public static readonly float ABILITY_PROJECTILE_DEFAULT_DELAY = 0.1f;
    public static readonly float ABILITY_MANYENEMY_DAMAGE_UP = 0.01f;
    public static readonly float ABILITY_DEFAULT_CRITICAL_CHANCE = 0.7f;
    public static readonly float ABILITY_DEFAULT_CRITICAL_DAMAGE = 0.5f;
    public static readonly float ABILITY_DEFAULT_MISS_CHANCE = 0.3f;
    public static readonly float ABILITY_PLUS_TOWER_REWARD = 0.3f;

    public static readonly float SISTER_BUFF_RANGE = 1.5f;

    public static readonly float TOWER_MOVESPEED = 1f;

    public static readonly float TOWER_EXPLOSION_RADIUS = 1f;

    public static readonly float SKILL_TORNADO_RANGE = 2f;

    public static readonly int ABILITY_GETGOLD = 200;
    public static readonly int MINUS_TOWER_CREATE_COST = 3;
    public static readonly float ABILITY_PLUS_ENEMY_REWARD = 0.3f;

    public static readonly string[] ABILITY_NAME = {
        "황금 별",
        "황금 왕관",
        "거미줄 수정구",
        "돼지 저금통",
        "성장의 씨앗",
        "네잎클로버",
        "전사의 검",
        "궁수의 활",
        "마법사의 지팡이",
        "여신의 축복",
        "핵주먹",
        "맹독 구슬",
        "고양이 발바닥",
        "동전 무더기",
        "랜덤 박스",
    };

    public static readonly string[] ABILITY_DESCRIPTION = {
        "타워 설치 비용이 영구적으로 감소합니다.",
        "타워 판매 비용이 영구적으로 증가합니다.",
        "모든 적의 이동속도가 영구적으로 감소합니다.",
        "적에게서 획득하는 골드가 증가합니다.",
        "다음번 생성하는 세 개의 타워는 레벨이 증가한 상태로 소환됩니다.",
        "공격시 일정 확률로 추가 투사체를 발사합니다.",
        "전사 타워의 출현 확률이 증가합니다. ",
        "궁수 타워의 출현 확률이 증가합니다.",
        "마법사 타워의 출현 확률이 증가합니다.",
        "필드에 존재하는 적의 숫자에 비례해 추가피해를 입힙니다.",
        "모든 타워의 공격에 스턴 확률을 부여합니다.",
        "모든 타워의 공격에 중독 효과를 부여합니다.",
        "공격시 높은 확률로 크리티컬 공격이 발동하지만, 낮은 확률로 공격이 빗나갑니다.",
        $"{ABILITY_GETGOLD}골드를 획득합니다.",
        "0~2레벨 사이의 랜덤한 타워를 얻습니다.",
    };

    public static readonly string[] ABILITY_SPRITE_PATH = {
        "Sprite/Icon/AbilityIcon0",
        "Sprite/Icon/AbilityIcon1",
        "Sprite/Icon/AbilityIcon2",
        "Sprite/Icon/AbilityIcon3",
        "Sprite/Icon/AbilityIcon4",
        "Sprite/Icon/AbilityIcon5",
        "Sprite/Icon/AbilityIcon6",
        "Sprite/Icon/AbilityIcon7",
        "Sprite/Icon/AbilityIcon8",
        "Sprite/Icon/AbilityIcon9",
        "Sprite/Icon/AbilityIcon10",
        "Sprite/Icon/AbilityIcon11",
        "Sprite/Icon/AbilityIcon12",
        "Sprite/Icon/AbilityIcon13",
        "Sprite/Icon/AbilityIcon14",
    };

    public static readonly int CELL_COUNT = 36;

    public static readonly int BUILDEFFECT_POOL_COUNT = 5;

    public static readonly int ABILITY_REWARD_ROUND = 5;
    public static readonly int TOWER_CREATE_COST = 30;
    public static readonly int ROUND_SPAWN_COUNT = 20;
    public static readonly int GAME_START_GOLD = 90;

    public static readonly int MAX_ENEMY_TYPE = 16;

    public static readonly int ENEMY_SEARCH_MAX_COUNT = 5;
    public static readonly int ENEMY_MAX_COUNT = 5;
    public static readonly int SFX_DEFAULT_CHANNELS = 10;

    public static readonly float ENEMY_DEFAULT_SCALE = .5f;
    public static readonly float DEBUFF_DAMAGE_DEFAULT_TICK = 0.1f;

    public static readonly float PERMISSION_RANGE = 0.02f;
    public static readonly float PROJECTILE_PERMISSION_RANGE = 0.3f;
    public static readonly float PROJECTILE_VELOCITY = 15f;
    public static readonly float PROJECTILE_DESTROY_TIME = 1f;
    public static readonly float GAMELEVEL_UP_TICK = 30f;
    public static readonly float ENEMY_SPAWN_DELAY = 1f;

    public static readonly float MOUSE_CLICK_RANGE = 0.33f;



    public static readonly float BGM_DEFAULT_VOLUME = 0.5f;
    public static readonly float SFX_DEFAULT_VOLUME = 0.5f; 

    public static readonly float FADE_TIME = 2f; 
    public static readonly float PRESS_TIME = 0.3f; 

    public static readonly string TAG_DEAD = "Dead";
    public static readonly string TAG_TOWER = "Tower";
    public static readonly string TAG_GROUND = "Ground";
    public static readonly string TAG_RUN = "Run";
    public static readonly string TAG_STUN = "Stun";
    public static readonly string TAG_ATTACK = "Attack";
    public static readonly string TAG_MOVEMENT = "Movement";
    public static readonly string TAG_Idle = "Idle";
    public static readonly string TAG_ENEMY = "Enemy";
    public static readonly string TAG_AUTOCREATE_BUTTON = "AutoCreate";
    public static readonly string ENEMY_MOVE_PATH = "EnemyMovePath";
    public static readonly string TAG_LOGIN_DATA = "LoginData";
    public static readonly string TAG_SCORE_DATA = "ScoreData";
    public static readonly string TAG_SCORE = "Score";
    public static readonly string TAG_KEY_DATA = "KeyData";
    public static readonly string TAG_DATA_KEY = "DataKey";
    public static readonly string TAG_KEY_DOCUMENT = "TqFvLDUVjjYWMfHDdMrX";

    public static readonly string[] TOWER_NAME = { "기사", "전사", "도적", "헌터", "레인저", "저격수",
    "화염마법사", "얼음마법사", "수녀"};

    public static readonly string OBJECT_REWARD_PATH = "Prefabs/Objects/Other/Gold";
    public static readonly string MENT_CREATE_COST = "타워 설치<color=#FFFF00>-{0}g</color>";
    public static readonly string MENT_SELL_REWARD = "타워 판매<color=#FFFF00>+{0}g</color>";
    public static readonly string MENT_TOWER_LEVEL = "레벨 : {0}";
    public static readonly string MENT_TOWER_KILL = "처치 수 : {0}";
    public static readonly string MENT_TOWER_DAMAGE = "공격력 : <color=#FFA500>{0:0.0}</color>";
    public static readonly string MENT_TOWER_DELAY = "공격 속도 : <color=#FFA500>{0:0.0}</color>";
    public static readonly string MENT_TOWER_RANGE = "공격 사거리 : <color=#FFA500>{0:0.0}</color>";

    public static readonly string MENT_MAX_DELAY = "공격속도 MAX";

    public static readonly string SKILL_TORNADO_PATH = "Prefabs/Objects/Skill/Skill_Tornado";
    public static readonly string SPRITE_BUTTON_DEFAULT_SPEED = "Sprite/Button/Button_DefaultSpeed";
    public static readonly string SPRITE_BUTTON_FASE_SPEED = "Sprite/Button/Button_FastSpeed";
    public static readonly string EFFECT_TOWER_BUILD = "Prefabs/Objects/Effect/Tower/BuildEffect";

    public static readonly string TILE_PATH = "Prefabs/Other/CreateTile";
    public static readonly string ARROWHEAD_PATH = "Objects/Other/ArrowHead";
    
    public static readonly string MENT_BUTTON_SAVE = "저장하시겠습니까?";
    public static readonly string MENT_FAIELD_LOGIN = "로그인 실패";
    public static readonly string MENT_SUCCESS_LOGIN = "로그인 성공";
    public static readonly string MENT_SUCCESS_REGISTRETION = "회원가입 성공";
    public static readonly string MENT_FAIELD_REGISTRETION = "회원가입 실패";
    public static readonly string MENT_BUTTON_HOME = "정말로 종료하시겠습니까?";
    public static readonly string MENT_BUTTON_BUY = "정말로 구매하시겠습니까?";

    public static readonly string MENT_GAME_ROUND = "{0} 라운드";
    public static readonly string MENT_GAME_ENEMY_NUMBER = "{0}/{1}";
    public static readonly string[] MENT_TOWER_ENHANCE_LEVEL = {
        "공격력 증가\n(Lv.{0})",
        "공격속도 증가\n(Lv.{0})",
    };

    public static readonly string UPGRADE_ICON_PATH = "Sprite/UI/Icon{0}";

    public static readonly string MENT_PLUS_GOLD = "+{0}g";
    public static readonly string MENT_GOLD = "{0}g";

    public static readonly string[] MENT_SKILL_DESCRIPTION = {
        "일정 시간\n공격력 및\n공격속도가\n증가합니다.\n(Lv.{0})",
        "산사태를 일으켜\n모든 적을\n기절시킵니다.\n(Lv.{0})",
        "맵을 순회하는\n회오리바람을\n일으킵니다.\n(Lv.{0})",
    };

    public static readonly string[] UPGRADE_NAME = {
        "건설자의 망치",
        "황금 열쇠",
        "톱니바퀴",
        "튕기는 검",
        "푸른 번개",
        "마법 고서",
    };

    public static readonly string[] UPGRADE_DESCRIPTION = {
        "타워 생성시 {0}%확률로 1레벨 증가한 타워를 생성합니다",
        "타워 합체시 {0}%확률로 1레벨 증가한 타워를 생성합니다",
        "게임 배속 시스템을 영구적으로 해금합니다",
        "전사 타입 타워들의 공격이 {0}%확률로 튕깁니다",
        "궁수 타입 타워들의 공격이 {0}%확률로 적을 관통합니다",
        "마법사 타입 타워들의 공격 범위가 {0}% 증가합니다",
    };

   
    public static readonly string[] TOWER_DESCRIPTION = {
        "모든 능력치가 균형 잡힌 강력한 유닛입니다",
        "느리지만 강력한 한방을 지닌 유닛입니다",
        "매우 빠른 공격속도로 적을 제압합니다",
        "긴 사거리와 좋은 밸런스를 가진 유닛입니다",
        "매우 빠른 공격속도로 적을 연속 공격합니다",
        "매우 느리지만 강력한 한방의 유닛입니다",
        "적을 불태워 지속 피해를 입힙니다",
        "적을 느리게 만들어 제압하는 유닛입니다",
        "아군을 강화하여 전투력을 높이는 유닛입니다",
    };

    public static readonly string ENEMY_PREFAB_PATH = "Prefabs/Objects/Enemy/Enemy_{0}";
    public static readonly int ENEMY_MAX_LEVEL = 20;

    public static readonly Vector3 DEFAULT_CREATE_POSITION = new Vector3(-1.75f, .1f, 0f);



    public static readonly int ENHANCE_MAXLEVEL = 50;

    public static readonly int SKILL_MAX_LEVEL = 5;

    [JsonConverter(typeof(Vector3Converter))]
    public static readonly Vector3 SKILL_TORNADO_DEFAULT_POSITION = new Vector3(-1.75f, -3.25f, 0f);
    public static readonly float SKILL_TORNADO_MOVESPEED = 1.5f;

    public static readonly string[] TOWER_PATH = {
        "Objects/Tower/Knight",
        "Objects/Tower/Warrior",
        "Objects/Tower/Rogue",
        "Objects/Tower/Hunter",
        "Objects/Tower/Ranger",
        "Objects/Tower/Sniper",
        "Objects/Tower/Firemage",
        "Objects/Tower/Icemage",
        "Objects/Tower/Sister",
    };

    public static readonly string[] TOWER_PREFAB_PATH = {
        "Prefabs/Objects/Tower/Knight",
        "Prefabs/Objects/Tower/Warrior",
        "Prefabs/Objects/Tower/Rogue",
        "Prefabs/Objects/Tower/Hunter",
        "Prefabs/Objects/Tower/Ranger",
        "Prefabs/Objects/Tower/Sniper",
        "Prefabs/Objects/Tower/Firemage",
        "Prefabs/Objects/Tower/Icemage",
        "Prefabs/Objects/Tower/Sister",
    };

    public static readonly string[] PROJECTILE_PATH = {
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Sword",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Axe",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Dagger",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Arrow",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Arrow",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Arrow",
        "Prefabs/Objects/Projectile/ExplosionProjectile/Projectile_Fireball",
        "Prefabs/Objects/Projectile/ExplosionProjectile/Projectile_Icearrow",
        "Prefabs/Objects/Projectile/NormalProjectile/Projectile_Holyarrow",
    };

    public static readonly string[] PROJECTILE_EXPLOSION_PATH = {
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
        "Prefabs/Objects/Effect/ExplosionEffect/FireExplosion",
        "Prefabs/Objects/Effect/ExplosionEffect/IceExplosion",
        "Prefabs/Objects/Effect/NormalEffect/NormalExplosion",
    };


    [JsonConverter(typeof(ColorConverter))]
    public static readonly Color[] COLOR_TOWERLEVEL = {
        Color.green,
        Color.blue,
        Color.magenta,
        Color.red,
        Color.yellow,
        Color.black,
    };

    public static readonly int TOWER_MAXLEVEL = 5;

    public static readonly string TOWERICON_SPRITE_PATH = "Sprite/Tower/TowerIcons";



    public static readonly string[] BGM_PATH = { 

    };

    public static readonly string[] SFX_PATH = {

    };



    public static readonly Vector3 TOWER_CREATE_POSITION = Vector3.down * 0.25f;
    #endregion
    #region Enum
    public enum UpgradeType {
        Hammer,
        Key,
        Gear,
        Sword,
        Thrunder,
        Book,
        Count
    }
    public enum BGMType {
    }

    public enum SFXType {

    }

    public enum BuffType {
        AttackDamageUp,
        AttackDelayDown,
    }

    public enum GameSpeed {
        Default,
        Fast,
        Stop,
    }

    public enum DebuffType {
        Fire,
        Slow,
        Poison,
        Stun,
        Count,
    }

    public enum DebuffBundle {
        Movement,
        Damage,
        Count,
    }

    public enum TowerState {
        Idle,
        Movement,
        Attack,
    }

    public enum AbilityType {
        MinusCost,
        PlusSellGold,
        MinusEnemyMoveSpeed,
        PlusGetGold,
        NextThreeTowerLevelThree,
        PlusProjectile,
        PlusSoilderTowerPercentage,
        PlusArcherTowerPercentage,
        PlusMageTowerPercentage,
        PlusAttackDamageUpToManyEnemy,
        PlusStunPercentage,
        PlusPoisonDamage,
        PlusCriticalChanceAndMissChance,
        GetGold,
        GetTower,
        Count,
    }

    public enum SkillType {
        PowerUp,
        Stun,
        Tornado,
        Count,
    }

    public enum EnemyState {
        Run,
        Stun,
        Dead,
    }
    public enum SceneType
    {
        None,
        Main,
        Login,
        Ingame,
    }

    public enum TowerBundle {
        Soldier,
        Archer,
        Mage,
        Count,
    }

    public enum EnhanceType {
        AttackDamage,
        AttackDelay,
        Count,
    }

    public enum TowerType {
        Knight,
        Warrior,
        Rogue,
        Hunter,
        Ranger,
        Sniper,
        Firemage,
        Icemage,
        Sister,
        Count,
    }

    public enum Direction {
        None,
        Right,
        Left,
    }
   
    #endregion

    #region Class
    public class EnhanceValue {
        public int Level { get; set; }
        public float Value { get; set; }
        public int Cost { get; set; }

        public EnhanceValue(int level, float value, int cost) {
            Level = level;
            Value = value;
            Cost = cost;
        }
    }

    public class UpgradeValue {
        public int Level { get; set; }
        public float Value { get; private set; }
        public int Cost { get; private set; }

        public Sprite OriginalSprite { get; private set; }
        public Sprite InputSprite { get; private set; }
        public bool IsSelect { get; set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public UpgradeValue(int level, float value, int cost) {
            Level = level;
            Value = value;
            Cost = cost;
            
        }

        public void Init(Sprite original, Sprite input
            , bool isSelect, string name, string description) {
            OriginalSprite = original;
            InputSprite = input;
            IsSelect = isSelect;
            Name = name;
            Description = description;
        }
    }

    public class SkillValue {
        public int Level { get; set; }
        public float Value { get; private set; }
        public float Time { get; private set; }
        public int Cost { get; private set; }
        public float Cooltime { get; private set; }
        
        public SkillValue(int level, float value, int cost, float time, float coolTime) {
            Level = level;
            Value = value;
            Cost = cost;
            Time = time;
            Cooltime = coolTime;
        }

        public void Init(float value, int cost, float time, float coolTime) {
            Value = value;
            Cost = cost;
            Time = time;
            Cooltime = coolTime;
        }
    }

    public class AbilityValue {
        public string Name { get; private set; }
        public AbilityType Type { get; private set; }
        public string Description { get; private set; }
        public Sprite Sprite { get; private set; }
        public AbilityValue(AbilityType type) {
            Type = type;
            Name = ABILITY_NAME[(int)Type];
            Description = ABILITY_DESCRIPTION[(int)Type];
            Sprite = Resources.Load<Sprite>(ABILITY_SPRITE_PATH[(int)Type]);
        }
    }
    #endregion
}
