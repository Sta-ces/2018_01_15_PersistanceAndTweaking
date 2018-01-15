using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PositionCharacterJSON : MonoBehaviour {

    public GameObject m_player;

    void Awake()
    {
        m_path = Application.dataPath + "/_Project/JSON/PositionOfCharacter.json";

        m_jsonString = File.ReadAllText(m_path);

        Characters character = JsonUtility.FromJson<Characters>(m_jsonString);
        if (character != null)
        {
            m_player.transform.position = character.m_positionCharacter;
            m_player.transform.rotation = character.m_rotationCharacter;
        }

        m_Archer = new Characters(m_player.name, m_player.transform.position, m_player.transform.rotation);
    }

    void Start()
    {
        File.WriteAllText(m_path, JsonUtility.ToJson(m_Archer));

        StartCoroutine("SavePosition");
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
            File.WriteAllText(m_path, JsonUtility.ToJson(m_Archer));
            m_jsonString = File.ReadAllText(m_path);
            Debug.Log("SAVE");
        }
    }

    private Characters m_Archer;
    private string m_path;
    private string m_jsonString;
    private List<Characters> m_lastPoints = new List<Characters>();
}