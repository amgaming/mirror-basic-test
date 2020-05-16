using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;    
    // Start is called before the first frame update
    [SerializeField] private SettingsPopup settingsPopup;
    private int _score;
    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();    
        settingsPopup.Close();    
    }

    // Update is called once per frame
    void Update()
    {
        //scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }
    public void OnOpenSettings() 
    { 
       settingsPopup.Open();    
    }
    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

}
