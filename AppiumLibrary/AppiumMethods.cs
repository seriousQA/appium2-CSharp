using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;

namespace AppiumLibrary;

public class AppiumMethods
{
    private static AppiumLocalService service;
    private static AndroidDriver _driver;

    /// <summary> Build and start Appium Service. </summary>
    public static void BuildAppiumLocalService()
    {       
        service = new AppiumServiceBuilder()
        .WithIPAddress("127.0.0.1")
        .UsingPort(4723)
        .WithLogFile(new FileInfo("C:\\temp\\appiumLog.txt"))
        .UsingDriverExecutable(new FileInfo(System.Environment.GetEnvironmentVariable("NODE_BINARY_PATH")))
        .WithAppiumJS(new FileInfo(System.Environment.GetEnvironmentVariable("APPIUM_BINARY_PATH")))
        .WithStartUpTimeOut(TimeSpan.FromMinutes(3))
        .Build();
        service.Start();
        if (service.IsRunning.ToString().Equals("False"))
        {
            Console.WriteLine("Appium Service isn't started.");
        }
        else
        {
            Console.WriteLine("Appium Service is started.");
        }
    }

    /// <summary> Dispose Appium Service. </summary>
    public static void DisposeAppiumService()
    {
        service.Dispose();
        Console.WriteLine("Appium Service is disposed.");
    }

    /// <summary> Initialize Android driver.</summary>
    public static void SetupAndroidDriver()
    {
        var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
        var driverOptions = new AppiumOptions()
        {
            AutomationName = AutomationName.AndroidUIAutomator2,
            PlatformName = "Android"
        };
        driverOptions.AddAdditionalAppiumOption("appium:autoAcceptAlerts", "true");
        driverOptions.AddAdditionalAppiumOption("appium:newCommandTimeout", 180);
        driverOptions.AddAdditionalAppiumOption("appium:connectHardwareKeyboard", "true");
        driverOptions.AddAdditionalAppiumOption("appium:waitForIdleTimeout", 100);
        driverOptions.AddAdditionalAppiumOption("appium:firstMatch", "true");
        driverOptions.AddAdditionalAppiumOption("appium:disableWindowAnimation", "true");

        _driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        Console.WriteLine("Android Driver is started.");
    }

    /// <summary> Dispose Android driver. </summary>
    public static void DisposeAndroidDriver()
    {
        _driver.Dispose();
        Console.WriteLine("Android Driver is disposed.");
    }

    /// <summary> Activate App. </summary>
    /// <param name="apkName"> string apk name. </param>
    public static void ActivateApp(string apkName)
    {
        try
        {
            _driver.ActivateApp(apkName);
            Console.WriteLine("Driver: " + apkName + " was activated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: " + apkName + " was not activated.");
        }
    }

    /// <summary> Terminate App. </summary>
    /// <param name="apkName"> string apk name. </param>
    public static void TerminateApp(string apkName)
    {
        try
        {
            _driver.TerminateApp(apkName);
            Console.WriteLine("Driver: " + apkName + " was activated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: " + apkName + " was not activated.");
        }
    }

    /// <summary> Dispose Android driver. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique elemrnt. </param>
    public static void ClickOnElement(string locator, string element)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);

        try
        {
            if (locator.Equals("id"))
            {
                _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] {locator}).ToString())).Click();
                Console.WriteLine("Driver: Click() on " + element);
            }
            if (locator.Equals("xpath"))
            {
                _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] {locator}).ToString())).Click();
                Console.WriteLine("Driver: Click() on " + element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Dispose Android driver. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique elemrnt. </param>
    /// <param name="attributeName"> string attribut to GET. </param>
    public static string GetAttributeValueOfElement(string locator, string element, string attributeName)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        
        string actualValue = "";
        try
        {
            if (locator.Equals("id"))
            {
                actualValue = _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] {locator}).ToString())).GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element);
            }
            if (locator.Equals("xpath"))
            {
                actualValue = _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] {locator}).ToString())).GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
        return actualValue;
    }

}
