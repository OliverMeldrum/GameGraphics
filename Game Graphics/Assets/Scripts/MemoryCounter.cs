using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using Unity.Profiling;
using UnityEngine;
using TMPro;

public class MemoryCounter : MonoBehaviour
{   
    ProfilerRecorder totalUsedMemoryRecorder;
    ProfilerRecorder totalReservedMemoryRecorder;
    ProfilerRecorder systemMemoryRecorder;
    ProfilerRecorder gcMemoryRecorder;
    ProfilerRecorder mainThreadTimeRecorder;

    [SerializeField]
    private TextMeshProUGUI statsText;

    void OnEnable()
    {
        totalUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
        totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
    }

    void OnDisable()
    {   
        totalUsedMemoryRecorder.Dispose();   
        totalReservedMemoryRecorder.Dispose();
    }

    void Update()
    {
        var sb = new StringBuilder(500);
        sb.AppendLine($"Memory Usage: {totalUsedMemoryRecorder.LastValue / (1024 * 1024)} MB");
        sb.AppendLine($"Total Reserved Memory: {totalReservedMemoryRecorder.LastValue / (1024 * 1024)} MB");
        statsText.text = sb.ToString();
    }
}
