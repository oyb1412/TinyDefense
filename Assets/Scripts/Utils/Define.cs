using Newtonsoft.Json;
using UnityEngine;

public class Define {
    #region Const

    public float ABILITY_ENEMY_MOVESPEED_MINUS;
    public float ABILITY_POISON_DEFAULT_DAMAGE;
    public float ABILITY_POISON_DEFAULT_TIME;
    public float ABILITY_STUN_DEFAULT_TIME;
    public float ABILITY_STUN_DEFAULT_CHANCE;
    public float ABILITY_PROJECTILE_DEFAULT_CHANCE;
    public float ABILITY_PROJECTILE_DEFAULT_DELAY;
    public float ABILITY_MANYENEMY_DAMAGE_UP;
    public float ABILITY_DEFAULT_CRITICAL_CHANCE;
    public float ABILITY_DEFAULT_CRITICAL_DAMAGE;
    public float ABILITY_DEFAULT_MISS_CHANCE;
    public float ABILITY_PLUS_TOWER_REWARD;

    public float SISTER_BUFF_RANGE;

    public float TOWER_MOVESPEED;

    public float TOWER_EXPLOSION_RADIUS;

    public float SKILL_TORNADO_RANGE;

    public int ABILITY_GETGOLD;
    public int MINUS_TOWER_CREATE_COST;
    public float ABILITY_PLUS_ENEMY_REWARD;

    public string[] ABILITY_NAME;

    public string[] ABILITY_DESCRIPTION;

    public string[] ABILITY_SPRITE_PATH;

    public int CELL_COUNT;

    public int BUILDEFFECT_POOL_COUNT;

    public int ABILITY_REWARD_ROUND;
    public int TOWER_CREATE_COST;
    public int ROUND_SPAWN_COUNT;
    public int GAME_START_GOLD;

    public int MAX_ENEMY_TYPE;
    public int MAX_EMAIL_LENGTH;
    public int MAX_PASSWORD_LENGTH;

    public int ABILITY_ATTACK_COUNT;
    public int ABILITY_ENEMY_COUNT;
    public int ABILITY_TOWER_COUNT;

    public int ENEMY_SEARCH_MAX_COUNT;
    public int ENEMY_MAX_COUNT;
    public int SFX_DEFAULT_CHANNELS;

    public float ENEMY_DEFAULT_SCALE;
    public float DEBUFF_DAMAGE_DEFAULT_TICK;

    public float PERMISSION_RANGE;
    public float PROJECTILE_PERMISSION_RANGE;
    public float PROJECTILE_VELOCITY;
    public float PROJECTILE_DESTROY_TIME;
    public float PROJECTILE_ROTATION_DELAY;
    public float GAMELEVEL_UP_TICK;
    public float FIRST_GAMELEVEL_UP_TICK;
    public float ENEMY_SPAWN_DELAY;

    public float MOUSE_CLICK_RANGE;

    public float TOWER_RANGE_ROTATE_SPEED;

    public float TOWER_RANGE;


    public float BGM_DEFAULT_VOLUME;
    public float SFX_DEFAULT_VOLUME;

    public float FADE_TIME;
    public float PRESS_TIME;

    public string TAG_DEAD;
    public string TAG_LOADING_SLIDER;
    public string TAG_TOWER;
    public string TAG_GROUND;
    public string TAG_RUN;
    public string TAG_STUN;
    public string TAG_ATTACK;
    public string TAG_MOVEMENT;
    public string TAG_Idle;
    public string TAG_ENEMY;
    public string TAG_PORTAL;
    public string TAG_AUTOCREATE_BUTTON;
    public string ENEMY_MOVE_PATH;
    public string TAG_LOGIN_DATA;
    public string TAG_SCORE_DATA;
    public string TAG_ENEMY_DATA;
    public string TAG_SKILL_DATA;
    public string TAG_TOWER_DATA;
    public string TAG_ENHANCE_DATA;
    public string TAG_GAME_DATA_JSON;
    public string TAG_GAME_DATA;
    public string TAG_SCORE;
    public string TAG_KEY_DATA;
    public string TAG_DATA_KEY;
    public string TAG_KEY_DOCUMENT;

    public string[] TOWER_NAME;

    public string OBJECT_REWARD_PATH;
    public string MENT_CREATE_COST;
    public string MENT_SELL_REWARD;
    public string MENT_TOWER_LEVEL;
    public string MENT_TOWER_KILL;
    public string MENT_TOWER_DAMAGE;
    public string MENT_TOWER_DELAY;
    public string MENT_TOWER_RANGE;

    public string MENT_MAX_DELAY;

    public string SKILL_TORNADO_PATH;
    public string SPRITE_BUTTON_DEFAULT_SPEED;
    public string SPRITE_BUTTON_FASE_SPEED;
    public string EFFECT_TOWER_BUILD;

    public string SPRITE_OTHER_ICON;
    public string SPRITE_TOP_ICON;

    public string RANKING_SCORE;

    public string TILE_PATH;
    public string ARROWHEAD_PATH;

    public string MENT_BUTTON_SAVE;
    public string MENT_FAIELD_LOGIN;
    public string MENT_SUCCESS_LOGIN;
    public string MENT_SUCCESS_REGISTRETION;
    public string MENT_FAIELD_REGISTRETION;
    public string MENT_BUTTON_HOME;
    public string MENT_BUTTON_BUY;

    public string MENT_GAME_ROUND;
    public string MENT_GAME_ENEMY_NUMBER;
    public string[] MENT_TOWER_ENHANCE_LEVEL;

    public string UPGRADE_ICON_PATH;

    public string MENT_PLUS_GOLD;
    public string MENT_GOLD;

    public string[] MENT_SKILL_DESCRIPTION;

    public string[] UPGRADE_NAME;

    public string[] UPGRADE_DESCRIPTION;


    public string[] TOWER_DESCRIPTION;

    public string ENEMY_PREFAB_PATH;
    public int ENEMY_MAX_LEVEL;


    public int ENHANCE_MAXLEVEL;
    public int SKILL_MAX_LEVEL;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 ARROWHEAD_SCALE;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 SKILL_TORNADO_DEFAULT_POSITION;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 DEFAULT_CREATE_POSITION;

    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 TOWER_CREATE_POSITION;

    [JsonConverter(typeof(ColorConverter))]
    public Color COLOR_ORANGE;

    [JsonConverter(typeof(ColorConverter))]
    public Color COLOR_NOT;

    [JsonConverter(typeof(ColorConverter))]
    public Color[] COLOR_TOWERLEVEL ;



    public float SKILL_TORNADO_MOVESPEED;

    public string[] TOWER_PATH;

    public string[] TOWER_PREFAB_PATH;

    public string[] PROJECTILE_PATH;

    public string[] PROJECTILE_EXPLOSION_PATH;

    public int TOWER_MAXLEVEL;

    public string TOWERICON_SPRITE_PATH;

    public string[] BGM_PATH;

    public string[] SFX_PATH;

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
        Main,
        Ingame,
        Count,
    }

    public enum SFXType {
        GameStartButton,
        SelectUIButton,
        DeSelectUIButton,
        SelectTowerUIButton,
        SelectTowerAndCell,
        GetGold,
        FireProjectile,
        HitProjectile,
        PowerUpSkill,
        StunSkill,
        TornadoSkill,
        FireExplosion,
        IceExplosion,
        GameOver,
        Count,
    }

    public enum BuffType {
        AttackDamageUp,
        AttackDelayDown,
        Count,
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
    public enum SceneType {
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
        public AbilityValue(AbilityType type, Define define) {
            Type = type;
            Name = define.ABILITY_NAME[(int)Type];
            Description = define.ABILITY_DESCRIPTION[(int)Type];
            Sprite = Resources.Load<Sprite>(define.ABILITY_SPRITE_PATH[(int)Type]);
        }
    }
    #endregion
}