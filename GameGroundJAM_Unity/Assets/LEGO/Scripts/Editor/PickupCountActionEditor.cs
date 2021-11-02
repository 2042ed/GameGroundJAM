using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.LEGO.Game;
using Unity.LEGO.Behaviours.Actions;
using Unity.LEGO.Behaviours.Triggers;
using UnityEditor.SceneManagement;

namespace Unity.LEGO.EditorExt
{
    [CustomEditor(typeof(PickupCountAction), true)]
    public class PickupCountActionEditor : ActionEditor
    {
        SerializedProperty m_VariableProp;
        Editor m_VariableEditor;

        PickupCountAction m_PickupCountAction;

        SerializedProperty m_EffectProp;

        List<Trigger> m_DependentTriggers = new List<Trigger>();

        protected override void OnEnable()
        {
            base.OnEnable();
            m_VariableProp = serializedObject.FindProperty("m_Variable");

            m_PickupCountAction = (PickupCountAction)m_Action;

            m_EffectProp = serializedObject.FindProperty("m_Effect");

        }

        protected void OnDisable()
        {
            DestroyImmediate(m_VariableEditor);
        }

        protected override void CreateGUI()
        {
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

            EditorGUILayout.PropertyField(m_ScopeProp);

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(m_AudioProp);
            EditorGUILayout.PropertyField(m_AudioVolumeProp);

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

            EditorGUILayout.PropertyField(m_EffectProp);

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

            // Refresh variable list.
            var variables = GetAvailableVariables();
            variables.Item2.Add("[Add New Variable]");

            // Update variable index.
            var index = variables.Item1.FindIndex(item => item == (Variable)m_VariableProp.objectReferenceValue);

            index = EditorGUILayout.Popup(new GUIContent("Variable", "The variable to modify."), index, variables.Item2.ToArray());

            if (index > -1)
            {
//                EditorGUILayout.PropertyField(m_OperatorProp);
//                EditorGUILayout.PropertyField(m_ValueProp);

                EditorGUI.EndDisabledGroup();

//                EditorGUILayout.PropertyField(m_PauseProp);

                EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

//                EditorGUILayout.PropertyField(m_RepeatProp);

                DrawSeparator();
                EditorGUILayout.LabelField("Variable Settings", EditorStyles.boldLabel);

                if (index == variables.Item2.Count - 1)
                {
                    var newVariable = CreateInstance<Variable>();
                    newVariable.Name = "Variable";
                    var newVariableAssetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(VariableManager.k_VariablePath, "Variable.asset"));
                    AssetDatabase.CreateAsset(newVariable, newVariableAssetPath);
                    m_VariableProp.objectReferenceValue = newVariable;
                }
                else
                {
                    m_VariableProp.objectReferenceValue = variables.Item1[index];

                    // Only recreate editor if necessary.
                    if (!m_VariableEditor || m_VariableEditor.target != m_VariableProp.objectReferenceValue)
                    {
                        DestroyImmediate(m_VariableEditor);
                        m_VariableEditor = CreateEditor(m_VariableProp.objectReferenceValue);
                    }

                    m_VariableEditor.OnInspectorGUI();

                    if (GUILayout.Button("Delete Variable"))
                    {
                        AssetDatabase.DeleteAsset(variables.Item3[index]);
                    }
                }
            }

            EditorGUI.EndDisabledGroup();
        }

    }
}
