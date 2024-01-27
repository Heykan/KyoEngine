using System;

namespace Kyo.Extension;

public static class ArrayExtension
{
    public static T Find<T>(this T[,] myArray, Predicate<T> match)
    {
        int rows = myArray.GetLength(0);
        int columns = myArray.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                T element = myArray[j, i];
                if (match(element))
                    return element;
            }
        }

        return default(T);
    }
}