using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Horas : MonoBehaviour
{
    [Header("Duración de los días y semana")]
    public float secondsPerGameDay = 480f;

    [Header("UI")]
    public TMP_Text horaText;

    private float gameTimeInSeconds = 0f;
    private float lastGameTimeInSeconds = 0f;

    void Awake() => DontDestroyOnLoad(this);

    void Start()
    {
        gameTimeInSeconds = 6 * 3600;
        lastGameTimeInSeconds = gameTimeInSeconds;
    }

    void Update()
    {

        // guarda prev para detectar cruces de 6:00
        float prev = lastGameTimeInSeconds;

        gameTimeInSeconds += Time.deltaTime * (24f * 60f * 60f / secondsPerGameDay);

        if (gameTimeInSeconds >= 24f * 60f * 60f)
            gameTimeInSeconds -= 24f * 60f * 60f;

        if (horaText != null) horaText.text = GetFormattedTime();

        lastGameTimeInSeconds = gameTimeInSeconds;
    }

    string GetFormattedTime()
    {
        int totalSeconds = (int)gameTimeInSeconds;
        int horas = totalSeconds / 3600;
        int minutos = (totalSeconds % 3600) / 60;
        return $"{horas:D2}:{minutos:D2}";
    }
}

