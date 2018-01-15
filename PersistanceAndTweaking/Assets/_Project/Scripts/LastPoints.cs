using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPoints {
    
    public List<Characters> m_allCharacters;

    public LastPoints(Characters _character)
    {
        m_allCharacters.Add(_character);
    }
}
