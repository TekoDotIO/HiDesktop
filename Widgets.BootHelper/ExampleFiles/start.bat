@echo off
color 0a
title Loading...
echo 请稍后，正在启动...

taskkill /f /im onedrive.exe
cd d:\
d:
cd .\whelper\win-x64\
widgets.boothelper.exe
cd ..
cd ..