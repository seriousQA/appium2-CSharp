using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiumLibrary;

namespace MSTestProject;

// AUT: com.android.settings. 
// Tested on emulator-5554.
// "locale":"en_EN", "platformName": "Android", "platformVersion":"13".
[TestClass]
public class EmulatorUnitTest
{
    [TestInitialize]
    public void Setup()
    {
        AppiumMethods.StartEmulator("Medium_Phone_API_33");
        AppiumMethods.BuildAppiumLocalService();
        AppiumMethods.SetupAndroidDriverOnEmulator();
        AppiumMethods.ActivateApp("com.android.settings");
        Thread.Sleep(1000);
    }
    
    [TestMethod]    
    public void Settings()
    {        
        string actual, expected;
        actual = AppiumMethods.GetAttributeValue("xpath", "t_Apps", "text");
        expected = "Apps";
        Assert.AreEqual(expected, actual);
        AppiumMethods.ScrollToElement("text", "t_AboutEmulatedDevice");
        actual = AppiumMethods.GetAttributeValue("xpath", "t_AboutEmulatedDevice", "text");
        expected = "About emulated device";
        AppiumMethods.GetScreenshot();
        Assert.AreEqual(expected, actual);
    }

    [TestCleanup]
    public void Teardown()
    {
        AppiumMethods.TerminateApp("com.android.settings");
        AppiumMethods.DisposeAndroidDriver();
        AppiumMethods.DisposeAppiumService();
        AppiumMethods.CloseEmulator();
    }
}