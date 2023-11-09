using System;
using Microsoft.EntityFrameworkCore;

namespace EFTutorial.Services
{
	public class Helper
	{
        //Convert a string into Title Case, in which the first letter of each word is capitalized.
        public static string ConvertTitle(string text)
        {
            string finalText = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0 || text[i - 1] == ' ') //capitalizes first letter in string and any letter following a blank space.
                    finalText += Char.ToUpper(text[i]);
                else
                    finalText += Char.ToLower(text[i]);
            }
            return finalText;
        }

        public static int GetIntInRange(string prompt, int bottom, int top)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    var userNumber = Convert.ToInt32(Console.ReadLine());
                    if (userNumber < bottom || userNumber > top)
                    {
                        Console.WriteLine($"Please enter a valid number.");
                        continue;
                    }
                    return userNumber;
                }
                catch
                {
                    Console.WriteLine($"Please enter a valid number between {bottom} and {top}.");
                }
            }
        }

    }
}