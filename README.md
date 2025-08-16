<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/seriousQA/appium2-CSharp">
    <img src="logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center"> Mobile testing using Appium 2.0 and CSharp</h3>
  <p align="center">    
	Mobile (real devices and emulators) testing using Appium 2.0+, VS Code, CSharp and MSTest framework.  
    <br />
    <a href="https://github.com/seriousQA/appium2-CSharp"><strong>Explore the docs »</strong></a>
    <br />
   ·
    <a href="https://github.com/seriousQA/appium2-CSharp/issues">Report Bug</a>
    ·
    <a href="https://github.com/seriousQA/appium2-CSharp/issues">Request Feature</a>
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
      </ul>
    </li>
    <li><a href="#quick-start">Quick start</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
The tests are created in Visual Studio Code using MSTest (test framework for .NET applications).
<p align="left">
<img src="https://github.com/user-attachments/assets/3c1a48a6-4b5f-4cc7-9ae7-2adf557f1e00" alt="appium-2.0" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/csharp/csharp-original.svg" alt="sharp" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/dotnetcore/dotnetcore-original.svg" alt="net" height="50"/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/vscode/vscode-original.svg" alt="vscode" height="50" />
<img src="https://github.com/user-attachments/assets/0de8b7b2-afb9-48f5-9273-ce2fc793379a" alt="mstest" height="50"/>
</p>

AUT #1 is a native Huawei calculator (com.android.calculator2). Tested on real android device (
"locale":"de_DE",
"manufacturer":"HUAWEI",
"model":"ANE-LX1",
"platformVersion":"9",
).

AUT #2 is a com.android.settings. Tested on emulator-5554 (
"locale":"en_EN",
"platformName": "Android",
"platformVersion":"13",
).

### Built With
* Visual Studio Code: https://code.visualstudio.com
* dotnet: https://dotnet.microsoft.com
* appium (dotnet-client): https://github.com/appium/dotnet-client
<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started
### Prerequisites
Necessary tools and packages:
* Visual Studio Code with C# Dev Kit extension
* node
* npm
* jdk
* Appium 2.0+ with uiautomator2 driver
* Appium-Inspector
* appium-doctor (optional)

Used packages:
* Appium.WebDriver (e.g., 8.0.0)
* Newtonsoft.Json (e.g., 13.0.3)
<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- QUICK START -->
## Qick start
1) Open the project and build it.
2) Prepare an emulator with Android 13. Start it.
3) Go to section "Testing" and run EmulatorUnitTests module.

<!-- LICENSE -->
## License
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
Distributed under the Apache 2.0 License. See `LICENSE.txt` for more information.
<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact
Project Link: [link](https://github.com/seriousQA/appium2-CSharp)
<p align="right">(<a href="#readme-top">back to top</a>)</p>
