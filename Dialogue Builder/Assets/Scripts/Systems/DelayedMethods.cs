using System.Collections;
using UnityEngine;

public static class DelayedMethods
{
    public static IEnumerator DelayedMethod(System.Action method, float wait = 0f)
    {
        yield return new WaitForSeconds(wait);
        method.Invoke();
    }
}

public static class DelayedMethods<T>
{
    public static IEnumerator DelayedMethod(System.Action<T> method, T arg, float wait = 0f)
    {
        yield return new WaitForSeconds(wait);
        method.Invoke(arg);
    }
}

public static class DelayedMethods<T, V>
{
    public static IEnumerator DelayedMethod(System.Action<T, V> method, T arg1, V arg2, float wait = 0f)
    {
        yield return new WaitForSeconds(wait);
        method.Invoke(arg1, arg2);
    }
}
