using System;

namespace BullsAndCows
{

    /// <summary>
    /// Это контейнер для методов игры "Быки и Коровы."
    /// </summary>
    class Program
    {

        /// <summary>
        /// Метод осуществляет вызов всех вспомогательных методов
        /// для реализации игры.
        /// </summary>
        static void Main()
        {
            // Загаданное компьютером число.
            string guessNumber = "";
            // Имя игрока.
            string playerName = "";
            // Длина загаданного числа. Вводится игроком.
            int numberLength = 0;
            // Счетчик количества попыток, за которое игрок угадывает число.
            int attemptCnt = 0;
            // Вывод на экран правил игры.
            GameDescription();
            // Инициализация значений для старта игры.
            InitNewGame(out guessNumber, out playerName, out numberLength, out attemptCnt);
            // Запуск процесса игры.
            ProcessGame(ref guessNumber, ref playerName, ref numberLength, ref attemptCnt);
        }

        private static void ProcessGame(ref string guessNumber, ref string playerName, ref int numberLength, ref int attemptCnt)
        {
            // Игра идет бесконечно, прерывание цикла возможно в случае, если игрок отгадал число.
            while (true)
            {
                // Увеличение количества попыток и считывание отгадываемого числа от игрока.
                attemptCnt++;
                Console.WriteLine("Попробуй угадать число:");
                string numberFromPlayer = Console.ReadLine();
                // Проверка введенного числа игрока на корректность.
                if (!CheckCorrectNumber(numberFromPlayer, numberLength))
                {
                    continue;
                }
                // Случай, когда игрок отгадал число. Предлагается либо продолжить, либо завершить игру.
                if (guessNumber == numberFromPlayer)
                {
                    // Заканчиваем игру и выходим из Main.
                    if (EndGame(playerName, attemptCnt))
                    {
                        return;
                    }
                    else
                    {
                        // Обновляем все переменные, начнинаем новую игру, пропускаем итерацию цикла while.
                        InitNewGame(out guessNumber, out playerName, out numberLength, out attemptCnt);
                        continue;
                    }
                }
                // Считаем количество отгаданных цифр (коров) и отгаданных правильных позиций цифр (быков) игроком.
                int guessDigits = CountGuessDigits(guessNumber, numberFromPlayer);
                int guessDigitsPlace = CountGuessDigitPlace(guessNumber, numberFromPlayer);
                // Вывод на экран: сколько коров и быков игрок отгадал.
                PrintCowsAndBulls(guessDigits - guessDigitsPlace, guessDigitsPlace);
            }
        }

        /// <summary>
        /// Метод принимает количество цифр, отгаданных игроком, 
        /// и количество цифр, отгаданных игроком, которые стоят на своих местах.
        /// Метод выводит на экран сообщение о том, сколько коров и быков
        /// было отгаданно с учетом падежных склонений.
        /// Также метод выводит случайную фразу для мотивации.
        /// </summary>
        /// <param name="guessDigits">
        /// Количество цифр, отгаданных игроком
        /// </param>
        /// <param name="guessDigitsPlace">
        /// Количество цифр на своих позициях
        /// </param>
        public static void PrintCowsAndBulls(int guessDigits, int guessDigitsPlace)
        {
            // В зависимости от количества коров выводится сообщение с правильными окончаниями.
            // 3 случая падежных окончаний.
            if (guessDigits == 1)
            {
                Console.Write($"{guessDigits} корова и ");
            }
            else if (guessDigits <= 4)
            {
                Console.Write($"{guessDigits} коровы и ");
            }
            else
            {
                Console.Write($"{guessDigits} коров и ");
            }
            // В зависимости от количества быков выводится сообщение с правильными окончаниями.
            // 3 случая падежных окончаний.
            if (guessDigitsPlace == 1)
            {
                Console.WriteLine($"{guessDigitsPlace} бык ");
            }
            else if (guessDigits <= 4)
            {
                Console.WriteLine($"{guessDigitsPlace} быка ");
            }
            else
            {
                Console.WriteLine($"{guessDigitsPlace} быков ");
            }
            // Массив мотивационных фраз. 
            string[] wishes = {"Не отчаивайся!", "У тебя получится :)",
            "Осталось немного!", "Ты почти у цели!", "Ты сможешь!", "Почти:)",
            "Еще чуть-чуть :)", "Немного не так :)", "Топим к мечте, брат!!!! Просто возьми и отгадай число!!!!"};
            // Берем случайный индекс в пределах массива фраз и выводим его на экран.
            Random rnd = new Random();
            Console.WriteLine(wishes[rnd.Next(0, wishes.Length)]);
            // Для красоты перевод строки.
            Console.WriteLine();
        }

        /// <summary>
        /// Метод считает, сколько игрок угадал цифр, стоящих на своих местах в загаданном числе.
        /// </summary>
        /// <param name="guessNumber">
        /// Загаданное компьютером число.
        /// </param>
        /// <param name="numberFromPlayer">
        /// Введенное игроком число в качестве отгадки.
        /// </param>
        /// <returns>
        /// Количество угаданных игроком цифр, стоящих на своих местах в загаданном числе.
        /// </returns>
        public static int CountGuessDigitPlace(string guessNumber, string numberFromPlayer)
        {
            // Счетчик количества цифр, которые угадал игрок, и которые стоят на своих местах в загаданном числе.
            int cntGuessDigitPlace = 0;
            // Проход по загаданному и введенному игроком числам и сравнение символов.
            for (int i = 0; i < guessNumber.Length; ++i)
            {
                if (guessNumber[i] == numberFromPlayer[i])
                {
                    // Увеличение счетчика отгаданных цифр.
                    cntGuessDigitPlace++;
                }
            }
            return cntGuessDigitPlace;
        }

        /// <summary>
        /// Метод считает, сколько игрок угадал цифр в загаданном числе.
        /// </summary>
        /// <param name="guessNumber">
        /// Загаданное компьютером число.
        /// </param>
        /// <param name="numberFromPlayer">
        /// Введенное игроком число в качестве отгадки.
        /// </param>
        /// <returns>
        /// Количество угаданных игроком цифр.
        /// </returns>
        public static int CountGuessDigits(string guessNumber, string numberFromPlayer)
        {
            // Счетчик количества цифр, которые угадал игрок в загаданном числе.
            int cntGuessDigits = 0;
            // Проход по загаданному числу и по введенному игроком числу, попарно сравниваем
            // все цифры и считаем, сколько цифр совпадает.
            for (int i = 0; i < numberFromPlayer.Length; ++i)
            {
                for (int j = 0; j < guessNumber.Length; ++j)
                {
                    // Если цифры равны, увеличиваем счетчик.
                    if (numberFromPlayer[i] == guessNumber[j])
                    {
                        cntGuessDigits++;
                        // Выход из цикла, т.к. дальше идти бесполезно.
                        break;
                    }
                }
            }
            return cntGuessDigits;
        }

        /// <summary>
        /// Метод обрабатывает концовку игры, предлагая либо завершить, либо начать новую игру.
        /// </summary>
        /// <param name="playerName">
        /// Имя игрока.
        /// </param>
        /// <param name="attemptCnt">
        /// Количество попыток, которые были потрачены игроком.
        /// </param>
        /// <returns>
        /// Возвращает true, если игрок завершает игру и false, если игрок хочет начать новую игру.
        /// </returns>
        public static bool EndGame(string playerName, int attemptCnt)
        {
            // Текстовые подсказки.
            Console.WriteLine($"Поздравляю тебя, {playerName}, ты угадал число всего за {attemptCnt} попыток!!!");
            Console.WriteLine("Для начала новой игры нажми ENTER");
            Console.WriteLine("Для выхода нажми Esc...");
            // Нажатая игроком клавиша.
            ConsoleKeyInfo keyToExit;
            // Обработка нажатых игроком клавиш. Возможны только 2 варианта.
            while (true)
            {
                // Считывание и обработка клавиши.
                keyToExit = Console.ReadKey();
                if (keyToExit.Key == ConsoleKey.Escape)
                {
                    return true;
                }
                if (keyToExit.Key == ConsoleKey.Enter)
                {
                    // Отделить новую игру.
                    Console.WriteLine("-----------------------------------");
                    return false;
                }
            }
        }

        /// <summary>
        /// Вывод на экран правил игры.
        /// </summary>
        public static void GameDescription()
        {
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.WriteLine("| Привет! Это игра быки и коровы. Смысл игры - угадать число, |");
            Console.WriteLine("|которое загадал компьютер. Загадонное число не может         |");
            Console.WriteLine("|содержать в себе повторяющиеся цифры или начинаться с нуля.  |");
            Console.WriteLine("|С каждой новой попыткой Вы пытаетесь его угадать,            |");
            Console.WriteLine("|осущетсвляя ввод предполагаемого Вами числа. В ответ на это, |");
            Console.WriteLine("|если Вы не угадали число, компьютер выводит сообщение о том, |");
            Console.WriteLine("|сколько цифр (коров) угадано, но расположено не на своих     |");
            Console.WriteLine("|местах, и сколько цифр (быков) угадано и находятся на        |");
            Console.WriteLine("|своих местах. Загаданное компьютером число положительное.    |");
            Console.WriteLine("|Желаю удачи!                                                 |");
            Console.WriteLine("+-------------------------------------------------------------+");
        }

        /// <summary>
        /// Метод принимает переменную для записи туда вводимой игроком длины загадываемого числа.
        /// Также осуществляется проверка входных данных на корректность.
        /// </summary>
        /// <param name="Number">
        /// Переменная для записи в нее желаемой игроком длины загадываемого числа.
        /// </param>
        public static void GetLengthFromPlayer(out int len)
        {
            // Введенное игроком число в типе строки.
            string stringNumber = "";
            // Считываем число, пока оно некорректно.
            do
            {
                // Считывание длины числа. Компьютером будет загадано число такой длины.
                Console.WriteLine("Введи длину числа в диапазоне от 1 до 10. Будет загадано число этой длины: ");
                stringNumber = Console.ReadLine();
                // Проверки на корректность введенного числа. Если число некорректно, предлагается ввести его снова.
                // Проверка на случай, если введенное число огромное.
                if (stringNumber.Length > 2)
                {
                    Console.WriteLine("Данные введены некорректно, повтори попытку.");
                    continue;
                }
                // Проверка на случай, если число нельзя превратить в число, или оно отрицательно,
                // или оно больше 10.
                if (!int.TryParse(stringNumber, out len) | len <= 0 | len > 10)
                {
                    Console.WriteLine("Данные введены некорректно, повтори попытку.");
                    continue;
                }
                // Если введенное число прошло все проверки на корректность, то выходим из цикла.
                break;

            } while (true);
        }

        /// <summary>
        /// Метод генерирует рандомное число в типе строки.
        /// Сгенерированное число не содержит в себе повторяющиеся цифры.
        /// Длина генерируемого числа от 1 до 10.
        /// </summary>
        /// <param name="len">
        /// Длина рандомного числа, которое сгенерируется.
        /// </param>
        /// <returns></returns>
        public static string GetRandomNumber(int len)
        {
            Random rnd = new Random();
            string digits = "0123456789";
            string randomNumber = "";
            bool[] usedDigits = new bool[10];

            do
            {
                int position = rnd.Next(0, 10);
                if (position == 0 && randomNumber.Length == 0)
                {
                    continue;
                }
                if (!usedDigits[position])
                {
                    randomNumber += digits[position];
                    usedDigits[position] = true;
                }
            } while (randomNumber.Length < len);

            return randomNumber;
        }

        /// <summary>
        /// Метод отвечает за начало игры: предлагает ввести имя игрока,
        /// а также вызывает вспомогательные методы для инициализации нужных
        /// переменных для новой игры.
        /// </summary>
        /// <param name="guessNumber">
        /// Загаданное компьютером число.
        /// </param>
        /// <param name="playerName">
        /// Имя игрока.
        /// </param>
        /// <param name="numberLength">
        /// Длина загаданного числа.
        /// </param>
        /// <param name="attemptCnt">
        /// Количество попыток, потраченных игроком.
        /// </param>
        public static void InitNewGame(out string guessNumber, out string playerName, out int numberLength, out int attemptCnt)
        {
            Console.WriteLine("Представься, пожалуйста :)");
            Console.WriteLine("Введи свое имя:");
            playerName = Console.ReadLine();
            GetLengthFromPlayer(out numberLength);
            guessNumber = GetRandomNumber(numberLength);
            attemptCnt = 0;
            Console.WriteLine();
        }

        /// <summary>
        /// Проверка вводимого игроком числа на корректность:
        /// оно должно подходить под условия игры.
        /// </summary>
        /// <param name="number">Введенное игроком число.</param>
        /// <param name="needLength">Необходимая длина вводимого игроком числа.</param>
        /// <returns>Возвращает true, если введенное число корректно, иначе false.</returns>
        public static bool CheckCorrectNumber(string number, int needLength)
        {
            if (number.Length != needLength)
            {
                Console.WriteLine("Твое число некорректно, повтори попытку.");
                Console.WriteLine("Возможно, тебе стоит перечитать правила игры :)");
                Console.WriteLine();
                return false;
            }
            if (!long.TryParse(number, out long temporaryLong) | temporaryLong <= 0)
            {
                Console.WriteLine("Твое число некорректно, повтори попытку.");
                Console.WriteLine("Возможно, тебе стоит перечитать правила игры :)");
                Console.WriteLine();
                return false;
            }
            if (number[0] == '0')
            {
                Console.WriteLine("Твое число некорректно, повтори попытку.");
                Console.WriteLine("Возможно, тебе стоит перечитать правила игры :)");
                Console.WriteLine();
                return false;
            }
            if (IsRepeatDigits(number))
            {
                Console.WriteLine("А я напоминаю, что загадонное число не может содержать в себе повторяющиеся цифры :)");
                Console.WriteLine();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод проверяет, есть ли в числе повторяющиеся цифры.
        /// </summary>
        /// <param name="number">Число, в котором нужно проверить наличие повторяющихся цифр.</param>
        /// <returns>Возвращает true, если в числе есть повторяющиеся цифры, иначе false.</returns>
        public static bool IsRepeatDigits(string number)
        {
            for (int i = 0; i < number.Length; ++i)
            {
                for (int j = 0; j < number.Length; ++j)
                {
                    if (i != j && number[i] == number[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
