using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainThreadRunner : MonoBehaviour
{
	private static MainThreadRunner runner;
	private static readonly List<System.Action> callbacks = new();

	public static void Run(System.Action callback)
	{
		EnsureInstance();
		lock (callbacks)
		{
			callbacks.Add(callback);
		}
	}
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void EnsureInstance()
	{
		if (runner == null)
		{
			GameObject go = new GameObject("MainThreadRunner");
			runner = go.AddComponent<MainThreadRunner>();
			Object.DontDestroyOnLoad(go);
		}
	}

	// Update is called once per frame
	void Update()
	{
		List<System.Action> callbacksCopy = null;
		lock (callbacks)
		{
			if (callbacks.Count > 0)
			{
				callbacksCopy = new(callbacks);
				callbacks.Clear();
			}
		}
		if (callbacksCopy != null)
		{
			foreach (var callback in callbacksCopy)
			{
				try
				{
					callback?.Invoke();
				}
				catch (System.Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}
	}
}

public static class MainThreadRunnerExtensions
{
	public static void ContinueWithOnMainThread(this Task task, System.Action<Task> callback)
	{
		task.ContinueWith(task =>
		{
			MainThreadRunner.Run(() =>
			{
				callback(task);
			});
		});
	}
	public static void ContinueWithOnMainThread<T>(this Task<T> task, System.Action<Task<T>> callback)
	{
		task.ContinueWith(task =>
		{
			MainThreadRunner.Run(() =>
			{
				callback(task);
			});
		});
	}
}
