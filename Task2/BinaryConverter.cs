using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_1_Kulazhin
{
    static class BinaryConverter
    {
        // Total method to convert any decimal data.
        public static string ConvertToBinary(int sourceValue)
        {
            if (sourceValue == 0)
                return "0";

            switch (sourceValue)
            {
                case < 0: return ConvertNegative(sourceValue);
                default: return ConvertPositive(sourceValue);
            }
        }

        // Method to convert positive values.
        private static string ConvertPositive(int sourceValue)
        {
            string convertedValue = string.Empty;
            for (int i = 1; sourceValue != 0; i++)
            {
                if (i % 5 == 0)
                {
                    convertedValue += " ";
                    continue;
                }
                switch (sourceValue % 2)
                {
                    case 0: convertedValue += "0"; sourceValue /= 2; break;
                    case 1: convertedValue += "1"; sourceValue /= 2; break;
                }
            }

            convertedValue = ReverseString(convertedValue);
            return convertedValue;
        }

        // Method to convert negative values (it uses ConvertPostitve() and ConvertToDecimal() underhood).
        private static string ConvertNegative(int sourceValue)
        {
            string result = String.Format($"0{ConvertPositive(-sourceValue)}");
            char[] chresult = result.ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                switch(chresult[i])
                {
                    case '0': chresult[i] = '1'; break;
                    case '1': chresult[i] = '0'; break;
                }
            }

            int interimInt = ConvertToDecimal(chresult);
            interimInt++;

            result = ConvertPositive(interimInt);
            return result;
        }

        // This method is used in ConvertPostitve() to reverse output result.
        private static string ReverseString(string sourceString)
        {
            string result = string.Empty;

            for (int i = sourceString.Length - 1; i >= 0; i--)
            {
                result += sourceString[i];
            }

            return result;
        }

        // This method is used in COnvertNegative() to add 1 to inverted binary value.
        private static int ConvertToDecimal(char[] sourceValue)
        {
            int result = 0;
            for (int i = sourceValue.Length - 1, j = 1; i >= 0; i--, j *= 2)
            {
                if (sourceValue[i] == ' ')
                {
                    j /= 2;
                    continue;
                }

                int digit = int.Parse(sourceValue[i].ToString());
                result += j * digit;
            }

            return result;
        }
    }
}
