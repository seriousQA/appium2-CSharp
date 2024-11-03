using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

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
            Console.WriteLine("Driver: " + apkName + " was terminated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: " + apkName + " was not terminated.");
        }
    }

    /// <summary> Remove App. </summary>
    /// <param name="apkName"> string apk name. </param>
    public static void RemoveApp(string apkName)
    {
        try
        {
            _driver.RemoveApp(apkName);
            Console.WriteLine("Driver: " + apkName + " was removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: " + apkName + " was not removed.");
        }
    }

    /// <summary> Install App. </summary>
    /// <param name="apkPath"> string absolute path to apk file. </param>
    public static void InstallApp(string apkPath)
    {
        try
        {
            _driver.InstallApp(apkPath);
            Console.WriteLine("Driver: apk was installed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: apk was not installed.");
        }
    }

    /// <summary> Hide keyboard. </summary>
    public static void HideKeyboard()
    {
        _driver.HideKeyboard();
        Console.WriteLine("Driver: HideKeyboard().");
    }

    /// <summary> Simulate Android phone BACK. </summary>
    public static void AndroidBack()
    {
        //KEYCODE_BACK constant value: 4 (0x00000004)
        _driver.Navigate().Back();
        Console.WriteLine("Driver: Back().");
    }

    /// <summary> Simulate Android phone NEXT. </summary>
    public static void AndroidNext()
    {
        //KEYCODE_NAVIGATE_NEXT constant value: 261 (0x00000105)
        _driver.ExecuteScript("mobile:performEditorAction", new Dictionary<string, string> { { "action", "next" } });
        Console.WriteLine("Driver: Next().");
    }

    /// <summary> Execute shell script. </summary>
    /// <param name="script"> string script. </param>
    public static void ExecuteShellScript(string script)
    {
        //E.g., to refresh Android mediastore using adb: 
        // "adb shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard"        
        _driver.ExecuteScript("mobile:shell", script);
        Console.WriteLine("Driver: Next().");
    }

    /// <summary> Start recording screen. </summary>
    public static void StartRecordingScreen()
    {
        _driver.StartRecordingScreen();
        Console.WriteLine("Driver: StartRecordingScreen().");
    }

    /// <summary> Stop recording screen. </summary>
    public static void StopRecordingScreen()
    {
        string video = _driver.StopRecordingScreen();
        byte[] decode = Convert.FromBase64String(video);
        File.WriteAllBytes("C:\\temp\\VideoRecording.mp4", decode);
        Console.WriteLine("Driver: StopRecordingScreen().");
    }

    /// <summary> Take a screenshot of the current viewport/window/page. </summary>
    public static void GetScreenshot()
    {
        _driver.GetScreenshot().SaveAsFile("C:\\temp\\Screenshot.png");
        Console.WriteLine("Driver: GetScreenshot().");
    }

    /// <summary> Click on element. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    public static void ClickOnElement(string locator, string element)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);

        try
        {
            if (locator.Equals("id"))
            {
                _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).Click();
                Console.WriteLine("Driver: Click() on " + element);
            }
            if (locator.Equals("xpath"))
            {
                _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).Click();
                Console.WriteLine("Driver: Click() on " + element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Get attribute value of element. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    /// <param name="attributeName"> string attribut to GET. </param>
    public static string GetAttributeValue(string locator, string element, string attributeName)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);

        string actualValue = "";
        try
        {
            if (locator.Equals("id"))
            {
                actualValue = _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element);
            }
            if (locator.Equals("xpath"))
            {
                actualValue = _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
        return actualValue;
    }

    /// <summary> SendKeys on element. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    /// <param name="value"> string value to send. </param>
    public static void SendKeysOnElement(string locator, string element, string value)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        AppiumElement el;
        try
        {
            if (locator.Equals("id"))
            {
                el = _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                el.Click();
                el.Clear();
                el.SendKeys(value);
                _driver.HideKeyboard();
                Console.WriteLine("Driver: SendKeys(" + value + ") on " + element);
            }
            if (locator.Equals("xpath"))
            {
                el = _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                el.Click();
                el.Clear();
                el.SendKeys(value);
                _driver.HideKeyboard();
                Console.WriteLine("Driver: SendKeys(" + value + ") on " + element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Is element displayed? </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    public static void ValidateIsDisplayed(string locator, string element)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);

        try
        {
            if (locator.Equals("id"))
            {
                if (_driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute("displayed").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: " + element + " is displayed.");
                }
                else
                {
                    Console.WriteLine("Driver: " + element + " is not displayed.");
                }
            }
            if (locator.Equals("xpath"))
            {
                if (_driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute("displayed").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: " + element + " is displayed.");
                }
                else
                {
                    Console.WriteLine("Driver: " + element + " is not displayed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Is element checked? </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    public static void ValidateIsChecked(string locator, string element)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);

        try
        {
            if (locator.Equals("id"))
            {
                if (_driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute("checked").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: " + element + " is checked.");
                }
                else
                {
                    Console.WriteLine("Driver: " + element + " is not checked.");
                }
            }
            if (locator.Equals("xpath"))
            {
                if (_driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).GetAttribute("checked").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: " + element + " is checked.");
                }
                else
                {
                    Console.WriteLine("Driver: " + element + " is not checked.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Place a file onto the device in a particular place. </summary>
    /// <param name="pathOnDevice"> string absolute path on mobile device. </param>
    /// <param name="fileName"> string file name with format of file. </param>
    /// <param name="sourceFilePath"> string absolute path on computer. </param>
    public static void PushFile(string pathOnDevice, string fileName, string sourceFilePath)
    {
        try
        {
            string destinationPathOnDevice = pathOnDevice + fileName;
            _driver.PushFile(destinationPathOnDevice, new FileInfo(sourceFilePath));
            Console.WriteLine("Driver: " + fileName + " was pushed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: " + fileName + " was not pushed.");
        }
    }

    /// <summary> Retrieve a file from the device's file system. </summary>
    /// <param name="pathOnDevice"> string absolute path on mobile device. </param>
    /// <param name="pathOnComputer"> string absolute path on computer. </param>
    public static void PullFile(string pathOnDevice, string pathOnComputer)
    {
        try
        {
            byte[] data = _driver.PullFile(pathOnDevice);
            File.WriteAllBytes(pathOnComputer, data);
            Console.WriteLine("Driver: file was saved on computer.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: file was not saved on computer.");
        }
    }

    /// <summary> Scroll to top. </summary>
    public static void ScrollToTop()
    {
        // E.g., "new UiScrollable(new UiSelector()).scrollIntoView(text(\"your Text\"));" or scroll to top:
        string command = "new UiScrollable(new UiSelector().scrollable(true).instance(0)).flingToBeginning(3)";
        _driver.FindElement(ByAndroidUIAutomator.AndroidUIAutomator(command));
        Console.WriteLine("Driver: ScrollToTop().");
    }
    
    /// <summary> Swipe. </summary>
    public static void Swipe()
    {      
        int screen_height = _driver.Manage().Window.Size.Height;
        int screen_width = _driver.Manage().Window.Size.Width;

        int startY = (int) (screen_height * 0.8);
        int endY = (int) (screen_width * 0.21);
        int startX = (int) (screen_width * 2.1);
        int endX = endY;
        
        PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch, "finger");
        var sequence  = new ActionSequence(finger);

        var move1 = finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.FromMilliseconds(100));
        var down = finger.CreatePointerDown(OpenQA.Selenium.Interactions.MouseButton.Left);
        var pause = finger.CreatePause(TimeSpan.FromMilliseconds(200));
        var move2 = finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, TimeSpan.FromMilliseconds(100));
        var up = finger.CreatePointerUp(OpenQA.Selenium.Interactions.MouseButton.Left);
        
        sequence.AddAction(move1);
        sequence.AddAction(down);
        sequence.AddAction(pause);
        sequence.AddAction(move2);
        sequence.AddAction(up);

        _driver.PerformActions((IList<ActionSequence>)sequence);
        Console.WriteLine("Driver: Swipe()");
    }

    /// <summary> Long press on element. </summary>
    /// <param name="locator"> string locator. </param>
    /// <param name="element"> string unique element. </param>
    public static void LongPress(string locator, string element)
    {
        Type repo = typeof(AppiumLibrary.ElementsRepository);
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        AppiumElement el = null;
        PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch, "finger");
        var sequence = new ActionSequence(finger);
        try
        {
            if (locator.Equals("id"))
            {
                el = _driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString()));

            }
            if (locator.Equals("xpath"))
            {
                el = _driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString()));
            }
            var move = finger.CreatePointerMove(CoordinateOrigin.Viewport, el.Coordinates.LocationOnScreen.X, el.Coordinates.LocationOnScreen.Y, TimeSpan.FromMilliseconds(100));
            var down = finger.CreatePointerDown(OpenQA.Selenium.Interactions.MouseButton.Left);
            var pause = finger.CreatePause(TimeSpan.FromSeconds(1));
            var up = finger.CreatePointerUp(OpenQA.Selenium.Interactions.MouseButton.Left);

            sequence.AddAction(move);
            sequence.AddAction(down);
            sequence.AddAction(pause);
            sequence.AddAction(up);

            _driver.PerformActions((IList<ActionSequence>)sequence);
            Console.WriteLine("Driver: LongPress() on " + element);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }
}
