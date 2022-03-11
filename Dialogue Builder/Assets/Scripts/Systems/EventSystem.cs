using System.Collections.Generic;

public static class EventSystem
{
    public static Dictionary<EventTypes, System.Action> methods = new Dictionary<EventTypes, System.Action>();

    public static void AddListener(EventTypes type, System.Action method)
    {
        if (!methods.ContainsKey(type))
        {
            methods.Add(type, null);
        }

        methods[type] += (method);
    }

    public static void RemoveListener(EventTypes type, System.Action method)
    {
        if (methods.ContainsKey(type))
        {
            methods[type] -= method;
        }
    }

    public static void InvokeEvent(EventTypes type)
    {
        if (methods.ContainsKey(type))
        {
            methods[type]?.Invoke();
        }
    }
}

public static class EventSystem<T>
{
    public static Dictionary<EventTypes, System.Action<T>> methods = new Dictionary<EventTypes, System.Action<T>>();

    public static void AddListener(EventTypes type, System.Action<T> method)
    {
        if (!methods.ContainsKey(type))
        {
            methods.Add(type, null);
        }

        methods[type] += (method);
    }

    public static void RemoveListener(EventTypes type, System.Action<T> method)
    {
        if (methods.ContainsKey(type))
        {
            methods[type] -= method;
        }
    }

    public static void InvokeEvent(EventTypes type, T arg)
    {
        if (methods.ContainsKey(type))
        {
            methods[type]?.Invoke(arg);
        }
    }
}
