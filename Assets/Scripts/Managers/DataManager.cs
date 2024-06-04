using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.IO.Directory;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using UnityEngine.Events;

public class GameData {
    public EnemyData EnemysLevelDatas { get; private set; }
    public SkillData SkillsLevelDatas { get; private set; }
    public TowerData TowersLevelDatas { get; private set; }
    public EnhanceData EnhancesLevelDatas { get; private set; }

    public GameData(EnemyData enemyData, SkillData skillData,
        TowerData towerData, EnhanceData enhanceData) {
        EnemysLevelDatas = enemyData;
        SkillsLevelDatas = skillData;
        TowersLevelDatas = towerData;
        EnhancesLevelDatas = enhanceData;
    }

    public GameData() 
    {
        EnemysLevelDatas = new EnemyData();
        SkillsLevelDatas = new SkillData();
        TowersLevelDatas = new TowerData();
        EnhancesLevelDatas = new EnhanceData();
    }
}

public class LoginData {
    public string Email { get; private set; }
    public string Password { get; private set; }

    public LoginData(string email, string password) {
        Email = email;
        Password = password;
    }
}

public class DataManager {
    private float minLoadingTime = 3f;

    public GameData GameData { get; set; }

    public Action<float> LoadingAction;

    public string Key { get;  set; }

    public bool CheckPathFile(string path) {
        if (File.Exists(Path.Combine(Application.persistentDataPath, path))) {
            return true;
        }
        return false;
    }

    public void SaveData() {
        string jsonData = JsonConvert.SerializeObject(GameData, Formatting.Indented, new JsonSerializerSettings {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> { new Vector3Converter(), new ColorConverter() }
        });

        SaveJson("GameData", jsonData);
    }

    public void LoadGameData(UnityAction callBack) {
        if(GameData == null) {
            GameData = new GameData();
            Managers.Instance.StartCoroutine(LoadGameDataAndStartPoolAsync(callBack));
        }
    }

    private IEnumerator LoadGameDataAndStartPoolAsync(UnityAction callback) {
        // JSON 파일 비동기로 로드
        Task<GameData> loadJsonTask = LoadJsonAsync<GameData>("GameData");

        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();

        float startTime = Time.time;

        while (!loadJsonTask.IsCompleted) {
            yield return null;
        }

        GameData = loadJsonTask.Result;

        float elapsedTime = Time.time - startTime;

        while (elapsedTime < minLoadingTime) {
            elapsedTime = Time.time - startTime;
            float progress = Mathf.Clamp01(elapsedTime / minLoadingTime);
            LoadingAction?.Invoke(progress);
            yield return null;
        }

        LoadingAction?.Invoke(1f);

        callback?.Invoke();
    }

    public LoginData LoadLoginData() {
        LoginData logindata = LoadJson<LoginData>(Define.TAG_LOGIN_DATA);
        return logindata;
    }

    public void SaveLoginData(string email, string password) {
        LoginData logindata = new LoginData(email, password);
        string jsonData = JsonConvert.SerializeObject(logindata);
        SaveJson(Define.TAG_LOGIN_DATA, jsonData);
    }

    public void SaveJson(string name, string jsonData) {
        string towerData = AesEncryption.Encrypt(jsonData, Key);

        string Path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        FileStream stream = new FileStream(Path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(towerData);
        stream.Write(data, 0, data.Length);
        stream.Close();
        Debug.Log($"{Path}에 {name}이름의 Json파일 세이브");
    }

    public T LoadJson<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load 실패. {Application.persistentDataPath}에 {name} 이름 파일이 없습니다.");
            return default;
        }

        string jsonData = File.ReadAllText(path);
        string json = AesEncryption.Decrypt(jsonData, Key);

        Debug.Log($"{Application.persistentDataPath}에서 {name} 이름의 Json 파일 로드");
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T> LoadJsonAsync<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load 실패. {Application.persistentDataPath}에 {name} 이름 파일이 없습니다.");
            return default;
        }

        string jsonData = await Task.Run(() => File.ReadAllText(path));
        string json = AesEncryption.Decrypt(jsonData, Key);

        Debug.Log($"{Application.persistentDataPath}에서 {name} 이름의 Json 파일 로드");
        return JsonConvert.DeserializeObject<T>(json);
    }
}

public class Vector3Converter : JsonConverter {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        Vector3 vector = (Vector3)value;
        JObject obj = new JObject
        {
            { "x", vector.x },
            { "y", vector.y },
            { "z", vector.z }
        };
        obj.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer) {
        JObject obj = JObject.Load(reader);
        float x = (float)obj["x"];
        float y = (float)obj["y"];
        float z = (float)obj["z"];
        return new Vector3(x, y, z);
    }

    public override bool CanConvert(System.Type objectType) {
        return objectType == typeof(Vector3);
    }
}

public class ColorConverter : JsonConverter {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        if (value is Color color) {
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            writer.WriteValue(color.r);
            writer.WritePropertyName("g");
            writer.WriteValue(color.g);
            writer.WritePropertyName("b");
            writer.WriteValue(color.b);
            writer.WritePropertyName("a");
            writer.WriteValue(color.a);
            writer.WriteEndObject();
        } else if (value is Color[] colors) {
            writer.WriteStartArray();
            foreach (var c in colors) {
                writer.WriteStartObject();
                writer.WritePropertyName("r");
                writer.WriteValue(c.r);
                writer.WritePropertyName("g");
                writer.WriteValue(c.g);
                writer.WritePropertyName("b");
                writer.WriteValue(c.b);
                writer.WritePropertyName("a");
                writer.WriteValue(c.a);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
        if (objectType == typeof(Color[])) {
            JArray array = JArray.Load(reader);
            var colors = new List<Color>();
            foreach (var item in array) {
                colors.Add(item.ToObject<Color>(serializer));
            }
            return colors.ToArray();
        } else {
            JObject obj = JObject.Load(reader);
            return new Color(
                obj["r"].Value<float>(),
                obj["g"].Value<float>(),
                obj["b"].Value<float>(),
                obj["a"].Value<float>()
            );
        }
    }

    public override bool CanConvert(Type objectType) {
        return objectType == typeof(Color) || objectType == typeof(Color[]);
    }
}