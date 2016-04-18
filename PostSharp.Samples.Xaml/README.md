# PostSharp.Samples.Xaml

You are to implement UI application that is quite simple in terms of visual complexity, thus it is perceived as simple by its users. However, users usually
have quite explicit expectations of application behavior that is common on their platform. You can immediatelly see how these expectations complicate a fairly 
simple application and add a load of unappreciated work.

PostSharp can help you to avoid this camouflaged complexity by implementing several complex patterns. These monsters made hair of many of your fellow programmers 
turn gray. You can see their basic usage in this example, a small WPF application.

NotifyPropertyChanged
---------------------

The user expects to see changes immediately after they make a change to the form. No more refresh button, that's definitely not in today.

You probably heard `INotifyPropertyChanged` interface and its extensive use in architectural patterns such as MVVM. You may even have implemented this interface
for several classes. It is pain. Sure, C# 6.0 relieved us of mentioning properties via literal string by instroducing `nameof` and similar features. It is still 
pain. There were and are few libraries that does a lot for you. Yet there is still pain.

PostSharp's `[NotifyPropertyChanged] aspects does all analysis in compile time, without making you code look ugly. It's also designed for multiple threads, tries to 
limit number of notifications and few more nice bits.

In the code of this example, you can see the `[NotifyPropertyChanged]` aspect applied to both a view and the view model to implement `INotifyPropertyChanged`. The 
aspect will raise change notifications for all properties without manual code, even for composite properties referencing children objects, like 
`CustomerViewModel.FullName`.


Undo/Redo
---------

When the user does something wrong, they expect a quick way to fix it. They are more advanced, they press `Ctrl+Z`. Nothing happens. Now they do the same thing
as their less advanced fellows; they search for a back button. There's none? It's your fault. There is? They changed their mind and need to redo the change. 
They cannot? Also your fault, every application has those.

PostSharp provides you with `Recordable` aspect that does exactly that. It records changes made to the object. You can group multiple changes into operations and
then go back and forth, restoring old state of the application.

In the code of this example, you can see the `[Recordable]` aspect applied on the `ModelBase` class. By inheritance, all model classes will get the aspect. We had 
to change collections to `AdvisableCollection` so that changes to these collections are also recorded (the UI does not demonstrate the ability to add or remove 
collections items).

`MainWindow.xaml` contains the Undo and Redo buttons provided by the `PostSharp.Patterns.Model.Controls` package.

The `MyOperationFormatter` class shows how to customize the name of operations as they appear under the Undo expandable button.  

Code Contracts
--------------

To be able to undo the wrong change they made, the user first need to see that they did something wrong. You can do that, sure, but doing this manually can be 
quite a hassle. And it pollutes the code.

PostSharp's Code Contracts allows you to simply add runtime checking and exception throwing with a single attribute, which is quite expressive and apparent on a 
first glance.

In the code of this example, you may see `[Required]` aspects to a few properties of the `CustomerModel` and `AddressModel` classes, which causes these properties
to throw an exception when someone attempts to set them to a null or empty string.

In `MainWindow.xaml`, we enabled the `ValidatesOnExceptions` feature of data bindings to display red borders when a property setter throws an exception.