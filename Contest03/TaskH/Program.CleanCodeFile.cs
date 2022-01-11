using System;
using System.IO;

partial class Program
{
    private static string[] ReadCodeLines(string codePath)
    {
        return File.ReadAllLines(codePath);
    }

    private static string[] CleanCode(string[] codeWithComments)
    {
        // �������� ����� ��� ������.

        using (File.Create("cleanCode.cs"))
        {

        }

        // ���������� ������������ ��� �������������� �����������.

        int transit = 0;

        // ������ ��� ������ ������ �� ������� �������, �� ��� ������ �����.

        string[] result = new string[codeWithComments.Length];

        // ������������ �����������.

        string oneComment = "//";

        // ������������� �����������.

        string multCommentStart = "/*";

        string multCommentEnd = "*/";

        // ������, � ������� ����� ���������� ������ ������� �� ������ ��������.

        string temp = "";

        for (int i = 0; i < codeWithComments.Length; i++)
        {
            temp = codeWithComments[i];

            // �������� ��� ������������� �����������.

            if (temp.TrimStart().StartsWith(oneComment))
            {
                codeWithComments[i] = " ";
            }

            // �������� ��� �������������� ����������� � ����� ������.

            if ((temp.TrimStart().StartsWith(multCommentStart)) && (temp.Trim().EndsWith(multCommentEnd)))
            {
                codeWithComments[i] = " ";
            }

            // �������� ��� �������������� ����������� � ������ �������.

            else if ((temp.TrimStart().StartsWith(multCommentStart)) && transit == 0)
            {
                transit = 1;
            }

            if (temp.Trim().EndsWith(multCommentEnd))
            {
                codeWithComments[i] = " ";
                transit = 0;
            }

            if (transit == 1)
            {
                codeWithComments[i] = " ";
            }
        }

        // �������� ������ ������� ��� ������ �����.

        result = Array.FindAll(codeWithComments, x => !string.IsNullOrEmpty(x.Trim()));

        return result;
    }

    private static void WriteCode(string codeFilePath, string[] codeLines)
    {
        File.WriteAllLines(codeFilePath, codeLines);
    }
}