# PostSharp.Samples.Xaml

This example demonstrates several patterns in a small XAML application:

* Undo/Redo
* NotifyPropertyChanged
* Code Contracts


NotifyPropertyChanged
---------------------

We applied the `[NotifyPropertyChanged]` aspect applied to both a view and the view model to implement `INotifyPropertyChanged`. The aspect
will raise change notifications for all properties without manual code, even for composite properties referencing children objects, like 
`CustomerViewModel.FullName`.


Undo/Redo
---------

We applied the `[Recordable]` aspect to the `ModelBase` class and therefore, by inheritance, to all model classes. We had to change collections
to `AdvisableCollection` so that changes in these collections are also recorded (however the UI does not demonstrate the ability to add or remove collections items).

`MainWindow.xaml` contains the Undo and Redo buttons provided by the `PostSharp.Patterns.Model.Controls` package.

The `MyOperationFormatter` class shows how to customize the name of operations as they appear under the Undo expandable button.

  

Code Contracts
--------------

We added the `[Required]` aspects to a few properties of the `CustomerModel` and `AddressModel` classes, which causes these properties
to throw an exception when someone attempts to set them to a null or empty string.

In `MainWindow.xaml`, we enabled the `ValidatesOnExceptions` feature of data bindings to display red borders when a property setter throws an exception.