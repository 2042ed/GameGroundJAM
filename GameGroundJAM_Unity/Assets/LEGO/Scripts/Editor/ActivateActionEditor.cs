using UnityEditor;
using Unity.LEGO.Behaviours.Actions;

namespace Unity.LEGO.EditorExt
{
    [CustomEditor(typeof(ActivateAction), true)]
    public class ActivateActionEditor : ActionEditor
    {
        SerializedProperty m_ObjectToActivateProp;
        SerializedProperty m_ActivationProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_ObjectToActivateProp = serializedObject.FindProperty("m_ObjectToActivate");
            m_ActivationProp = serializedObject.FindProperty("m_Activation");
        }

        protected override void CreateGUI()
        {

            if (m_ObjectToActivateProp.objectReferenceValue == null) {
                EditorGUILayout.HelpBox("You must set an Object.", MessageType.Warning);
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

            EditorGUILayout.PropertyField(m_ObjectToActivateProp);
            EditorGUILayout.PropertyField(m_ActivationProp);
            EditorGUI.EndDisabledGroup();

        }
    }
}
