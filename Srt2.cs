using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System.IO;


public class Srt2 : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI previousSubtitleText;
    public Button startButton, stopButton, nextButton, previousButton;
    public Button[] srtFileButtons;
    private string[] srtFilePaths = {
    Path.Combine(Application.streamingAssetsPath, "実験A.srt"),
    Path.Combine(Application.streamingAssetsPath, "実験B.srt"),
    Path.Combine(Application.streamingAssetsPath, "実験C.srt")};

    private string[] lines;
    private int currentSubtitleIndex = 0;
    private bool isPlaying = false;

    void Start()
    {
        SetupButtons();
        LoadSrtFile(srtFilePaths[0]);
    }

    void SetupButtons()
    {
        startButton.onClick.AddListener(StartSubtitles);
        stopButton.onClick.AddListener(StopSubtitles);
        nextButton.onClick.AddListener(() => ChangeSubtitle(4));
        previousButton.onClick.AddListener(() => ChangeSubtitle(-4));
        
        for (int i = 0; i < srtFileButtons.Length; i++)
        {
            int index = i; // Avoid closure problem
            srtFileButtons[i].onClick.AddListener(() => LoadSrtFile(srtFilePaths[index]));
        }
    }

    void LoadSrtFile(string filePath)
    {
        StartCoroutine(LoadSrtFileCoroutine(filePath));
    }


    IEnumerator LoadSrtFileCoroutine(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(filePath))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
            else
            {
                lines = www.downloadHandler.text.Split('\n');
                currentSubtitleIndex = 0;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartSubtitles();
    }

    public void StartSubtitles()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            StartCoroutine(DisplaySubtitles());
        }
    }

    public void StopSubtitles()
    {
        isPlaying = false;
        StopAllCoroutines();
    }

    private void ChangeSubtitle(int step)
    {
        if ((currentSubtitleIndex >= 0 && currentSubtitleIndex < lines.Length - 4) || step < 0)
        {
            currentSubtitleIndex = Mathf.Clamp(currentSubtitleIndex + step, 0, lines.Length - 4);
            if (isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(DisplaySubtitles());
            }
        }
    }

    private IEnumerator DisplaySubtitles()
    {
        int index = currentSubtitleIndex;
        while (isPlaying && index < lines.Length)
        {
            UpdateSubtitleText(index);
            index += 4;
            yield return new WaitForSeconds(CalculateWaitTime(index));
        }
    }

    private void UpdateSubtitleText(int index)
    {
        if (index >= 0 && index < lines.Length)
        {
            previousSubtitleText.text = index >= 2 ? lines[index - 2] : "";
            textMeshPro.text = lines[index + 2];
        }
    }

    private float CalculateWaitTime(int index)
    {
        if (index >= 0 && index < lines.Length - 1)
        {
            var times = lines[index + 1].Split(new string[] { " --> " }, StringSplitOptions.None);
            var startTimes = times[0].Split(':');
            var endTimes = times[1].Split(':');
            var startTime = TimeSpan.Parse(startTimes[0] + ":" + startTimes[1] + ":" + startTimes[2].Replace(',', '.'));
            var endTime = TimeSpan.Parse(endTimes[0] + ":" + endTimes[1] + ":" + endTimes[2].Replace(',', '.'));
            return (float)(endTime - startTime).TotalSeconds;
        }
        return 0f;
    }
}
