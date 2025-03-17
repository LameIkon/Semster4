using UnityEngine;

/// <summary>
/// An abstract class design was chosen over an interface in case we need to upscale this system.
/// Abstract classes are more suited for that purpose.
/// </summary>
public interface SOCondition 
{
    public bool IsMet(); 
}