This example demonstrates how to configure customize the name and GUID of the ETW event source.

The custom log source is implemented in the `MyEventSource` class. This class must fulfill the following requirements:

* Inherit the `PostSharpEventSource` class 
* Be annotated with the `EventSource` and `Guid` custom attributes.
* Contain a set of predefined methods with arbitrary name but with predefined signature and with an `Event` custom attribute
  with the exact predefined value of the `eventId` parameter. These methods exist for the sole purpose of defining the
  event metadata. They are never invoked directly.


An instance of the new event source class is passed to the constructor of `EventSourceLoggingBackend`.


## Viewing the log

To view the log produced by this example, you need to attach a log collector:

1. Download PerfView from https://www.microsoft.com/en-us/download/details.aspx?id=28567.

2. Execute: perfview.exe collect -OnlyProviders:*MyEventSource

3. Execute this program.

4. In PerfView, click 'Stop collecting', then in the PerfView tree view click 'PerfViewData.etl.zip' and finally 'Events'.


## Documentation

[Logging to the etw](http://doc.postsharp.net/etw)