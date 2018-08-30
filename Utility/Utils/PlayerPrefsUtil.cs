﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken from sabresaurus's PlayerPrefsEditor code. https://github.com/sabresaurus/PlayerPrefsEditor
public static class PlayerPrefsUtil
{
    // Prefix to know if the string is a key.
    public const string KEY_PREFIX = "ENC-";

    /// <summary>
	/// Check if the string given is an encrypted key.
	/// </summary>
	public static bool IsEncryptedKey(string value)
    {
        return value.StartsWith(KEY_PREFIX);
    }

    /// <summary>
    /// Decrypts the passed-in key.
    /// </summary>
    public static string DecryptKey(string encryptedKey)
    {
        if (encryptedKey.StartsWith(KEY_PREFIX))
        {
            // Remove the prefix.
            string strippedKey = encryptedKey.Substring(KEY_PREFIX.Length);

            return EncryptionUtil.DecryptString(strippedKey);
        }
        else
        {
            Debug.Log("PlayerPrefsUtil : Key given doesn't contain proper data header.");
            return encryptedKey;
        }
    }

    #region Set

    /// <summary>
    /// Store a bool as a player pref (as an int).
    /// </summary>
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    /// <summary>
    /// Store a vector2 as a player pref (as floats).
    /// </summary>
    public static void SetVector2(string key, Vector2 value)
    {
        SetFloatArray(key, new float[] { value.x, value.y });
    }

    /// <summary>
    /// Store a vector2Int as a player pref (as ints).
    /// </summary>
    public static void SetVector2Int(string key, Vector2Int value)
    {
        SetIntArray(key, new int[] { value.x, value.y });
    }

    /// <summary>
    /// Store a vector3 as a player pref (as floats).
    /// </summary>
    public static void SetVector3(string key, Vector3 value)
    {
        SetFloatArray(key, new float[] { value.x, value.y, value.z });
    }

    /// <summary>
    /// Store a vector3Int as a player pref (as ints).
    /// </summary>
    public static void SetVector3Int(string key, Vector3Int value)
    {
        SetIntArray(key, new int[] { value.x, value.y, value.z });
    }

    /// <summary>
    /// Store a vector4 as a player pref (as floats).
    /// </summary>
    public static void SetVector4(string key, Vector4 value)
    {
        SetFloatArray(key, new float[] { value.x, value.y, value.z, value.w });
    }

    /// <summary>
    /// Store a color as a player pref (as an int).
    /// </summary>
    public static void SetColor(string key, Color value)
    {
        SetVector4(key, value);
    }

    /// <summary>
    /// Store an enum as a player pref (as a string).
    /// </summary>
    public static void SetEnum(string key, Enum value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }

    #endregion

    #region Set Encrypted

    /// <summary>
    /// Encrypted version of PlayerPrefs.SetFloat(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedFloat(string key, float value)
    {
        Encrypt(EncryptionUtil.EncryptString(key), EncryptionUtil.EncryptFloat(value));
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.SetInt(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedInt(string key, int value)
    {
        Encrypt(EncryptionUtil.EncryptString(key), EncryptionUtil.EncryptInt(value));
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.SetString(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedString(string key, string value)
    {
        Encrypt(EncryptionUtil.EncryptString(key), EncryptionUtil.EncryptString(value));
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.SetBool(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedBool(string key, bool value)
    {
        Encrypt(EncryptionUtil.EncryptString(key), EncryptionUtil.EncryptInt(value ? 1 : 0));
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector2(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector2(string key, Vector2 value)
    {
        SetEncryptedFloatArray(key, new float[] { value.x, value.y });
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector2Int(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector2Int(string key, Vector2Int value)
    {
        SetEncryptedIntArray(key, new int[] { value.x, value.y });
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector3(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector3(string key, Vector3 value)
    {
        SetEncryptedFloatArray(key, new float[] { value.x, value.y, value.z });
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector3Int(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector3Int(string key, Vector3Int value)
    {
        SetEncryptedIntArray(key, new int[] { value.x, value.y, value.z });
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector4(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector4(string key, Vector4 value)
    {
        SetEncryptedFloatArray(key, new float[] { value.x, value.y, value.z, value.w });
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetColor(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedColor(string key, Color value)
    {
        SetEncryptedVector4(key, value);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetEnum(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedEnum(string key, Enum value)
    {
        SetEncryptedString(key, value.ToString());
    }

    #endregion

    #region Set Array

    /// <summary>
    /// Just like PlayerPrefs.SetFloat(), but to store an array of item at once.
    /// </summary>
    public static void SetFloatArray(string key, float[] values)
    {
        // The key itself points at the data quantity.
        PlayerPrefs.SetInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            PlayerPrefs.SetFloat(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Just like PlayerPrefs.SetInt(), but to store an array of item at once.
    /// </summary>
    public static void SetIntArray(string key, int[] values)
    {
        // The key itself points at the data quantity.
        PlayerPrefs.SetInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            PlayerPrefs.SetInt(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Just like PlayerPrefs.SetString(), but to store an array of item at once.
    /// </summary>
    public static void SetStringArray(string key, string[] values)
    {
        // The key itself points at the data quantity.
        PlayerPrefs.SetInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            PlayerPrefs.SetString(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Just like PlayerPrefs.SetBool(), but to store an array of item at once, merged into 32bits ints.
    /// </summary>
    public static void SetBoolArray(string key, bool[] values)
    {
        int[] ints = BitwiseUtil.MergeBoolsToInt(values);

        // The key itself points at the bool quantity, not ints.
        PlayerPrefs.SetInt(key, values.Length);

        for (int i = 0; i < ints.Length; i++)
        {
            PlayerPrefs.SetInt(key + i.ToString(), ints[i]);
        }
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetVector2(), but to store an array of item at once.
    /// </summary>
    public static void SetVector2Array(string key, Vector2[] values)
    {
        float[] floats = new float[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            floats[i] = (i % 2 == 0 ? values[Mathf.FloorToInt(i / 2)].x : values[Mathf.FloorToInt(i / 2)].y);
        }

        SetFloatArray(key, floats);
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetVector2Int(), but to store an array of item at once.
    /// </summary>
    public static void SetVector2IntArray(string key, Vector2Int[] values)
    {
        int[] ints = new int[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            ints[i] = (i % 2 == 0 ? values[Mathf.FloorToInt(i / 2)].x : values[Mathf.FloorToInt(i / 2)].y);
        }

        SetIntArray(key, floats);
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetVector3(), but to store an array of item at once.
    /// </summary>
    public static void SetVector3Array(string key, Vector3[] values)
    {
        float[] floats = new float[values.Length * 3];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].x;
                    break;

                case 1:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].y;
                    break;

                case 2:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].z;
                    break;
            }
        }

        SetFloatArray(key, floats);
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetVector3Int(), but to store an array of item at once.
    /// </summary>
    public static void SetVector3IntArray(string key, Vector3Int[] values)
    {
        int[] ints = new int[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].x;
                    break;

                case 1:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].y;
                    break;

                case 2:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].z;
                    break;
            }
        }

        SetIntArray(key, floats);
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetVector4(), but to store an array of item at once.
    /// </summary>
    public static void SetVector4Array(string key, Vector4[] values)
    {
        float[] floats = new float[values.Length * 4];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 4)
            {
                case 0:
                    floats[i] = values[Mathf.FloorToInt(i / 4)].x;
                    break;

                case 1:
                    floats[i] = values[Mathf.FloorToInt(i / 4)].y;
                    break;

                case 2:
                    floats[i] = values[Mathf.FloorToInt(i / 4)].z;
                    break;

                case 3:
                    floats[i] = values[Mathf.FloorToInt(i / 4)].w;
                    break;
            }
        }

        SetFloatArray(key, floats);
    }

    /// <summary>
    /// Just like PlayerPrefsUtil.SetColor(), but to store an array of item at once, merged into 32bits ints.
    /// </summary>
    public static void SetColorArray(string key, Color[] values)
    {
        SetVector4Array(key, values);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetEnum(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEnumArray(string key, Enum[] values)
    {
        string[] strings = new string[values.Length];

        for(int i = 0; i < value.Length; i++)
        {
            strings[i] = values[i];
        }
        
        SetStringArray(key, strings);
    }

    #endregion

    #region Set Encrypted Array

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetFloatArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedFloatArray(string key, float[] values)
    {
        // The key itself points at the data quantity.
        SetEncryptedInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            SetEncryptedFloat(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetIntArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedIntArray(string key, int[] values)
    {
        // The key itself points at the data quantity.
        SetEncryptedInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            SetEncryptedInt(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetStringArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedStringArray(string key, string[] values)
    {
        // The key itself points at the data quantity.
        SetEncryptedInt(key, values.Length);

        for (int i = 0; i < values.Length; i++)
        {
            SetEncryptedString(key + i.ToString(), values[i]);
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetBoolArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedBoolArray(string key, bool[] values)
    {
        int[] ints = BitwiseUtil.MergeBoolsToInt(values);

        // The key itself points at the data quantity.
        SetEncryptedInt(key, values.Length);

        for (int i = 0; i < ints.Length; i++)
        {
            SetEncryptedInt(key + i.ToString(), ints[i]);
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector2Array(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector2Array(string key, Vector2[] values)
    {
        float[] floats = new float[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            floats[i] = (i % 2 == 0 ? values[Mathf.FloorToInt(i / 2)].x : values[Mathf.FloorToInt(i / 2)].y);
        }

        SetEncryptedFloatArray(key, floats);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector2IntArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector2IntArray(string key, Vector2Int[] values)
    {
        int[] ints = new int[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            ints[i] = (i % 2 == 0 ? values[Mathf.FloorToInt(i / 2)].x : values[Mathf.FloorToInt(i / 2)].y);
        }

        SetEncryptedIntArray(key, floats);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector3Array(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector3Array(string key, Vector3[] values)
    {
        float[] floats = new float[values.Length * 3];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].x;
                    break;

                case 1:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].y;
                    break;

                case 2:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].z;
                    break;
            }
        }

        SetEncryptedFloatArray(key, floats);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector3IntArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector3IntArray(string key, Vector3Int[] values)
    {
        int[] ints = new int[values.Length * 2];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].x;
                    break;

                case 1:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].y;
                    break;

                case 2:
                    ints[i] = values[Mathf.FloorToInt(i / 3)].z;
                    break;
            }
        }

        SetEncryptedIntArray(key, floats);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetVector4Array(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedVector4Array(string key, Vector4[] values)
    {
        float[] floats = new float[values.Length * 3];

        for (int i = 0; i < values.Length; i++)
        {
            switch (i % 3)
            {
                case 0:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].x;
                    break;

                case 1:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].y;
                    break;

                case 2:
                    floats[i] = values[Mathf.FloorToInt(i / 3)].z;
                    break;
            }
        }

        SetEncryptedFloatArray(key, floats);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetColorArray(), stored key and values are encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedColorArray(string key, Color[] values)
    {
        SetEncryptedVector4Array(key, values);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtil.SetEnum(), stored key and value is encrypted in player prefs.
    /// </summary>
    public static void SetEncryptedEnumArray(string key, Enum[] values)
    {
        string[] strings = new string[values.Length];

        for (int i = 0; i < value.Length; i++)
        {
            strings[i] = values[i];
        }

        SetEncryptedStringArray(key, strings);
    }

    #endregion


    #region Get

    /// <summary>
    /// A key is passed, returning a bool.
    /// </summary>
    public static bool GetBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(result) == 1;
    }

    /// <summary>
    /// A key is passed, returning a vector2.
    /// </summary>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = new Vector2())
    {
        float[] floats = GetFloatArray(key);

        if (floats.Length < 2)
        {
            return defaultValue;
        }

        return new Vector2(floats[0], floats[1]);
    }

    /// <summary>
    /// A key is passed, returning a vector2Int.
    /// </summary>
    public static Vector2Int GetVector2Int(string key, Vector2Int defaultValue = new Vector2Int())
    {
        int[] ints = GetIntArray(key);

        if (ints.Length < 2)
        {
            return defaultValue;
        }

        return new Vector2Int(ints[0], ints[1]);
    }

    /// <summary>
    /// A key is passed, returning a vector3.
    /// </summary>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = new Vector3())
    {
        float[] floats = GetFloatArray(key);

        if (floats.Length < 3)
        {
            return defaultValue;
        }

        return new Vector2(floats[0], floats[1], floats[2]);
    }

    /// <summary>
    /// A key is passed, returning a vector3Int.
    /// </summary>
    public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = new Vector3Int())
    {
        int[] ints = GetIntArray(key);

        if (ints.Length < 3)
        {
            return defaultValue;
        }

        return new Vector3Int(ints[0], ints[1], ints[2]);
    }

    /// <summary>
    /// A key is passed, returning a vector4.
    /// </summary>
    public static Vector4 GetVector4(string key, Vector4 defaultValue = new Vector4())
    {
        float[] floats = GetFloatArray(key);

        if (floats.Length < 4)
        {
            return defaultValue;
        }

        return new Vector4(floats[0], floats[1], floats[2], floats[3]);
    }

    /// <summary>
    /// A key is passed, returning a Color.
    /// </summary>
    public static Color GetColor(string key, Color defaultValue = new Color())
    {
        return GetVector4(key, defaultValue);
    }

    /// <summary>
    /// A key is passed, returning an Enum of the passed type.
    /// </summary>
    public static T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct
    {
        string stringValue = PlayerPrefs.GetString(key);

        if (!string.IsNullOrEmpty(stringValue))
        {
            // Existing value, so parse it using the supplied generic type and cast before returning it
            return (T)Enum.Parse(typeof(T), stringValue);
        }
        else
        {
            // No player pref for this, just return default. If no default is supplied this will be the enum's default.
            return defaultValue;
        }
    }

    #endregion

    #region Get Encrypted

    /// <summary>
    /// Encrypted version of PlayerPrefs.GetFloat(), an unencrypted key is passed and the value is returned decrypted.
    /// </summary>
    public static float GetEncryptedFloat(string key, float defaultValue = 0.0f)
    {
        string result;

        if (Decrypt(key, ref result))
        {
            return EncryptionUtil.DecryptFloat(result);
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.GetInt(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static int GetEncryptedInt(string key, int defaultValue = 0)
    {
        string result;

        if (Decrypt(key, ref result))
        {
            return EncryptionUtil.DecryptInt(result);
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.GetString(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static string GetEncryptedString(string key, string defaultValue = "")
    {
        string result;

        if (Decrypt(key, ref result))
        {
            return EncryptionUtil.DecryptString(result);
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetBool(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static bool GetEncryptedBool(string key, bool defaultValue = false)
    {
        string result;

        if (Decrypt(key, ref result))
        {
            return EncryptionUtil.DecryptInt(result) == 1;
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetVector2(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Vector2 GetEncryptedVector2(string key, Vector2 defaultValue = new Vector2())
    {
        float[] floats = GetEncryptedFloatArray(key);

        if (floats.Length < 2)
        {
            return defaultValue;
        }

        return new Vector2(floats[0], floats[1]);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetVector2Int(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Vector2Int GetEncryptedVector2Int(string key, Vector2Int defaultValue = new Vector2Int())
    {
        int[] ints = GetEncryptedIntArray(key);

        if (ints.Length < 2)
        {
            return defaultValue;
        }

        return new Vector2Int(ints[0], ints[1]);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetVector3(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Vector3 GetEncryptedVector3(string key, Vector3 defaultValue = new Vector3())
    {
        float[] floats = GetEncryptedFloatArray(key);

        if (floats.Length < 3)
        {
            return defaultValue;
        }

        return new Vector2(floats[0], floats[1], floats[2]);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetVector3Int(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Vector3Int GetEncryptedVector3Int(string key, Vector3Int defaultValue = new Vector3Int())
    {
        int[] ints = GetEncryptedIntArray(key);

        if (ints.Length < 3)
        {
            return defaultValue;
        }

        return new Vector3Int(ints[0], ints[1], ints[2]);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetVector4(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Vector4 GetEncryptedVector4(string key, Vector4 defaultValue = new Vector4())
    {
        float[] floats = GetEncryptedFloatArray(key);

        if (floats.Length < 4)
        {
            return defaultValue;
        }

        return new Vector4(floats[0], floats[1], floats[2], floats[3]);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetColor(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static Color GetEncryptedColor(string key, Color defaultValue = new Color())
    {
        return GetEncryptedVector4(key, defaultValue);
    }

    /// <summary>
    /// Encrypted version of PlayerPrefsUtils.GetEnum(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static T GetEncryptedEnum<T>(string key, T defaultValue = default(T)) where T : struct
    {
        string stringValue = PlayerPrefs.GetEncryptedString(key);

        if (!string.IsNullOrEmpty(stringValue))
        {
            // Existing value, so parse it using the supplied generic type and cast before returning it
            return (T)Enum.Parse(typeof(T), stringValue);
        }
        else
        {
            // No player pref for this, just return default. If no default is supplied this will be the enum's default.
            return defaultValue;
        }
    }

    #endregion

    #region Get Array

    public static float[] GetFloatArray(string key, float defaultValue = 0.0f)
    {
        int count = PlayerPrefs.GetInt(key, 0);

        float[] result = new float[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = PlayerPrefs.GetFloat(key + i.ToString(), defaultValue);
        }

        return result;
    }

    #endregion

    #region Get Encrypted Array



    #endregion


    #region GetFloat





    public static float[] GetEncryptedFloatArray(string key, float defaultValue = 0.0f)
    {
        int count = GetEncryptedInt(key, 0);

        float[] result = new float[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = GetEncryptedFloat(key + i.ToString(), defaultValue);
        }

        return result;
    }

    #endregion

    #region GetInt



    public static int[] GetIntArray(string key, int defaultValue = 0)
    {
        int count = PlayerPrefs.GetInt(key, 0);

        int[] result = new int[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = PlayerPrefs.GetInt(key + i.ToString(), defaultValue);
        }

        return result;
    }

    public static int[] GetEncryptedIntArray(string key, int defaultValue = 0)
    {
        int count = GetEncryptedInt(key, 0);

        int[] result = new int[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = GetEncryptedInt(key + i.ToString(), defaultValue);
        }

        return result;
    }

    #endregion

    #region GetString



    public static string[] GetStringArray(string key, string defaultValue = 0)
    {
        int count = PlayerPrefs.GetInt(key, 0);

        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = PlayerPrefs.GetString(key + i.ToString(), defaultValue);
        }

        return result;
    }

    public static string[] GetEncryptedStringArray(string key, string defaultValue = 0)
    {
        int count = GetEncryptedInt(key, 0);

        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = GetEncryptedString(key + i.ToString(), defaultValue);
        }

        return result;
    }

    #endregion

    #region GetBool

    /// <summary>
    /// Helper method to retrieve a bool from PlayerPrefs (stored as an int)
    /// </summary>
    public static bool GetBool(string key, bool defaultValue = false)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
        else
        {
            // No existing player pref value, so return defaultValue instead.
            return defaultValue;
        }
    }



    public static bool[] GetBoolArray(string key, bool defaultValue = false)
    {
        int count = PlayerPrefs.GetInt(key, 0);

        bool[] result = new bool[count];

        for (int i = 0; i < Mathf.FloorToInt(values.Length / 32) + 1; i++)
        {
            int flag = PlayerPrefs.GetInt(key + i.ToString(), defaultValue ? int.MaxValue : 0);
            bool[] flags = BitwiseUtil.SplitIntIntoBools(flag);

            for(int j = 0; j < Mathf.Min(32, count - (32 * i)); j++)
            {
                result[j + (32 * i)] = flags[j];
            }
        }

        return result;
    }

    public static bool[] GetEncryptedBoolArray(string key, bool defaultValue = false)
    {
        int count = GetEncryptedInt(key, 0);

        bool[] result = new bool[count];

        for (int i = 0; i < Mathf.FloorToInt(values.Length / 32) + 1; i++)
        {
            int flag = GetEncryptedInt(key + i.ToString(), defaultValue ? int.MaxValue : 0);
            bool[] flags = BitwiseUtil.SplitIntIntoBools(flag);

            for (int j = 0; j < Mathf.Min(32, count - (32 * i)); j++)
            {
                result[j + (32 * i)] = flags[j];
            }
        }

        return result;
    }

    #endregion





    private static void Encrypt(string key, string value)
    {
        PlayerPrefs.SetString(KEY_PREFIX + encryptedKey, value);
    }

    private static bool Decrypt(string key, ref string result)
    {
        string encryptedKey = EncryptionUtil.EncryptString(key);
        string fetchedString = PlayerPrefs.GetString(encryptedKey);

        if (!string.IsNullOrEmpty(fetchedString))
        {
            result = fetchedString;
            return true;
        }
        else
        {
            return false;
        }
    }
}
