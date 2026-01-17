using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiumLibrary;

namespace MSTestProject;

// AUT: com.android.chrome 109.0.5414.123. 
// Tested on emulator-5554
// "locale":"en_EN", "platformName": "Android", "platformVersion":"13".
[TestClass]
public class HybridTestSet
{
    [TestInitialize]
    public void Setup()
    {
        AppiumMethods.StartEmulator("Medium_Phone_API_33");
        AppiumMethods.BuildAppiumLocalService();
        AppiumMethods.SetupAndroidDriverOnEmulator();
        AppiumMethods.ActivateApp("com.android.chrome");
        AppiumMethods.ClickOnElement("id", "chrome_home");
        AppiumMethods.WaitTillElementIsVisible("id", "chrome_search_box_text", 5);
        AppiumMethods.ClickOnElement("id", "chrome_search_box_text");
        AppiumMethods.WaitTillElementIsVisible("id", "chrome_url_bar", 5);
        AppiumMethods.SendKeysOnElement("id", "chrome_url_bar", "https://de.wikipedia.org/wiki/Wikipedia:Hauptseite");
        AppiumMethods.PressKeyCode(66); // Press Enter key
        Thread.Sleep(3000);
    }

    [TestMethod]
    public void OpenWikipedia()
    {
        AppiumMethods.SwitchContext("WEBVIEW_chrome");
        AppiumMethods.WaitTillElementIsVisible("id", "Willkommen_bei_Wikipedia", 20);
        AppiumMethods.ValidateIsDisplayed("xpath", "Wikipedia_Presse");
        AppiumMethods.GetScreenshot();
        AppiumMethods.ClickOnElement("xpath", "Wikipedia_Presse");
        // to test waiting for element to disappear
        AppiumMethods.WaitTillElementDisappear("id", "Willkommen_bei_Wikipedia", 20);

        AppiumMethods.WaitTillElementIsVisible("xpath", "Ueber_Wikipedia", 20);
        AppiumMethods.ValidateIsDisplayed("xpath", "Ueber_Wikipedia");
        AppiumMethods.GetScreenshot();
        AppiumMethods.SwitchContext("NATIVE_APP");
    }

    [TestCleanup]
    public void Teardown()
    {
        AppiumMethods.TerminateApp("com.android.chrome");
        AppiumMethods.DisposeAndroidDriver();
        AppiumMethods.DisposeAppiumService();
        AppiumMethods.CloseEmulator();
    }
}