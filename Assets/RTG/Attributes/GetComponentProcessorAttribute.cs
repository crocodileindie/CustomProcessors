using System;
using UnityEngine;

namespace RTG.CustomProcessors.Attributes
{
    public enum Source
    {
        Self,
        Children,
        Parent,
    }

    [AttributeUsage(AttributeTargets.Field,AllowMultiple = false)]
    public sealed class GetComponentProcessorAttribute : PropertyAttribute
    {
        internal Source m_Source = Source.Self;
        internal bool m_IncludeInactive;
        internal bool MAlwaysProcess;
        public Source Source { get { return m_Source; } }
        public bool IncludeInactive { get { return m_IncludeInactive; } }
        public bool AlwaysProcess { get { return MAlwaysProcess; } }

        public GetComponentProcessorAttribute()
        {
        }

        public GetComponentProcessorAttribute(bool alwaysProcess)
        {
            MAlwaysProcess = alwaysProcess;
        }

        public GetComponentProcessorAttribute(bool alwaysProcess, Source source)
        {
            MAlwaysProcess = alwaysProcess;
            m_Source = source;
        }

        public GetComponentProcessorAttribute(bool alwaysProcess, Source source, bool includeInactive)
        {
            MAlwaysProcess = alwaysProcess;
            m_Source = source;
            m_IncludeInactive = includeInactive;
        }
    }
}