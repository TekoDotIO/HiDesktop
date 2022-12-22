# HiDesktop - 轻量级的桌面小组件仓库
## HiDesktop - A lightweight desktop-widgets repository.

#项目概况

欢迎使用 HiDesktop . 这是一款轻量级的桌面小组件仓库。本项目使用.NET C#进行开发，小组件是使用WinForm技术开发而成的。本项目旨在通过极小的资源占用量、简易的操作、较高的自由度、小巧的应用程序体积等，为电子白板以及桌面级设备提供更好的小组件体验。


#跨平台特性

HiDesktop基于C# WinForm技术进行开发，理论上可借助Mono等辅助工具实现跨平台。我们的开发人员并未对此进行测试与适配。因此建议用户在原生Windows系统环境下编译运行。


#程序框架

.NET 6.0


#已经实现的功能

-倒计时功能

-文本标签功能

-配置文件功能

-屏幕保护程序

-开机自启动


#已经列入计划的功能

- [ ] -课程表功能
- [ ] -便签功能
- [ ] -天气功能
- [ ] -控制悬浮窗功能
- [ ] -程序设置UI
- [ ] -右键菜单与热重载
- [ ] -桌面启动菜单功能


#关于配置文件

此项目的配置文件采取“*.properties”配置文件的格式。一般这些配置文件位于“Properties“文件夹内

例如：
```
key=value
```
等号左边为设置项的键，右边为设置项的值。上方配置文件所示的设置项key被设定为value。

常见设置项的释义对照表：

**fontSize** 字号。应为一个数字。

**allowMove** 是否允许移动。应为true或false。

**…_Color** 某个控件的表面颜色。应为一个十六进制颜色代码。如“#FF0000”。

**refreshTime** 刷新间隔。应为一个数字。单位是毫秒。如“refreshTime=500”表示每0.5秒刷新一次。

**opacity** 透明度。应为0～1间的一个小数。如“opacity=0.5”表示透明度为50%。

**location**窗体最左上角位置。通常为一个用英文逗号隔开的数对。单位是“像素”。如“location=400,1000”表示窗口的左上角像素位于屏幕的x=400，y=1000处。当location被设置为auto时可恢复程序设定的位置默认值。一般此设置项无需手动更改。

**type** 用于指示配置文件所对应的程序类型。一般无需手动更改。如果需要新建小组件，手动添加此项即可。程序运行将会自动补全剩余内容为默认值。

**enabled** 是否启用该配置文件。当值为“true”，此配置文件对应的组件将会被加载，当值为“false”则不会被加载。

**topMost** 是否置顶小组件。当值为“true”，小组件将被固定在所有窗口顶端，当值为“false”则不会。

**font** 字体。一般是一个指向某个ttf或otf字体文件的路径。
**text** 需要显示的文本。

#关于开源许可

开发方使用Apache2.0开源协议对HiDesktop进行开源。


#关于开发者团队

HiDesktop开发团队非常高兴能听取各方意见和建议，如果您对我们的代码有任何疑问，或者有任何的改进建议，欢迎您通过以下联系方式与我们共同探讨！我们不一定24小时在线，但是我们一定会在看到消息后第一时间考虑，在考虑周全后尽快回复。 

微信：EachFengye1003 

QQ：3072554288 

Telegram：@Fengye1003





#ProjectOverview

Welcome to HiDesktop . This is a lightweight desktop widget repository. NET C# and the widgets are developed using WinForm technology. The project aims to provide a better widget experience for whiteboards and desktop-level devices through a very small resource footprint, easy operation, high freedom, and small application size.


#CrossPlatformFeatures

HiDesktop is developed based on C# WinForm technology and can theoretically be cross-platform with the help of auxiliary tools such as Mono. This has not been tested and adapted by our developers. Therefore, we recommend that you compile and run it in a native Windows environment.


#ApplicationFramework

.NET 6.0


#FeaturesImplemented

-Countdown timer

-Text label function

-Profile function

-Screen saver

-Power-on self-start


#ScheduledFunctions

- [ ]  -Curriculum function
- [ ] -Sticky note function
- [ ] -Weather function
- [ ] -Control hover window function
- [ ] -Program Settings UI
- [ ] -Right-click menu and hot reload
- [ ] -Desktop launch menu function


#AboutTheConfigurationFile

The configuration files for this project are in the format of "*.properties" configuration files. Generally these configuration files are located in the "Properties" folder

For example
```
key=value
```
The left side of the equal sign is the key of the setting item, and the right side is the value of the setting item. The setting item key shown in the configuration file above is set to value.

Cross-reference table of common setting items' interpretations.

**fontSize** Font size. It should be a number.

**allowMove** Whether to allow movement. Should be true or false.

**..._Color** The color of the surface of a control. Should be a hexadecimal color code. For example, "#FF0000".

**refreshTime** Refresh interval. Should be a number. The unit is milliseconds. For example, "refreshTime=500" means refresh every 0.5 seconds.

**opacity** Transparency. It should be a decimal number between 0 and 1. For example, "opacity=0.5" means the transparency is 50%.

**location** The position of the top-left corner of the form. Usually a comma-separated number pair. The unit is "pixels". For example, "location=400,1000" means the upper-left corner of the window is located at x=400 and y=1000 of the screen. When the location is set to auto, the default value of the programmed location is restored. Normally this setting does not need to be changed manually.

**type** is used to indicate the type of program that the profile corresponds to. Usually no manual change is required. If you want to create a new widget, you can add this item manually. The program will automatically complete the rest of the content as default when it runs.

**enabled** Enables or disables this profile. When the value is "true", the component corresponding to this profile will be loaded, when the value is "false", it will not be loaded.

**topMost** Indicates if the widget will be top-mounted. When the value is "true", the widget will be fixed at the top of all windows, when the value is "false", it will not.

**font** The font. Usually a path to a ttf or otf font file.
**text** The text to be displayed.

#AboutOpenSourceLicense

The developer uses the Apache2.0 open source protocol for HiDesktop open source. 


#AboutTheDeveloperTeam

HiDesktop development team is very happy to listen to comments and suggestions, if you have any questions about our code, or any suggestions for improvement, you are welcome to discuss with us through the following contacts! We may not be online 24 hours a day, but we will consider the message as soon as we see it, and reply as soon as possible after thorough consideration. 

WeChat：EachFengye1003 

QQ：3072554288 

Telegram：@Fengye1003
