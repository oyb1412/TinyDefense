using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;

/// <summary>
/// 게임 내 모든 데이터 묶음 클래스
/// </summary>
public class GameData {
    //애너미 데이터
    public EnemyData EnemyDatas { get; private set; }
    //스킬 데이터
    public SkillData SkillDatas { get; private set; }
    //타워 데이터
    public TowerData TowerDatas { get; private set; }
    //인핸스 데이터
    public EnhanceData EnhanceDatas { get; private set; }

    public GameData() 
    {
        EnemyDatas = new EnemyData();
        SkillDatas = new SkillData();
        TowerDatas = new TowerData();
        EnhanceDatas = new EnhanceData();
    }
}

/// <summary>
/// 로그인 정보 관리 클래스
/// </summary>
public class LoginData {
    //이메일
    public string Email { get; private set; }
    //비밀번호
    public string Password { get; private set; }

    public LoginData(string email, string password) {
        Email = email;
        Password = password;
    }
}

/// <summary>
/// 각종 데이터 관리 클래스
/// </summary>
public class DataManager {
    //모든 게임 데이터
    public GameData GameData { get; set; }
    //모든 게임 데이터(Defind)
    public Define DefineData { get; set; }
    //데이터 암호화를 위한 키
    public string Key { get; set; }

    /// <summary>
    /// 로컬에 게임 데이터 저장.
    /// 첫 게임 시작시 호출
    /// </summary>
    public void SaveData(object data) {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> { new Vector3Converter(), new ColorConverter() }
        });

        DecryptionSaveJson(Managers.Data.DefineData.TAG_GAME_DATA, jsonData);
    }

    /// <summary>
    /// 로그인 데이터 로드
    /// </summary>
    /// <returns></returns>
    public LoginData LoadLoginData() {
        LoginData logindata = DecryptionLoadJson<LoginData>(Managers.Data.DefineData.TAG_LOGIN_DATA);
        return logindata;
    }

    /// <summary>
    /// 로컬에 로그인 데이터 저장
    /// 첫 로그인시 호출
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public void SaveLoginData(string email, string password) {
        LoginData logindata = new LoginData(email, password);
        string jsonData = JsonConvert.SerializeObject(logindata);
        DecryptionSaveJson(Managers.Data.DefineData.TAG_LOGIN_DATA, jsonData);
    }

    

    /// <summary>
    /// 특정 데이터 삭제
    /// </summary>
    /// <param name="name"></param>
    public bool DeleteData(string name) {
        string path = Path.Combine(Application.persistentDataPath, name);
        if(!File.Exists(path)) {
            Debug.Log("삭제하려는 데이터가 존재하지 않습니다");
            return false;
        }

        File.Delete(path);
        return true;
    }

    /// <summary>
    /// Json파일 저장(복호화)
    /// Application.persistentDataPath에 저장
    /// </summary>
    /// <param name="name">파일 이름</param>
    /// <param name="jsonData">데이터</param>
    public void DecryptionSaveJson(string name, string jsonData) {
        string towerData = AesEncryption.Encrypt(jsonData, Key);

        string Path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        FileStream stream = new FileStream(Path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(towerData);
        stream.Write(data, 0, data.Length);
        stream.Close();
        Debug.Log($"{Path}에 {name}이름의 Json파일 세이브");
    }

    /// <summary>
    /// JSON파일 로드(복호화)
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="name">파일 이름</param>
    /// Application.persistentDataPath패스에서 로드
    /// <returns></returns>
    public T DecryptionLoadJson<T>(string name) {
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

    /// <summary>
    /// Json파일 저장
    /// Application.persistentDataPath에 저장
    /// </summary>
    /// <param name="name">파일 이름</param>
    /// <param name="jsonData">데이터</param>
    public void SaveJson(string name, string jsonData) {
        string Path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        FileStream stream = new FileStream(Path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
        Debug.Log($"{Path}에 {name}이름의 Json파일 세이브");
    }

    /// <summary>
    /// JSON파일 로드
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="name">파일 이름</param>
    /// Application.persistentDataPath패스에서 로드
    /// <returns></returns>
    public T LoadJson<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load 실패. {Application.persistentDataPath}에 {name} 이름 파일이 없습니다.");
            return default;
        }

        string jsonData = File.ReadAllText(path);

        Debug.Log($"{Application.persistentDataPath}에서 {name} 이름의 Json 파일 로드");
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    /// <summary>
    /// JSON파일 비동기 로드(복호화)
    /// </summary>
    /// <typeparam name="T">타입</typeparam>
    /// <param name="name">파일 이름</param>
    /// Application.persistentDataPath패스에서 비동기 로드
    /// <returns></returns>
    public async Task<T> DecryptionLoadJsonAsync<T>(string name) {
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

    /// <summary>
    /// 특정 패스에 해당 파일이 존재하는지 확인
    /// </summary>
    /// <param name="path">확인할 패스</param>
    /// <returns></returns>
    public bool CheckPathFile(string path) {
        if (File.Exists(Path.Combine(Application.persistentDataPath, path))) {
            return true;
        }
        return false;
    }
}

/// <summary>
/// Vector3타입을 직렬화/역직렬화 하기 위한 컨버터
/// </summary>
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

/// <summary>
/// COLOR타입을 직렬화/역직렬화 하기 위한 컨버터
/// </summary>
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