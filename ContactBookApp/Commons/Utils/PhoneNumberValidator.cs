using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactBookApp.Commons.Utils
{
    static class PhoneNumberValidator
    {

        private static Regex phoneNumberPattern;


        /// <summary>
        ///  Pattern to validate Phonenumber
        /// </summary>
        public static Regex PhoneNumberPattern { get; set; }
        

        static PhoneNumberValidator()
        {
            PhoneNumberPattern = new Regex("[0-9]{10}");
        }


        /// <summary>
        /// Validate 10 digit Phonenumber using the predefined regex pattern 
        /// </summary>
        /// <param name="phoneNumber">
        /// Phonenumber to be validated
        /// </param>
        /// <returns>
        /// True if the phonenumber is valid, else false
        /// </returns>
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            return PhoneNumberPattern.IsMatch(phoneNumber);
        }

    }
}
