using UnityEngine;
using UnityEngine.UI;
using OneStore;

public class UIManager : MonoBehaviour
{
    public Text text;

    void Awake()
    {
        ALC_ResultListener.PrintLog += PrintLog;
    }
    void OnDestroy()
    {
        ALC_Manager.Destroy();
        ALC_ResultListener.PrintLog -= PrintLog;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // =========================================================================
    // API 호출
    // =========================================================================

    /**
     * Check license in flexible mode
     */
    public void CheckLicenseOfFlexible()
    {
        Debug.Log("UIManager CheckLicenseOfFlexible");
        PrintLog("UIManager", "Flexible Click !!!");
        ALC_Manager.CheckLicense(Constant.BASE_64_ENCODED_PUBLIC_KEY, "Flexible");
    }

    /**
     * Check license in strict mode
     */
    public void CheckLicenseOfStrict()
    {
        Debug.Log("UIManager CheckLicenseOfStrict");
        PrintLog("UIManager", "Strict Click !!!");
        ALC_Manager.CheckLicense(Constant.BASE_64_ENCODED_PUBLIC_KEY, "Strict");
    }

    /**
     * Express response data on the screen.
     */
    public void PrintLog(string tag, string message)
    {
        text.text += "=== [ " + tag + " ] ===\n" + message + "\n";
    }
}
