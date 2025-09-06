using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AppiumLibrary;

public class AppiumMethods
{
    private static AppiumLocalService service;
    private static AndroidDriver _driver;
    private static Type repo = typeof(AppiumLibrary.ElementsRepository);

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

    /// <summary> Initialize Android driver.</summary>
    public static void SetupAndroidDriverOnEmulator()
    {
        var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
        var driverOptions = new AppiumOptions()
        {
            AutomationName = AutomationName.AndroidUIAutomator2,
            PlatformName = "Android",
            PlatformVersion = "13",
            DeviceName = "emulator-5554"
        };
        driverOptions.AddAdditionalAppiumOption("appium:autoAcceptAlerts", "true");
        driverOptions.AddAdditionalAppiumOption("appium:newCommandTimeout", 180);
        driverOptions.AddAdditionalAppiumOption("appium:autoGrantPermissions", "true");

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
    /// <param name="apkName"> string, apk name. </param>
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
    /// <param name="apkName"> string, apk name. </param>
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
    /// <param name="apkName"> string, apk name. </param>
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
    /// <param name="apkPath"> string, absolute path to apk file. </param>
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
    /// <param name="script"> string, script body. </param>
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
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    public static void ClickOnElement(string locator, string element)
    {
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
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    /// <param name="attributeName"> string, attribut to GET. </param>
    public static string GetAttributeValue(string locator, string element, string attributeName)
    {
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
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    /// <param name="value"> string, value to send. </param>
    public static void SendKeysOnElement(string locator, string element, string value)
    {
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
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    public static void ValidateIsDisplayed(string locator, string element)
    {
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
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    public static void ValidateIsChecked(string locator, string element)
    {
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
    /// <param name="fileName"> string, file name with format of file. </param>
    /// <param name="sourceFilePath"> string, absolute path on computer. </param>
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
    /// <param name="pathOnDevice"> string, absolute path on mobile device. </param>
    /// <param name="pathOnComputer"> string, absolute path on computer. </param>
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

        int startY = (int)(screen_height * 0.8);
        int endY = (int)(screen_width * 0.2);
        int startX = (int)(screen_width / 2);
        int endX = endY;

        PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch, "finger");
        var sequence = new ActionSequence(finger);

        var move1 = finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.FromMilliseconds(100));
        var down = finger.CreatePointerDown(OpenQA.Selenium.Interactions.MouseButton.Left);
        var pause = finger.CreatePause(TimeSpan.FromMilliseconds(200));
        var move2 = finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, endY, TimeSpan.FromMilliseconds(100));
        var up = finger.CreatePointerUp(OpenQA.Selenium.Interactions.MouseButton.Left);

        sequence.AddAction(move1);
        sequence.AddAction(down);
        sequence.AddAction(pause);
        sequence.AddAction(move2);
        sequence.AddAction(up);

        var actions_seq = new List<ActionSequence>{
            sequence
        };
        _driver.PerformActions(actions_seq);
        Console.WriteLine("Driver: Swipe()");
    }

    /// <summary> Long press on element. </summary>
    /// <param name="locator"> string, locator. </param>
    /// <param name="element"> string, unique element. </param>
    public static void LongPress(string locator, string element)
    {
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

            var actions_seq = new List<ActionSequence>{
            sequence
        };
            _driver.PerformActions(actions_seq);
            Console.WriteLine("Driver: LongPress() on " + element);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }
    /// <summary> Navigate to URL. </summary>
    /// <param name="url"> string, URL to navigate to. </param>
    public static void NavigateToUrl(string url)
    {
        try
        {
            _driver.Navigate().GoToUrl(url);
            Console.WriteLine("Driver: Navigate to " + url);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: Could not navigate to " + url);
        }
    }
    /// <summary> Refresh the current page. </summary>
    /// <remarks> This method is useful for web testing scenarios. </remarks>
    public static void RefreshPage()
    {
        try
        {
            _driver.Navigate().Refresh();
            Console.WriteLine("Driver: Page refreshed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: Could not refresh the page.");
        }
    }

    /// <summary> Wait until element is visible. </summary>
    /// <param name="locator"> string, locator type (e.g., "id", "xpath"). </param>
    /// <param name="element"> string, unique element name. </param>
    /// <param name="timeoutInSeconds"> int, timeout in seconds. </param>
    public static void WaitTillElementIsVisible(string locator, string element, int timeoutInSeconds)
    {
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
        IWebElement el = null;
        try
        {
            if (locator.Equals("id"))
            {
                wait.Until(driver =>
                {
                    el = driver.FindElement(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                    Console.WriteLine("Driver: Element " + element + " is visible.");
                    return el.Displayed;
                });
            }
            if (locator.Equals("TagName"))
            {
                wait.Until(driver =>
                {
                    el = driver.FindElement(By.TagName(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                    Console.WriteLine("Driver: Element " + element + " is visible.");
                    return el.Displayed;
                });
            }
            if (locator.Equals("xpath"))
            {
                wait.Until(driver =>
                {
                    el = driver.FindElement(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                    Console.WriteLine("Driver: Element " + element + " is visible.");
                    return el.Displayed;
                });
            }
            if (locator.Equals("css"))
            {
                wait.Until(driver =>
                {
                    el = driver.FindElement(By.CssSelector(elementInfo.Invoke(repo, new[] { locator }).ToString()));
                    Console.WriteLine("Driver: Element " + element + " is visible.");
                    return el.Displayed;
                });
            }
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("Driver: Element " + element + " is not visible after " + timeoutInSeconds + " seconds.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Press a key code on the keyboard. </summary>
    /// <param name="key"> int, key to press (e.g., "search", "done"). 
    /// <see cref="https://developer.android.com/reference/android/view/KeyEvent"/>  </param>
    public static void PressKeyCode(int key)
    {
        try
        {
            _driver.PressKeyCode(key);
            Console.WriteLine("Driver: Pressed key " + key);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
            Console.WriteLine("Driver: Could not press key " + key);
        }
    }

    /// <summary> Get elements by locator. </summary>
    /// <param name="locator"> string, locator type (e.g., "id", "xpath"). </param>
    /// <param name="element"> string, unique element name. </param>
    /// <returns> List of IWebElement. </returns>
    public static List<IWebElement> GetElements(string locator, string element)
    {
        List<IWebElement> elements = new List<IWebElement>();
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        elements = new List<IWebElement>();
        return elements;
    }

    /// <summary> Click on element in list by index. </summary>
    /// <param name="locator"> string, locator type (e.g., "id", "xpath"). </param>
    /// <param name="element"> string, unique element name. </param>
    /// <param name="index"> int, index of element in list. </param>
    public static void ClickOnElementInList(string locator, string element, int index)
    {
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        List<IWebElement> elements = new List<IWebElement>();
        try
        {
            if (locator.Equals("id"))
            {
                elements = _driver.FindElements(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                elements[index].Click();
                Console.WriteLine("Driver: Click() on " + element + " in list at index " + index);
            }
            if (locator.Equals("xpath"))
            {
                elements = _driver.FindElements(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                elements[index].Click();
                Console.WriteLine("Driver: Click() on " + element + " in list at index " + index);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Get attribute value of element in list by index. </summary>
    /// <param name="locator"> string, locator type (e.g., "id", "xpath"). </param>
    /// <param name="element"> string, unique element name. </param>
    /// <param name="index"> int, index of element in list. </param>
    /// <param name="attributeName"> string, attribute to GET. </param>
    /// <returns> string, attribute value. </returns>
    public static string GetAttributeValueFromElementInList(string locator, string element, int index, string attributeName)
    {
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        List<IWebElement> elements = new List<IWebElement>();
        string actualValue = "";
        try
        {
            if (locator.Equals("id"))
            {
                elements = _driver.FindElements(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                actualValue = elements[index].GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element + " in list at index " + index);
            }
            if (locator.Equals("xpath"))
            {
                elements = _driver.FindElements(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                actualValue = elements[index].GetAttribute(attributeName);
                Console.WriteLine("Driver: GetAttribute(" + attributeName + ") of " + element + " in list at index " + index);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
        return actualValue;
    }

    /// <summary> Is element [N] in list displayed? </summary>
    /// <param name="locator"> string, locator type (e.g., "id", "xpath"). </param>
    /// <param name="element"> string, unique element name. </param>
    /// <param name="index"> int, index of element in list. </param>
    public static void ValidateIsDisplayedElementInList(string locator, string element, int index)
    {
        System.Reflection.MethodInfo elementInfo = repo.GetMethod(element);
        List<IWebElement> elements = new List<IWebElement>();
        try
        {
            if (locator.Equals("id"))
            {
                elements = _driver.FindElements(By.Id(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                if (elements[index].GetAttribute("displayed").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: Element " + element + " in list at index " + index + " is displayed.");
                }
                else
                {
                    Console.WriteLine("Driver: Element " + element + " in list at index " + index + " is not displayed.");
                }
            }
            if (locator.Equals("xpath"))
            {
                elements = _driver.FindElements(By.XPath(elementInfo.Invoke(repo, new[] { locator }).ToString())).Cast<IWebElement>().ToList();
                if (elements[index].GetAttribute("displayed").ToString().Equals("true"))
                {
                    Console.WriteLine("Driver: Element " + element + " in list at index " + index + " is displayed.");
                }
                else
                {
                    Console.WriteLine("Driver: Element " + element + " in list at index " + index + " is not displayed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }

    /// <summary> Start Android emulator. </summary>
    /// <param name="avdName"> string, name of the AVD (Android Virtual Device) to start <see cref="https://developer.android.com/studio/run/emulator-commandline?hl=en"/>. </param>
    /// <remarks> Ensure that the Android SDK's 'emulator' command is in your system PATH. </remarks>
    public static void StartEmulator(string avdName)
    {
        try
        {
            // 1. Check if emulator is already running
            System.Diagnostics.ProcessStartInfo adbCheckInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C adb devices",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string adbOutput = "";
            using (System.Diagnostics.Process adbProcess = System.Diagnostics.Process.Start(adbCheckInfo))
            {
                adbOutput = adbProcess.StandardOutput.ReadToEnd();
                adbProcess.WaitForExit();
            }

            if (adbOutput.Contains("emulator-5554"))
            {
                Console.WriteLine("An emulator is already running. Connecting to existing emulator.");
                return;
            }

            // 2. Start emulator if not running
            string userName = Environment.GetEnvironmentVariable("USERNAME");
            string emulatorPath = $@"C:\Users\{userName}\AppData\Local\Android\Sdk\tools\emulator.exe";

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = emulatorPath,
                Arguments = $"-avd {avdName}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine("ERROR: " + e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for emulator to boot (poll adb for device)
                bool booted = false;
                for (int i = 0; i < 20; i++) // Wait up to 20*5=100 seconds
                {
                    System.Threading.Thread.Sleep(5000);
                    using (System.Diagnostics.Process checkProcess = System.Diagnostics.Process.Start(adbCheckInfo))
                    {
                        string checkOutput = checkProcess.StandardOutput.ReadToEnd();
                        checkProcess.WaitForExit();
                        if (checkOutput.Contains("emulator-5554") && checkOutput.Contains("device"))
                        {
                            booted = true;
                            break;
                        }
                    }
                }

                if (!booted)
                {
                    process.Kill();
                    throw new Exception("Emulator did not start successfully.");
                }
            }

            Console.WriteLine($"Emulator {avdName} started and ready.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while starting the emulator: " + ex.Message);
            throw; // Stop the test by rethrowing the exception
        }
    }

    /// <summary> Close Android emulator. </summary>
    /// <remarks> Ensure that the Android SDK's 'adb' command is in your system PATH. </remarks>
    public static void CloseEmulator()
    {
        try
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C adb emu kill",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine("ERROR: " + e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Optionally, wait for the emulator to close
                Thread.Sleep(5000); // Wait for 5 seconds (adjust as necessary)
            }

            Console.WriteLine("Emulator closed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while closing the emulator: " + ex.Message);
        }
    }
}
