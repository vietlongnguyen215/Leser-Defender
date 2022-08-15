using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhDisplay : MonoBehaviour
{
    Text healhPlayer;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        healhPlayer = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healhPlayer.text = player.getHealh().ToString();
    }
}
