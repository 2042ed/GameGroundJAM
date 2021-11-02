using LEGOModelImporter;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.LEGO.Behaviours.Actions
{
    public class ActivateAction : Action
    {
        [SerializeField, Tooltip("The Object to Activate.")]
        GameObject m_ObjectToActivate = null;

        bool m_Done = false;
        protected override void Reset()
        {
            base.Reset();
            m_Done = false;

 //           m_Scope = Scope.Brick;
            m_IconPath = "Assets/LEGO/Gizmos/LEGO Behaviour Icons/Shoot Action.png";
        }

        protected void Update()
        {
            if (m_Active)
            {

                if (!m_Done)
                {
                    ActivateObject();
                    m_Done = true;
                }
            }
        }

        void ActivateObject()
        {
            Debug.Log("HEIIII");
            if (m_ObjectToActivate)
            {
                m_ObjectToActivate.SetActive(true);
            }
        }

    }
}
