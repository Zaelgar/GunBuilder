using System.Collections.Generic;
using UnityEngine;

static public class ServiceLocator
{
	static private readonly Dictionary<System.Type, object> m_systems = new Dictionary<System.Type, object>();

	static public T Register<T>(object target)
	{
		if (m_systems.ContainsKey(typeof(T)))
		{
			var name = Get<T>().ToString();
			if (name == "null")
			{
				m_systems.Remove(typeof(T));
				m_systems.Add(typeof(T), target);
			}
			else
				Debug.Log("There is already a type of : " + typeof(T) + " that exists");
		}
		else
		{
			Debug.Log("Registering " + typeof(T));
			m_systems.Add(typeof(T), target);
		}
		return (T)target;
	}

	static public bool Deregister<T>()
	{
		if (m_systems.ContainsKey(typeof(T)))
		{
			return m_systems.Remove(typeof(T));
		}
		else
		{
			Debug.Log("There isn't a type of : " + typeof(T) + " that exists");
			return false;
		}
	}

	static public T Get<T>()
	{
		object ret = null;
		m_systems.TryGetValue(typeof(T), out ret);
		if (ret == null)
		{
			Debug.Log("Could not find [" + (typeof(T)) + "] as a registered system");
		}
		return (T)ret;
	}

	static public bool Contains<T>()
	{
		return (m_systems.ContainsKey(typeof(T)));
	}
}