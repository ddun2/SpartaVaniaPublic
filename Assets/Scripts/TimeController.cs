using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float timeScaleValue = 1f;

    void Update()
    {
        // timeScale 값을 설정한 값으로 변경
        Time.timeScale = timeScaleValue;
    }
}