using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiumLibrary;

namespace MSTestProject;

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
        Thread.Sleep(2000);
    }

    [TestMethod]
    public void Settings()
    {   
        string actual1 = AppiumMethods.GetAttributeValue("xpath", "t_Apps", "text");
        string expected1 = "Apps";        
        Assert.AreEqual(expected1, actual1);
        AppiumMethods.Swipe();
        Thread.Sleep(1000);
        AppiumMethods.GetScreenshot();
        string actual2 = AppiumMethods.GetAttributeValue("xpath", "t_AboutEmulatedDevice", "text");
        string expected2 = "About emulated device";        
        Assert.AreEqual(expected2, actual2);
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