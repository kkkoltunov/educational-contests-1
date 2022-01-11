using System;
using System.IO;

partial class Program
{
    private static string GetTextFromFile(string inputPath)
    {
        string text = "";

        using (StreamReader sr = new StreamReader(inputPath))
        {
            text = sr.ReadToEnd();
        }

        return text;
    }

    private static int GetSumFromText(string text)
    {
        // ����� ����.

        int count = 0;

        // ������, � ������� ����� �������� ������ ��� ������ ��������.

        string res = "";

        // ����, ������� ���������� ����� ������ ��� ������ ��������.

        for (int i = 0; i < text.Length; i++)
        {
            if ((text[i] == ',') || (text[i] == '.') || (text[i] == '!') || (text[i] == '?') || (text[i] == '\n'))
            {
                res += " ";
            }
            if ((text[i] != ',') && (text[i] != '.') && (text[i] != '!') && (text[i] != '?') && (text[i] != '\n'))
            {
                res += text[i];
            }
        }

        // ������ �� ���� ��������� ������.

        string[] textString = res.Split(' ');

        int n;

        // ������� �����.

        for (int i = 0; i < textString.Length; i++)
        {
            if (int.TryParse(textString[i], out n))
            {
                count += n;
            }
        }

        return count;
    }
}