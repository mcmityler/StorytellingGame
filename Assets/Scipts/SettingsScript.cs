using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] GameObject _settingPanel;
    bool _settingOn = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ToggleSettingScreen();
        }
    }
    public void ToggleSettingScreen(){
            _settingOn = _settingPanel.activeInHierarchy;
            _settingPanel.SetActive(!_settingOn);
            _settingOn = !_settingOn;
    }
}
