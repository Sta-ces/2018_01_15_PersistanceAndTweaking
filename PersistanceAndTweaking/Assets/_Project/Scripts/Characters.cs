using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Characters{
    public string m_nameObject;
    public Vector3 m_positionCharacter;
    public Quaternion m_rotationCharacter;
    public List<>

    public Characters(string _name, Vector3 _position, Quaternion _rotation)
    {
        m_nameObject = _name;
        m_positionCharacter = _position;
        m_rotationCharacter = _rotation;
    }
	
}
