using System.Collections;
using UnityEngine;

public static class DelayedMethods
{
    public static IEnumerator DelayedMethod (System.Action method, float wait = 0f)
    {
        yield return new WaitForSeconds(wait);
        method.Invoke();
    }
}
