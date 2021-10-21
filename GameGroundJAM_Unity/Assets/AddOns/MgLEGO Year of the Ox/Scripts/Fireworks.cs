using System.Collections.Generic;
using UnityEngine;

namespace Unity.LEGO.AddOns.YearOfTheOx
{
    public class Fireworks : MonoBehaviour
    {
        [SerializeField, Tooltip("The effect used when the fireworks explodes.")]
        ParticleSystem m_FireworksEffect = null;

        [SerializeField, Tooltip("The effect used while the fireworks launches.")]
        ParticleSystem m_SparksEffect = null;

        [SerializeField, Tooltip("One of these colours will be randomly used to tint the fireworks explosion.")]
        List<Color> m_Colours = null;

        ParticleSystem m_ParticleSystem;

        void Awake()
        {
            // Spawn the sparks.
            m_ParticleSystem = Instantiate(m_SparksEffect, transform.position, transform.rotation);
        }

        void Update()
        {
            // Track the projectile's position and rotation.
            m_ParticleSystem.transform.position = transform.position;
            m_ParticleSystem.transform.rotation = transform.rotation;
        }

        void OnDestroy()
        {
            // Spawn the fireworks and assign a random colour from the palette.
            var particleSystem = Instantiate(m_FireworksEffect, transform.position, transform.rotation);
            if (m_Colours.Count > 0)
            {
                var mainModule = particleSystem.main;
                var startColor = mainModule.startColor;
                startColor.color = m_Colours[Random.Range(0, m_Colours.Count)];
                mainModule.startColor = startColor;
            }

            // Stop the sparks.
            m_ParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
