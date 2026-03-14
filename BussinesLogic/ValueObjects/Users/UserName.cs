using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.ValueObjects.Users
{
    [ComplexType]
    public record UserName
    {
        private string Value { get; set; }

        public UserName(string value)
        {
            Value = value;
            Validate();
        }

        private bool ValidateCharacters()
        {
            bool result = false;
            bool upperOk = false;
            bool lowerOk = false;
            int i = 0;

            while(Value.Length > i  && !result)
            {
                char c = Value[i];

                if (char.IsLower(c))
                {
                    lowerOk = true;
                }else upperOk = true;
                i++;
            }

            if (upperOk && lowerOk) result = true;

            return result;
        }
        private void Validate()
        {
            if (!ValidateCharacters()) throw new Exception("Error. Name must contenin Upper and Lower letters");
        }
    }
}
