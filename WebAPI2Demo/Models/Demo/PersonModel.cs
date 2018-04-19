using System.ComponentModel.DataAnnotations;

namespace WebAPI2Demo.Models.Demo
{
	public class PersonModel
	{
		[Required]
		[StringLength(25)]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = true)]
		public string MiddleInitial { get; set; }

		[Required]
		public string LastName { get; set; }

		[Range(1, 108)]
		public int Age { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }

		[EmailAddress]
		public string EmailAddress { get; set; }

		[Compare("EmailAddress")]
		[EmailAddress]
		public string EmailAddressConfirmation { get; set; }

		[RegularExpression(@"^(?!\b(\d)\1+-(\d)\1+-(\d)\1+\b)(?!123-45-6789|219-09-9999|078-05-1120)(?!666 | 000 | 9\d{2})\d{3}-(?!00)\d{2}-(?!0{4})\d{4}$]")]
		public string Ssn { get; set; }
	}
}