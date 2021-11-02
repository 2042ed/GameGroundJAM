using UnityEditor;
using Unity.LEGO.Behaviours.Actions;

namespace Unity.LEGO.EditorExt
{
    [CustomEditor(typeof(ActivateAction), true)]
    public class ActivateActionEditor : ActionEditor
    {
        SerializedProperty m_ObjectToActivateProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_ObjectToActivateProp = serializedObject.FindProperty("m_ObjectToActivate");
        }

        protected override void CreateGUI()
        {

            if (m_ObjectToActivateProp.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("You must set an Object.", MessageType.Warning);
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying );

            EditorGUILayout.PropertyField(m_ObjectToActivateProp);
            EditorGUI.EndDisabledGroup();

        }
    }
}
