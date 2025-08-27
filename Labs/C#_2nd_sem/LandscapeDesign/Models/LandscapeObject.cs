using LandscapeDesign.Enums;

namespace LandscapeDesign.Models
{
    internal class LandscapeObject
    {
        public ObjectType Type { get; private set; } = ObjectType.NonObject;
        public void ChangeObject(ObjectType objectType)
        {
            Type = objectType;
        }
    }
}