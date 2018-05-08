# HTTP Request Recorder for Static Resources
The demo shows how to record the calling status of an online JavaScript file.

## Prerequisites
Download and install [sqlite-netFx45-setup-x64-2012-1.0.108.0.exe](http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki) 
    
You may see the issue when running the project in Visual Studio:

![System.BadImageFormatException](http://www.codepool.biz/wp-content/uploads/2018/04/sqlite-BadImageFormatException.PNG)

The workaround is to check the following option in settings.

![use 64 bit version of IIS express](http://www.codepool.biz/wp-content/uploads/2018/04/iis-64bit.PNG)

## How to Use
1. Open **dist** folder in Visual Studio.
2. Add SQLite reference.
3. Build and publish the project to IIS.
4. Deploy the test folder to another web server.
5. Visit **index.htm** and then check the log file or db file generated under IIS website folder.

## Blog
[How to Capture HTTP Request of Static Files Deployed on IIS](http://www.codepool.biz/capture-http-request-static-files-iis.html)
