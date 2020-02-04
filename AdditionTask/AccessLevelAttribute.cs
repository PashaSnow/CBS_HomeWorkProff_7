using System;

namespace AdditionTask
{
    public enum Leavel
    {
        LowControl,
        MediumControl,
        HighControl
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AccessLevelAttribute : System.Attribute
    {
        readonly Leavel accessLeavel;
        public AccessLevelAttribute(Leavel accessLeavel)
        {
            this.accessLeavel = accessLeavel;
        }

        public Leavel Access
        {
            get
            {
                return accessLeavel;
            }
        }
    }
}
