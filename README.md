# NLog.Pushbullet
Custom NLog target with Pushbullet integration.
## Configuration

Simply add it as an extension in the config file and setup the target as normal.
```xml
<nlog> 
  <extensions> 
    <add assembly="NLog.Pushbullet"/> 
  </extensions> 
  <targets> 
    <target name="pushbullet" type="Pushbullet" accessToken="12345" noteTitle="Message from NLog on ${machinename}"/>
  </targets> 
  <rules> 
    <logger name="*" minLevel="Error" appendTo="pushbullet"/>
  </rules> 
</nlog>
```
Available on [NuGet](https://www.nuget.org/packages/NLog.Pushbullet/) for easy dependency management.