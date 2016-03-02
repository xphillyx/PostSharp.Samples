using System.ComponentModel;
using System.Text;
using PostSharp.Patterns.Contracts;

namespace PostSharp.Samples.Xaml
{
    public class AddressModel : ModelBase
    {
        [DisplayName("Address Line 1")]
        [Required]
        public string Line1 { get; set; }

        [DisplayName("Address Line 2")]
        public string Line2 { get; set; }

        [Required]
        public string Town { get; set; }

        public string Country { get; set; }

        public string FullAddress
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (this.Line1 != null) stringBuilder.Append(this.Line1);
                if (this.Line2 != null)
                {
                    if (stringBuilder.Length > 0) stringBuilder.Append("; ");
                    stringBuilder.Append(this.Line2);
                }
                if (this.Town != null)
                {
                    if (stringBuilder.Length > 0) stringBuilder.Append("; ");
                    stringBuilder.Append(this.Town);
                }
                if (this.Country != null)
                {
                    if (stringBuilder.Length > 0) stringBuilder.Append("; "); 
                    stringBuilder.Append(this.Country);
                }

                return stringBuilder.ToString();
            }
        }
    }
}