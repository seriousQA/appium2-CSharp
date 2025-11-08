using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiumLibrary;

namespace MSTestProject;

// AUT: com.android.chrome. 
// Tested on emulator-5554
// "locale":"en_EN", "platformName": "Android", "platformVersion":"13".
[TestClass]
public class WebTestSet
{
    [TestInitialize]
    public void Setup()
    {
        AppiumMethods.BuildAppiumLocalService();
        AppiumMethods.SetupAndroidDriverOnEmulator();
        AppiumMethods.ActivateApp("com.android.chrome");        
        AppiumMethods.ClickOnElement("id", "chrome_home");
        AppiumMethods.WaitTillElementIsVisible("id", "chrome_search_box_text", 5);
        AppiumMethods.ClickOnElement("id", "chrome_search_box_text");
        AppiumMethods.WaitTillElementIsVisible("id", "chrome_url_bar", 5);
        AppiumMethods.SendKeysOnElement("id", "chrome_url_bar", "https://dimonade.eu.pythonanywhere.com/");
        AppiumMethods.PressKeyCode(66); // Press Enter key
        Thread.Sleep(1000);
        AppiumMethods.WaitTillElementIsVisible("xpath", "Geoplotnik_Home", 15);
    }

    [TestMethod]
    public void validateNavlinks()
    {
        AppiumMethods.ClickOnElement("xpath", "Geoplotnik_HeaderBurger");
        AppiumMethods.WaitTillElementIsVisible("xpath", "Geoplotnik_Navlink", 20);
        AppiumMethods.ValidateIsDisplayed("xpath", "Geoplotnik_Navlink");
        AppiumMethods.GetScreenshot();
        foreach (var content in AppiumMethods.GetElements("xpath", "Geoplotnik_Navlink"))
        {
            string contentDesc = content.GetAttribute("content-desc");
            Assert.IsTrue(contentDesc.Equals("Home")
            || contentDesc.Equals("General plots")
            || contentDesc.Equals("Rock classification")
            || contentDesc.Equals("Series discriminant templates")); // Validate that the content description matches expected values
        }
    }

    [TestCleanup]
    public void Teardown()
    {
        AppiumMethods.TerminateApp("com.android.chrome");
        AppiumMethods.DisposeAndroidDriver();
        AppiumMethods.DisposeAppiumService();
    }
}