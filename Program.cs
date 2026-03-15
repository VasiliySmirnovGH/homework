// Домашнее задание №1: Реализация алгоритма поиска с опечатками
// Расстояние Дамерау-Левенштейна (алгоритм Вагнера-Фишера)

/// <summary>
/// Вычисление расстояния Дамерау-Левенштейна
/// </summary>
static int Distance(string str1Param, string str2Param)
{
    if ((str1Param == null) || (str2Param == null)) return -1;

    int str1Len = str1Param.Length;
    int str2Len = str2Param.Length;

    // Если хотя бы одна строка пустая,
    // возвращается длина другой строки
    if ((str1Len == 0) && (str2Len == 0)) return 0;
    if (str1Len == 0) return str2Len;
    if (str2Len == 0) return str1Len;

    // Приведение строк к верхнему регистру
    string str1 = str1Param.ToUpper();
    string str2 = str2Param.ToUpper();

    // Объявление матрицы
    int[,] matrix = new int[str1Len + 1, str2Len + 1];

    // Инициализация нулевой строки и нулевого столбца матрицы
    for (int i = 0; i <= str1Len; i++) matrix[i, 0] = i;
    for (int j = 0; j <= str2Len; j++) matrix[0, j] = j;

    // Вычисление расстояния Дамерау-Левенштейна
    for (int i = 1; i <= str1Len; i++)
    {
        for (int j = 1; j <= str2Len; j++)
        {
            // Эквивалентность символов, переменная symbEqual
            // соответствует m(s1[i],s2[j])
            int symbEqual = (
                (str1.Substring(i - 1, 1) ==
                 str2.Substring(j - 1, 1)) ? 0 : 1);

            int ins   = matrix[i, j - 1] + 1;             // Добавление
            int del   = matrix[i - 1, j] + 1;             // Удаление
            int subst = matrix[i - 1, j - 1] + symbEqual; // Замена

            // Элемент матрицы вычисляется
            // как минимальный из трёх случаев
            matrix[i, j] = Math.Min(Math.Min(ins, del), subst);

            // Дополнение Дамерау по перестановке соседних символов
            if ((i > 1) && (j > 1) &&
                (str1.Substring(i - 1, 1) == str2.Substring(j - 2, 1)) &&
                (str1.Substring(i - 2, 1) == str2.Substring(j - 1, 1)))
            {
                matrix[i, j] = Math.Min(matrix[i, j],
                    matrix[i - 2, j - 2] + symbEqual);
            }
        }
    }

    // Возвращается нижний правый элемент матрицы
    return matrix[str1Len, str2Len];
}

/// <summary>
/// Вывод расстояния Дамерау-Левенштейна в консоль
/// </summary>
static void WriteDistance(string str1Param, string str2Param)
{
    int d = Distance(str1Param, str2Param);
    Console.WriteLine($"'{str1Param}','{str2Param}' -> {d}");
}

// ---- Демонстрация работы ----
Console.WriteLine("=== Демонстрация алгоритма Вагнера-Фишера ===");
Console.WriteLine();

Console.WriteLine("Добавление одного символа в начало, середину и конец строки");
WriteDistance("пример", "1пример");
WriteDistance("пример", "при1мер");
WriteDistance("пример", "пример1");

Console.WriteLine("Добавление двух символов в начало, середину и конец строки");
WriteDistance("пример", "12пример");
WriteDistance("пример", "при12мер");
WriteDistance("пример", "пример12");

Console.WriteLine("Добавление трёх символов");
WriteDistance("пример", "1при2мер3");

Console.WriteLine("Транспозиция");
WriteDistance("прИМер", "прМИер");

Console.WriteLine("Рассмотренный ранее пример");
WriteDistance("ИВАНОВ", "БАННВО");

Console.WriteLine();
Console.WriteLine("=== Интерактивный режим ===");
Console.WriteLine("Введите две строки для вычисления расстояния.");
Console.WriteLine("Для выхода введите 'exit' в качестве первой строки.");
Console.WriteLine();

// ---- Вечный цикл ввода ----
while (true)
{
    Console.Write("Введите первую строку: ");
    string? input1 = Console.ReadLine();

    if (input1 != null && input1.ToLower() == "exit")
    {
        Console.WriteLine("Выход из программы.");
        break;
    }

    Console.Write("Введите вторую строку: ");
    string? input2 = Console.ReadLine();

    if (input1 == null || input2 == null)
    {
        Console.WriteLine("Ошибка ввода. Попробуйте ещё раз.");
        continue;
    }

    int result = Distance(input1, input2);
    Console.WriteLine($"Расстояние Дамерау-Левенштейна между '{input1}' и '{input2}': {result}");
    Console.WriteLine();
}
