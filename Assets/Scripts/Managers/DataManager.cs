using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;

/// <summary>
/// ���� �� ��� ������ ���� Ŭ����
/// </summary>
public class GameData {
    //�ֳʹ� ������
    public EnemyData EnemyDatas { get; private set; }
    //��ų ������
    public SkillData SkillDatas { get; private set; }
    //Ÿ�� ������
    public TowerData TowerDatas { get; private set; }
    //���ڽ� ������
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
/// �α��� ���� ���� Ŭ����
/// </summary>
public class LoginData {
    //�̸���
    public string Email { get; private set; }
    //��й�ȣ
    public string Password { get; private set; }

    public LoginData(string email, string password) {
        Email = email;
        Password = password;
    }
}

/// <summary>
/// ���� ������ ���� Ŭ����
/// </summary>
public class DataManager {
    //��� ���� ������
    public GameData GameData { get; set; }
    //��� ���� ������(Defind)
    public Define DefineData { get; set; }
    //������ ��ȣȭ�� ���� Ű
    public string Key { get; set; }

    /// <summary>
    /// ���ÿ� ���� ������ ����.
    /// ù ���� ���۽� ȣ��
    /// </summary>
    public void SaveData(object data) {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> { new Vector3Converter(), new ColorConverter() }
        });

        DecryptionSaveJson(Managers.Data.DefineData.TAG_GAME_DATA, jsonData);
    }

    /// <summary>
    /// �α��� ������ �ε�
    /// </summary>
    /// <returns></returns>
    public LoginData LoadLoginData() {
        LoginData logindata = DecryptionLoadJson<LoginData>(Managers.Data.DefineData.TAG_LOGIN_DATA);
        return logindata;
    }

    /// <summary>
    /// ���ÿ� �α��� ������ ����
    /// ù �α��ν� ȣ��
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public void SaveLoginData(string email, string password) {
        LoginData logindata = new LoginData(email, password);
        string jsonData = JsonConvert.SerializeObject(logindata);
        DecryptionSaveJson(Managers.Data.DefineData.TAG_LOGIN_DATA, jsonData);
    }

    

    /// <summary>
    /// Ư�� ������ ����
    /// </summary>
    /// <param name="name"></param>
    public bool DeleteData(string name) {
        string path = Path.Combine(Application.persistentDataPath, name);
        if(!File.Exists(path)) {
            Debug.Log("�����Ϸ��� �����Ͱ� �������� �ʽ��ϴ�");
            return false;
        }

        File.Delete(path);
        return true;
    }

    /// <summary>
    /// Json���� ����(��ȣȭ)
    /// Application.persistentDataPath�� ����
    /// </summary>
    /// <param name="name">���� �̸�</param>
    /// <param name="jsonData">������</param>
    public void DecryptionSaveJson(string name, string jsonData) {
        string towerData = AesEncryption.Encrypt(jsonData, Key);

        string Path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        FileStream stream = new FileStream(Path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(towerData);
        stream.Write(data, 0, data.Length);
        stream.Close();
        Debug.Log($"{Path}�� {name}�̸��� Json���� ���̺�");
    }

    /// <summary>
    /// JSON���� �ε�(��ȣȭ)
    /// </summary>
    /// <typeparam name="T">Ÿ��</typeparam>
    /// <param name="name">���� �̸�</param>
    /// Application.persistentDataPath�н����� �ε�
    /// <returns></returns>
    public T DecryptionLoadJson<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load ����. {Application.persistentDataPath}�� {name} �̸� ������ �����ϴ�.");
            return default;
        }

        string jsonData = File.ReadAllText(path);
        string json = AesEncryption.Decrypt(jsonData, Key);

        Debug.Log($"{Application.persistentDataPath}���� {name} �̸��� Json ���� �ε�");
        return JsonConvert.DeserializeObject<T>(json);
    }

    /// <summary>
    /// Json���� ����
    /// Application.persistentDataPath�� ����
    /// </summary>
    /// <param name="name">���� �̸�</param>
    /// <param name="jsonData">������</param>
    public void SaveJson(string name, string jsonData) {
        string Path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        FileStream stream = new FileStream(Path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
        Debug.Log($"{Path}�� {name}�̸��� Json���� ���̺�");
    }

    /// <summary>
    /// JSON���� �ε�
    /// </summary>
    /// <typeparam name="T">Ÿ��</typeparam>
    /// <param name="name">���� �̸�</param>
    /// Application.persistentDataPath�н����� �ε�
    /// <returns></returns>
    public T LoadJson<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load ����. {Application.persistentDataPath}�� {name} �̸� ������ �����ϴ�.");
            return default;
        }

        string jsonData = File.ReadAllText(path);

        Debug.Log($"{Application.persistentDataPath}���� {name} �̸��� Json ���� �ε�");
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    /// <summary>
    /// JSON���� �񵿱� �ε�(��ȣȭ)
    /// </summary>
    /// <typeparam name="T">Ÿ��</typeparam>
    /// <param name="name">���� �̸�</param>
    /// Application.persistentDataPath�н����� �񵿱� �ε�
    /// <returns></returns>
    public async Task<T> DecryptionLoadJsonAsync<T>(string name) {
        string path = string.Format("{0}/{1}.json", Application.persistentDataPath, name);
        if (!File.Exists(path)) {
            Debug.LogError($"Load ����. {Application.persistentDataPath}�� {name} �̸� ������ �����ϴ�.");
            return default;
        }

        string jsonData = await Task.Run(() => File.ReadAllText(path));
        string json = AesEncryption.Decrypt(jsonData, Key);

        Debug.Log($"{Application.persistentDataPath}���� {name} �̸��� Json ���� �ε�");
        return JsonConvert.DeserializeObject<T>(json);
    }

    /// <summary>
    /// Ư�� �н��� �ش� ������ �����ϴ��� Ȯ��
    /// </summary>
    /// <param name="path">Ȯ���� �н�</param>
    /// <returns></returns>
    public bool CheckPathFile(string path) {
        if (File.Exists(Path.Combine(Application.persistentDataPath, path))) {
            return true;
        }
        return false;
    }
}

/// <summary>
/// Vector3Ÿ���� ����ȭ/������ȭ �ϱ� ���� ������
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
/// COLORŸ���� ����ȭ/������ȭ �ϱ� ���� ������
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