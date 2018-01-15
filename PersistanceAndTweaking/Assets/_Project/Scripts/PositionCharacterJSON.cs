using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PositionCharacterJSON : MonoBehaviour {

    public GameObject m_player;
    public GameObject targetGhost;

    void Awake()
    {
        m_pathCharacters = Application.dataPath + "/_Project/JSON/PositionOfCharacter.json";
        m_pathLastPoints = Application.dataPath + "/_Project/JSON/LastPointsJSON.json";

        m_jsonString = File.ReadAllText(m_pathCharacters);
        m_jsonLastPoints = File.ReadAllText(m_pathLastPoints);

        Characters character = JsonUtility.FromJson<Characters>(m_jsonString);
        List<Characters> last = JsonUtility.FromJson<List<Characters>>(m_jsonLastPoints);

        if (character != null)
        {
            m_player.transform.position = character.m_positionCharacter;
            m_player.transform.rotation = character.m_rotationCharacter;
        }

        if (last != null && last.Count > 0)
            m_lastPoints = last;

        m_Archer = new Characters(m_player.name, m_player.transform.position, m_player.transform.rotation);
    }

    void Start()
    {
        File.WriteAllText(m_pathCharacters, JsonUtility.ToJson(m_Archer));
        File.WriteAllText(m_pathLastPoints, JsonUtility.ToJson(m_lastPoints));

        StartCoroutine("SavePosition");
        StartCoroutine("GhostPosition");
    }

    void FixedUpdate()
    {
        m_Archer.m_positionCharacter = m_player.transform.position;
        m_Archer.m_rotationCharacter = m_player.transform.rotation;
    }

    private IEnumerator SavePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            File.WriteAllText(m_pathCharacters, JsonUtility.ToJson(m_Archer));

             //m_lastPoints.Add(m_Archer);
            //File.WriteAllText(m_pathLastPoints, JsonUtility.ToJson(m_lastPoints.ToString()));

            m_jsonString = File.ReadAllText(m_pathCharacters);
            m_jsonLastPoints = File.ReadAllText(m_pathLastPoints);
            Debug.Log("SAVE");
        }
    }

    private IEnumerator GhostPosition()
    {
        int character = 0;
        while (true)
        {
            yield return new WaitForSeconds(5);
            targetGhost.transform.position = m_lastPoints[character].m_positionCharacter;
            targetGhost.transform.rotation = m_lastPoints[character].m_rotationCharacter;
            character++;
            Debug.Log("GHOST");
        }
    }

    public Characters m_Archer;
    private List<Characters> m_lastPoints = new List<Characters>();

    private string m_pathCharacters;
    private string m_pathLastPoints;

    private string m_jsonString;
    private string m_jsonLastPoints;
}