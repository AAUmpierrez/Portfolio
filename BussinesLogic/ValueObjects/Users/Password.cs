using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.ValueObjects.Users
{
    [ComplexType]
    public class Password
    {
        public string Value { get; set; }


        public Password(string value) { 
            Value = value;
            Validate();
        }

        private bool ValidateCharacters()
        {
            bool isLetter = false;
            bool isDigit = false;
            bool isUppercase = false;
            bool isLowercase = false;
            bool isSpecialChar = false;
            bool result = false;

            int i = 0;

            while(Value.Length> i && !result)
            {
                char c = Value[i];

                if (char.IsLetter(c))
                {
                    isLetter = true;
                    if(char.IsLower(c))isLowercase = true;
                    else isUppercase = true;
                }
                else isDigit = true;
                if(char.IsPunctuation(c) || char.IsSymbol(c)) isSpecialChar = true;

                if(isLetter && isLowercase&& isUppercase && isSpecialChar&&isDigit) result = true;

                i++;

            }


            return result;
        }

        private void Validate() 
        {
            if (!ValidateCharacters()) throw new Exception("Error. Remember contains upper, lower cases, a digit and special char");
        }


    }
}
