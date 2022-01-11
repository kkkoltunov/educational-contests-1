using System;
using System.Collections.Generic;
using System.IO;

partial class Program
{
    private static bool ValidateQuery(string query, out string[] queryParameters)
    {
        // ����� ������.

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        // ������ �� ������, ������� �������� ��� �����.

        queryParameters = query.ToLower().Split(' ');

        // ����������, ������� ������������ ��� �������� ������������ �����.

        bool flagFirst = false;
        bool flagSecond = false;
        bool flagThird = false;

        bool result = false;

        // ���������� ��� ����������� string � double.

        double thirdParam = 0;

        // ������� � ���������� ���������� �������.

        string[] titleText = { "first_name", "last_name", "group" };
        string[] titleNums = { "rating", "gpa" };

        if (queryParameters.Length == 3)
        {
            // �������� �� "first_name", "last_name", "group".

            for (int i = 0; i < titleText.Length; i++)
            {
                if ((queryParameters[0] == titleText[i]) && (queryParameters[1] == "==" || queryParameters[1] == "<>"))
                {
                    flagFirst = true;
                }
            }

            // �������� �� "rating", "gpa".

            for (int i = 0; i < titleNums.Length; i++)
            {
                if ((queryParameters[0] == titleNums[i]) && (queryParameters[1] == ">=" || queryParameters[1] == "<="))
                {
                    flagSecond = true;
                }
            }

            if (queryParameters[0] == titleNums[0] || queryParameters[0] == titleNums[1])
            {
                if (double.TryParse(queryParameters[2], out thirdParam))
                {
                    flagThird = true;
                }
            }
        }

        // ��������� �������� �� ������������.

        if ((flagFirst == true) || ((flagSecond == true) && (flagThird == true)))
        {
            result = true;
        }
        else
        {
            File.WriteAllText("queryResult.txt", "Incorrect input");
        }

        return result;
    }

    private static List<string> ProcessQuery(string[] queryParameters, string pathToDatabase)
    {
        // ������ �����, ������� ������� �� ����� ���� ������.

        string[] data = File.ReadAllLines(pathToDatabase);

        // ���������� ��� ����������� �� string � int.

        int ratingBase = 0;
        int ratingInput = 0;

        // ���������� ��� ����������� �� string � double.

        double gpaBase = 0;
        double gpaInput = 0;

        // ����, ������� ����������� ������ �� ���� ������.

        List<string> result = new List<string>();

        for (int i = 1; i < data.Length; i++)
        {
            // ��������� ������ ������ �� ��������� ��������� (�� ;).

            string[] temp = data[i].ToLower().Split(';');

            // �������� �� "first_name".

            if (queryParameters[0] == "first_name" && queryParameters[1] == "==")
            {
                if (queryParameters[2] == temp[0])
                {
                    result.Add(data[i]);
                }
            }
            if (queryParameters[0] == "first_name" && queryParameters[1] == "<>")
            {
                if (queryParameters[2] != temp[0])
                {
                    result.Add(data[i]);
                }
            }

            // �������� �� "last_name".

            if (queryParameters[0] == "last_name" && queryParameters[1] == "==")
            {
                if (queryParameters[2] == temp[1])
                {
                    result.Add(data[i]);
                }
            }
            if (queryParameters[0] == "last_name" && queryParameters[1] == "<>")
            {
                if (queryParameters[2] != temp[1])
                {
                    result.Add(data[i]);
                }
            }

            // �������� �� "group".

            if (queryParameters[0] == "group" && queryParameters[1] == "==")
            {
                if (queryParameters[2] == temp[2])
                {
                    result.Add(data[i]);
                }
            }
            if (queryParameters[0] == "group" && queryParameters[1] == "<>")
            {
                if (queryParameters[2] != temp[2])
                {
                    result.Add(data[i]);
                }
            }

            // �������� �� "rating".

            if (queryParameters[0] == "rating" && queryParameters[1] == ">=")
            {
                int.TryParse(queryParameters[2], out ratingInput);
                int.TryParse(temp[3], out ratingBase);

                if (ratingInput <= ratingBase)
                {
                    result.Add(data[i]);
                }
            }
            if (queryParameters[0] == "rating" && queryParameters[1] == "<=")
            {
                int.TryParse(queryParameters[2], out ratingInput);
                int.TryParse(temp[3], out ratingBase);

                if (ratingInput >= ratingBase)
                {
                    result.Add(data[i]);
                }
            }

            // �������� �� "gpa".

            if (queryParameters[0] == "gpa" && queryParameters[1] == ">=")
            {
                double.TryParse(queryParameters[2], out gpaInput);
                double.TryParse(temp[4], out gpaBase);

                if (gpaInput <= gpaBase)
                {
                    result.Add(data[i]);
                }
            }
            if (queryParameters[0] == "gpa" && queryParameters[1] == "<=")
            {
                double.TryParse(queryParameters[2], out gpaInput);
                double.TryParse(temp[4], out gpaBase);

                if (gpaInput >= gpaBase)
                {
                    result.Add(data[i]);
                }
            }
        }

        return result;
    }
}