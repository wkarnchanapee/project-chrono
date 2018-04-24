using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{
}
[System.Serializable]
public class StringEvent : UnityEvent<string>
{
}
[System.Serializable]
public class BoolEvent : UnityEvent<bool>
{
}
[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}
[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{
}

