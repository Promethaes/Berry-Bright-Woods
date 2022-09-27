using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Linq;
using UnityEditor.UIElements;

class PolymorphicListDisplay
{

    string name = "";
    bool foldout = false;
    int toolbarIndex = -1;

    List<Type> types;
    List<string> typeNames = new List<string>();
    ScriptableObject objectBeingMade = null;
    SerializedObject objectBeingMadeSerialized = null;
    GUIStyle style = new GUIStyle();

    SerializedObject owner;
    SerializedProperty list;


    int listSize => list.arraySize;

    //should be initialized once at creation of editor
    public PolymorphicListDisplay(Type baseType, SerializedProperty List, SerializedObject Owner, string objectName)
    {
        //get all derived types
        //https://stackoverflow.com/questions/857705/get-all-derived-types-of-a-type
        types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(domainAssembly => domainAssembly.GetTypes())
               .Where(type => baseType.IsAssignableFrom(type)
               ).ToList<Type>();
        name = objectName;
        owner = Owner;
        style.richText = true;
        list = List;

        foreach (var type in types)
            typeNames.Add(type.Name);
    }


    public void Display()
    {
        DisplayFoldout();
        DisplayObjectCreator();
    }


    void DisplayFoldout()
    {
        GUILayout.Space(10.0f);
        foldout = EditorGUILayout.Foldout(foldout, $"{name}s");
        if (foldout)
        {
            for (int i = 0; i < listSize; i++)
            {
                GUILayout.Space(5.0f);
                GUILayout.BeginHorizontal();
                GUILayout.Label($"<color=white><b>{name} #{i + 1}</b></color>", style);

                if (GUILayout.Button("Remove"))
                {
                    GUILayout.EndHorizontal();
                    AssetDatabase.RemoveObjectFromAsset(objectBeingMadeSerialized.targetObject);
                    list.DeleteArrayElementAtIndex(i);
                    owner.ApplyModifiedPropertiesWithoutUndo();

                    continue;
                }

                GUILayout.EndHorizontal();
                var reference = list.GetArrayElementAtIndex(i).objectReferenceValue;
                if(!reference){
                    Debug.LogError("null lol");
                    continue;
                }
                var refEditor = Editor.CreateEditor(reference);
                refEditor.OnInspectorGUI();
            }
        }

    }

    void DisplayObjectCreator()
    {
        GUILayout.Space(10.0f);
        var tempIndex = GUILayout.Toolbar(toolbarIndex == -1 ? 0 : toolbarIndex, typeNames.ToArray());

        if (toolbarIndex != tempIndex || objectBeingMadeSerialized == null && objectBeingMade == null)
        {
            toolbarIndex = tempIndex;
            objectBeingMade = ScriptableObject.CreateInstance(typeNames[toolbarIndex]);
            objectBeingMadeSerialized = new SerializedObject(objectBeingMade);
        }

        GUILayout.Label($"Create {typeNames[toolbarIndex]}");
        var tempEditor = Editor.CreateEditor(objectBeingMade);
        tempEditor.OnInspectorGUI();

        if (GUILayout.Button($"Add {name}"))
        {
            objectBeingMadeSerialized.ApplyModifiedProperties();
            AssetDatabase.AddObjectToAsset(objectBeingMadeSerialized.targetObject,AssetDatabase.GetAssetPath(owner.targetObject));
            list.arraySize++;
            list.GetArrayElementAtIndex(listSize - 1).objectReferenceValue = objectBeingMadeSerialized.targetObject;
            owner.ApplyModifiedProperties();
            objectBeingMade = ScriptableObject.CreateInstance(typeNames[toolbarIndex]);
            objectBeingMadeSerialized = new SerializedObject(objectBeingMade);
        }
    }
}

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    int index = -1;

    PolymorphicListDisplay conditionsDisplay;

    Vector2 scrollPos = new Vector2();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        conditionsDisplay.Display();
        //rewardsDisplay.Display();
    }


    public override VisualElement CreateInspectorGUI()
    {
        conditionsDisplay = new PolymorphicListDisplay(
            typeof(QuestCondition),
            serializedObject.FindProperty("conditions"),
            serializedObject,
            "Condition"
        );
        return base.CreateInspectorGUI();
    }
}