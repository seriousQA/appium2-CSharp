using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppiumLibrary;

namespace MSTestProject;

[TestClass]
public class UnitTest
{
    [TestInitialize]
    public void Setup()
    {
        AppiumMethods.BuildAppiumLocalService();
        AppiumMethods.SetupAndroidDriver();
        AppiumMethods.ActivateApp("com.android.calculator2");
        AppiumMethods.ClickOnElement("id", "btn_clear");
    }

    [TestMethod]
    public void TestMethod1_Sum()
    {
        AppiumMethods.ClickOnElement("id", "btn_digit7");
        AppiumMethods.ClickOnElement("id", "btn_add");
        AppiumMethods.ClickOnElement("id", "btn_digit3");
        AppiumMethods.ClickOnElement("id", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "10";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod2_Sub()
    {
        AppiumMethods.ClickOnElement("id", "btn_digit1");
        AppiumMethods.ClickOnElement("id", "btn_digit0");
        AppiumMethods.ClickOnElement("id", "btn_sub");
        AppiumMethods.ClickOnElement("id", "btn_digit5");
        AppiumMethods.ClickOnElement("id", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "5";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod3_Div()
    {
        AppiumMethods.ClickOnElement("id", "btn_digit8");
        AppiumMethods.ClickOnElement("id", "btn_div");
        AppiumMethods.ClickOnElement("id", "btn_digit4");
        AppiumMethods.ClickOnElement("id", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "2";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod4_Mul()
    {
        AppiumMethods.ClickOnElement("id", "btn_digit6");
        AppiumMethods.ClickOnElement("id", "btn_mul");
        AppiumMethods.ClickOnElement("id", "btn_digit1");
        AppiumMethods.ClickOnElement("id", "btn_digit0");
        AppiumMethods.ClickOnElement("id", "btn_digit0");
        AppiumMethods.ClickOnElement("id", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "600";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod5_DecimalPoint()
    {
        AppiumMethods.ClickOnElement("xpath", "btn_digit2");
        AppiumMethods.ClickOnElement("xpath", "btn_dec_point");
        AppiumMethods.ClickOnElement("xpath", "btn_digit1");
        AppiumMethods.ClickOnElement("xpath", "btn_sub");
        AppiumMethods.ClickOnElement("xpath", "btn_digit1");        
        AppiumMethods.ClickOnElement("xpath", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "1,1";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestMethod6_Percentage()
    {
        AppiumMethods.ClickOnElement("id", "btn_digit1");
        AppiumMethods.ClickOnElement("id", "btn_digit0");
        AppiumMethods.ClickOnElement("id", "btn_digit0");
        AppiumMethods.ClickOnElement("id", "btn_pct");      
        AppiumMethods.ClickOnElement("id", "btn_eq");
        string actual = AppiumMethods.GetAttributeValue("id", "edt_formula", "text");
        string expected = "1";
        Assert.AreEqual(expected, actual);
    }

    [TestCleanup]
    public void Teardown()
    {
        AppiumMethods.TerminateApp("com.android.calculator2");
        AppiumMethods.DisposeAndroidDriver();
        AppiumMethods.DisposeAppiumService();
    }
}