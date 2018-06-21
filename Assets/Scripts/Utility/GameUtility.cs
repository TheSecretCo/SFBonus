using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtility : MonoBehaviour
{
    public static int CountDigits (int number)
    {
        // In case of negative numbers
        number = Math.Abs(number);
        if (number >= 10)
        {
            return CountDigits(number / 10) + 1;
        }
        return 1;
    }

    public static IEnumerator LoadSceneRoutine (string _sceneName, LoadSceneMode _mode = LoadSceneMode.Single, Action _callback = null)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            //Debug.LogError(asyncOperation.progress);
            yield return null;
        }

        //if (_callback != null)
        //{
        //    _callback();
        //}

        //if (_mode.Equals(LoadSceneMode.Single))
        //{
        //    SceneManager.UnloadSceneAsync(activeScene);
        //}
    }

    public static string GetStreamingassetPath (string _path)
    {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        stringBuilder.Append(Application.dataPath + "/StreamingAssets/");

#if !UNITY_EDITOR
#if UNITY_IOS
        stringBuilder.Append(Application.dataPath + "/Raw/");
#endif
#if UNITY_ANDROID
        stringBuilder.Append("jar:file://" + Application.dataPath + "!/assets/");
#endif
#endif
        stringBuilder.Append(_path);
        return stringBuilder.ToString();
    }

    public static T DeserializeFromPath<T> (string _path)
    {

        using (var fileStream = File.Open(_path, FileMode.Open, FileAccess.Read))
        {
            T result = GameDevWare.Serialization.MsgPack.Deserialize<T>(fileStream);
            return result;
        }
    }

    public static T DeserializeFromByte<T> (byte[] _bytes)
    {
        MemoryStream stream = new MemoryStream(_bytes);
        T result = GameDevWare.Serialization.MsgPack.Deserialize<T>(stream);
        return result;
    }

    static bool isRotating;
    public static IEnumerator RotateAround (Transform _transform, Transform _target, Vector3 _rotateAxis, float _degrees, float _totalTime, Action _onRotateEnd = null)
    {
        //if (isRotating)
        //    yield break;

        //isRotating = true;

        var startRotation = _transform.rotation;
        var startPosition = _transform.position;
        _transform.RotateAround(_target.position, _rotateAxis, _degrees);
        var endRotation = _transform.rotation;
        var endPosition = _transform.position;
        _transform.rotation = startRotation;
        _transform.position = startPosition;

        var rate = _degrees / _totalTime;
        for (float i = 0.0f; i < _degrees; i += Time.deltaTime * rate)
        {
            yield return null;
            _transform.RotateAround(_target.position, _rotateAxis, Time.deltaTime * rate);
        }

        _transform.rotation = endRotation;
        _transform.position = endPosition;

        if (_onRotateEnd != null)
        {
            _onRotateEnd();
        }

        //isRotating = false;
    }

    public static GameObject GetProgressBarPrefab ()
    {
        return Resources.Load<GameObject>("UI/ProgressBar");
    }
}
