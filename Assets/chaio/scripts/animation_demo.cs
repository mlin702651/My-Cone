using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_demo : MonoBehaviour
{
    public Animator magicSong;
   
    // Start is called before the first frame update
    void Start()
    {
        magicSong = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {

            magicSong.SetBool("bSongMagic", true);
        }
        else
        {
            magicSong.SetBool("bSongMagic", false);
            magicSong.GetBool("bSongMagic");
        }
        Debug.Log(magicSong.GetBool("bSongMagic"));
    }
}
