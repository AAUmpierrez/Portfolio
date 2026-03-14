using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.ValueObjects.Users
{
    [ComplexType]
    public record Email
    {
        public string Value { get; set; }

        public Email(string value)
        {
            Value = value;
            Validate();
        }

        private bool ValidateCharacters()
        {
            bool result = false;
            bool isAt = false;
            bool isDot = false;
            int i = 0;

            if(Value.Contains("@"))isAt = true;
            if (Value.Contains(".")) isDot = true;
            
            
            if (isAt&&isDot) result = true;

            return result;
        }

        private void Validate()
        {
            if (!ValidateCharacters()) throw new Exception("Error. Email format incorrect");
        }
    }
}
