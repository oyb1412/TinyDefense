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
        "Ȳ�� ��",
        "Ȳ�� �հ�",
        "�Ź��� ������",
        "���� ������",
        "������ ����",
        "����Ŭ�ι�",
        "������ ��",
        "�ü��� Ȱ",
        "�������� ������",
        "������ �ູ",
        "���ָ�",
        "�͵� ����",
        "����� �߹ٴ�",
        "���� ������",
        "���� �ڽ�",
    };

    public static readonly string[] ABILITY_DESCRIPTION = {
        "Ÿ�� ��ġ ����� ���������� �����մϴ�.",
        "Ÿ�� �Ǹ� ����� ���������� �����մϴ�.",
        "��� ���� �̵��ӵ��� ���������� �����մϴ�.",
        "�����Լ� ȹ���ϴ� ��尡 �����մϴ�.",
        "������ �����ϴ� �� ���� Ÿ���� ������ ������ ���·� ��ȯ�˴ϴ�.",
        "���ݽ� ���� Ȯ���� �߰� ����ü�� �߻��մϴ�.",
        "���� Ÿ���� ���� Ȯ���� �����մϴ�. ",
        "�ü� Ÿ���� ���� Ȯ���� �����մϴ�.",
        "������ Ÿ���� ���� Ȯ���� �����մϴ�.",
        "�ʵ忡 �����ϴ� ���� ���ڿ� ����� �߰����ظ� �����ϴ�.",
        "��� Ÿ���� ���ݿ� ���� Ȯ���� �ο��մϴ�.",
        "��� Ÿ���� ���ݿ� �ߵ� ȿ���� �ο��մϴ�.",
        "���ݽ� ���� Ȯ���� ũ��Ƽ�� ������ �ߵ�������, ���� Ȯ���� ������ �������ϴ�.",
        $"{ABILITY_GETGOLD}��带 ȹ���մϴ�.",
        "0~2���� ������ ������ Ÿ���� ����ϴ�.",
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

    public static readonly string[] TOWER_NAME = { "���", "����", "����", "����", "������", "���ݼ�",
    "ȭ��������", "����������", "����"};

    public static readonly string OBJECT_REWARD_PATH = "Prefabs/Objects/Other/Gold";
    public static readonly string MENT_CREATE_COST = "Ÿ�� ��ġ<color=#FFFF00>-{0}g</color>";
    public static readonly string MENT_SELL_REWARD = "Ÿ�� �Ǹ�<color=#FFFF00>+{0}g</color>";
    public static readonly string MENT_TOWER_LEVEL = "���� : {0}";
    public static readonly string MENT_TOWER_KILL = "óġ �� : {0}";
    public static readonly string MENT_TOWER_DAMAGE = "���ݷ� : <color=#FFA500>{0:0.0}</color>";
    public static readonly string MENT_TOWER_DELAY = "���� �ӵ� : <color=#FFA500>{0:0.0}</color>";
    public static readonly string MENT_TOWER_RANGE = "���� ��Ÿ� : <color=#FFA500>{0:0.0}</color>";

    public static readonly string MENT_MAX_DELAY = "���ݼӵ� MAX";

    public static readonly string SKILL_TORNADO_PATH = "Prefabs/Objects/Skill/Skill_Tornado";
    public static readonly string SPRITE_BUTTON_DEFAULT_SPEED = "Sprite/Button/Button_DefaultSpeed";
    public static readonly string SPRITE_BUTTON_FASE_SPEED = "Sprite/Button/Button_FastSpeed";
    public static readonly string EFFECT_TOWER_BUILD = "Prefabs/Objects/Effect/Tower/BuildEffect";

    public static readonly string TILE_PATH = "Prefabs/Other/CreateTile";
    public static readonly string ARROWHEAD_PATH = "Objects/Other/ArrowHead";
    
    public static readonly string MENT_BUTTON_SAVE = "�����Ͻðڽ��ϱ�?";
    public static readonly string MENT_FAIELD_LOGIN = "�α��� ����";
    public static readonly string MENT_SUCCESS_LOGIN = "�α��� ����";
    public static readonly string MENT_SUCCESS_REGISTRETION = "ȸ������ ����";
    public static readonly string MENT_FAIELD_REGISTRETION = "ȸ������ ����";
    public static readonly string MENT_BUTTON_HOME = "������ �����Ͻðڽ��ϱ�?";
    public static readonly string MENT_BUTTON_BUY = "������ �����Ͻðڽ��ϱ�?";

    public static readonly string MENT_GAME_ROUND = "{0} ����";
    public static readonly string MENT_GAME_ENEMY_NUMBER = "{0}/{1}";
    public static readonly string[] MENT_TOWER_ENHANCE_LEVEL = {
        "���ݷ� ����\n(Lv.{0})",
        "���ݼӵ� ����\n(Lv.{0})",
    };

    public static readonly string UPGRADE_ICON_PATH = "Sprite/UI/Icon{0}";

    public static readonly string MENT_PLUS_GOLD = "+{0}g";
    public static readonly string MENT_GOLD = "{0}g";

    public static readonly string[] MENT_SKILL_DESCRIPTION = {
        "���� �ð�\n���ݷ� ��\n���ݼӵ���\n�����մϴ�.\n(Lv.{0})",
        "����¸� ������\n��� ����\n������ŵ�ϴ�.\n(Lv.{0})",
        "���� ��ȸ�ϴ�\nȸ�����ٶ���\n����ŵ�ϴ�.\n(Lv.{0})",
    };

    public static readonly string[] UPGRADE_NAME = {
        "�Ǽ����� ��ġ",
        "Ȳ�� ����",
        "��Ϲ���",
        "ƨ��� ��",
        "Ǫ�� ����",
        "���� ��",
    };

    public static readonly string[] UPGRADE_DESCRIPTION = {
        "Ÿ�� ������ {0}%Ȯ���� 1���� ������ Ÿ���� �����մϴ�",
        "Ÿ�� ��ü�� {0}%Ȯ���� 1���� ������ Ÿ���� �����մϴ�",
        "���� ��� �ý����� ���������� �ر��մϴ�",
        "���� Ÿ�� Ÿ������ ������ {0}%Ȯ���� ƨ��ϴ�",
        "�ü� Ÿ�� Ÿ������ ������ {0}%Ȯ���� ���� �����մϴ�",
        "������ Ÿ�� Ÿ������ ���� ������ {0}% �����մϴ�",
    };

   
    public static readonly string[] TOWER_DESCRIPTION = {
        "��� �ɷ�ġ�� ���� ���� ������ �����Դϴ�",
        "�������� ������ �ѹ��� ���� �����Դϴ�",
        "�ſ� ���� ���ݼӵ��� ���� �����մϴ�",
        "�� ��Ÿ��� ���� �뷱���� ���� �����Դϴ�",
        "�ſ� ���� ���ݼӵ��� ���� ���� �����մϴ�",
        "�ſ� �������� ������ �ѹ��� �����Դϴ�",
        "���� ���¿� ���� ���ظ� �����ϴ�",
        "���� ������ ����� �����ϴ� �����Դϴ�",
        "�Ʊ��� ��ȭ�Ͽ� �������� ���̴� �����Դϴ�",
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
