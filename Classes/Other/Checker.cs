using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace MedicalDatabaseApplication
{
    class Checker
    {

        private string names = @"^[a-zA-Z]{3,}$";
        private string address = @"^\d{1,}\s\w{1,}\s\w{1,}$";
        private string phone = @"^((\+61\s?)?(\((0|02|03|04|07|08)\))?)?\s?\d{1,4}\s?\d{1,4}\s?\d{0,4}$";
        private string medicare = @"^^(\d{4}[ ]?\d{5}[ ]?\d)|(\d{9}\s?\d{1})$$";
        private string mrn = @"^\w{3}\d{10}$";


        public Checker()
        {

        }

        // Sees If The Name Is Actually A Name
        public Match CheckName(string input)
        {
            return Regex.Match(input, names);
        }

        // Sees If The Address is Actually an Address
        public Match CheckAddress(string input)
        {
            return Regex.Match(input, address);
        }

        // Makes Sure The Phones Are Actual Phone Numbers
        public Match CheckPhone(string input)
        {
            return Regex.Match(input, phone);
        }

        // Makes Sure The Medicare Number Has An Identifier & Is Actually A Medicare Number
        public Match CheckMedicare(string input)
        {
            return Regex.Match(input, medicare);
        }

        // Checks To See If The Medical Registration Number Is Actually A MRN
        public Match CheckMRN(string input)
        {
            return Regex.Match(input, mrn);
        }
    }
}
