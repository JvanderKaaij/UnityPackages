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
    
    [MenuItem("Window/Custom Plot Graph")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PlotGraphEditor));
    }

    public void OnEnable()
    {
        var root = rootVisualElement;
        uxml.CloneTree(root);
        curveView = root.Q<CurveField>("plot_curve");
        ObjectField plotGraphObj = root.Q<ObjectField>("plot_object");
        plotGraphObj.objectType = typeof(PlotGraph);
        plotGraphObj.RegisterValueChangedCallback(x =>
        {
            plotGraph = (PlotGraph)plotGraphObj.value;
            if (plotGraph != null)
            {
                plotGraph.OnUpdate += Draw;
                Draw();
            }
        });
        
        var save_btn = root.Q<Button>("save_btn");

        save_btn.clicked += () =>
        {
            OpenFilePicker();
            // SaveCurve(save_path.value);
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
        
        File.WriteAllText(path, csvString);
        Debug.Log($"Successfully stored the graph values: {path}");
    }
    
}
