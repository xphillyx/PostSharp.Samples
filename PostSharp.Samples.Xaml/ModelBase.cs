using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

namespace PostSharp.Samples.Xaml
{
    [NotifyPropertyChanged]
    [Recordable(ProvideMethodContext = true)]
    public abstract class ModelBase
    {
        
    }


}