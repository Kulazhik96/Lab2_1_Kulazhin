using System;

namespace Lab2_1_Kulazhin
{
    public static class NewtonMethod
    {
        // Find first integer power-th root that is LOWER than source value.
        private static double GetFirstApproximation(double num, double pow)
        {
            double firstApprox = 1.0; // Start searching first approximation from 1.
            while (Math.Pow(firstApprox, pow) <= num)
            {
                firstApprox++;
            }

            // When firstApprox becomes greater than source value, decrement firstApprox.
            return --firstApprox;
        }

        // Use Newton's method.
        public static double GetRoot(double num, double pow, double accuracy)
        {
            // Check for standard situations.
            bool isChanged = false;
            CheckValues(ref num, ref pow, ref isChanged);
            if (isChanged)
            {
                return num;
            }

            double absNum = Math.Abs(num);
            double firstApprox = GetFirstApproximation(absNum, pow);
            double result = firstApprox;

            for (int i = 0; i < accuracy; i++)
            {
                result = (((pow - 1) * result) + (absNum / Math.Pow(result, pow - 1))) / pow;
            }

            return num < 0 ? -result : result;
        }

        // Check for some statndard situations: pow is 0 or 1; pow is < 0; even pow from negative root.
        private static void CheckValues(ref double num, ref double pow, ref bool isChanged)
        {
            if (pow == 0)
            {
                num = 1;
                isChanged = true;
            }
            if (pow == 1)
            {
                isChanged = true;
            }
            if (pow % 2 == 0 && num < 0)
            {
                throw new ArgumentException("Can't take even root from negative value!");
            }
            if (pow < 0)
            {
                throw new ArgumentException("Power must be greater than zero!");
            }
        }
    }
}
