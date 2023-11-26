using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public AudioSource pling;
    public Image blackPanel;
    private bool _isTurningBlack = false;
    private float _alpha;
    
    // Start is called before the first frame update
    void Start()
    {
        _alpha = 0;
        blackPanel.color = new Color(0, 0, 0, _alpha);
        _isTurningBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isTurningBlack)
        {
            blackPanel.color = new Color(0, 0, 0, _alpha);
            _alpha += 0.5f * Time.deltaTime;
            if(_alpha >= 1)
            {
                _alpha = 1;
            }

            if(blackPanel.color == new Color(0, 0, 0 , 1))
            {

                SceneManager.LoadScene(0);
            }
        }
    }

    public void SceneLoad(int index)
    {
        pling.Play();
        _isTurningBlack = true;
    }

}
