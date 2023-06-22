using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PlotGraphEditor:EditorWindow
{
    public VisualTreeAsset uxml;
    private CurveField curveView;
    private PlotGraph plotGraph;
    private PlotGraph prevPlotGraph;
    
    [MenuItem("Window/Custom Plot Graph")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PlotGraphEditor));
    }

    public void Update()
    {
        if (!Application.isPlaying) return;
        
        plotGraph = Selection.activeGameObject?.GetComponent<PlotGraph>();
        
        if (plotGraph)
        {
            if(plotGraph.OnUpdate == null){
                plotGraph.OnUpdate += Draw;
            }
            prevPlotGraph = plotGraph;
            Draw();
        }else if(prevPlotGraph.OnUpdate != null)
        {
            prevPlotGraph.OnUpdate -= Draw;
        }

    }

    public void OnEnable()
    {
        var root = rootVisualElement;
        uxml.CloneTree(root);
        curveView = root.Q<CurveField>("plot_curve");

        var save_btn = root.Q<Button>("save_btn");
        save_btn.clicked += () =>
        {
            OpenFilePicker();
        };
        
        var clear_btn = root.Q<Button>("clear_btn");
        clear_btn.clicked += () =>
        {
            plotGraph.Clear();
        };
    }

    private void Draw()
    {
        curveView.label = plotGraph.name;
        curveView.value = plotGraph.GetCurve();
    }
    
    private void OpenFilePicker()
    {
        var selectedFilePath = EditorUtility.SaveFilePanel("Save a File", "", "data", "csv");
        SaveCurve(selectedFilePath);
    }
    
    private void SaveCurve(string path)
    {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Time,Value");
        foreach (var kf in curveView.value.keys)
        {
            csv.AppendLine($"{kf.time},{kf.value}");
        }
        string csvString = csv.ToString();
        if(path != null){
            File.WriteAllText(path, csvString);
        }
        Debug.Log($"Successfully stored the graph values: {path}");
    }
    
}
