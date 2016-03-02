using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Model;

namespace PostSharp.Samples.Xaml
{
    public class CustomerModel: ModelBase
    {
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        [Child]
        public AdvisableCollection<AddressModel> Addresses { get; set; }

        [Reference]
        public AddressModel PrincipalAddress { get; set; }
    }
}
