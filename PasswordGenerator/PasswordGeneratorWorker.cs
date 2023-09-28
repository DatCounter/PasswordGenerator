using WindowsInput;
using WindowsInput.Native;

namespace PasswordGenerator
{
    public class PasswordGeneratorWorker : BackgroundService
    {
        private readonly IKeyboardSimulator               _keyboardSimulator;
        private readonly ILogger<PasswordGeneratorWorker> _logger;
        private readonly Random                           _random;
        //Необходимо вынести это в конфигурацию
        private          int                              _lenghtOfPassword = 16;

        public PasswordGeneratorWorker(ILogger<PasswordGeneratorWorker> logger)
        {
            _logger            = logger;
            _keyboardSimulator = new InputSimulator().Keyboard;
            _random            = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PasswordGeneratorWorker is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                //Опция выбора быстрых клавиш
                if (KeyboardHelper.IsKeyDown(VirtualKeyCode.SHIFT) && KeyboardHelper.IsKeyDown(VirtualKeyCode.VK_P))
                {
                    // Генерация случайного пароля
                    string randomPassword = GenerateRandomPassword();

                    // Вставка пароля и стирание P
                    _keyboardSimulator.KeyPress(VirtualKeyCode.BACK);
                    _keyboardSimulator.TextEntry(randomPassword);

                    _logger.LogInformation($"Generated and inserted password: {randomPassword}");
                }

                await Task.Delay(100, stoppingToken); // Пауза для опроса клавиш
            }
        }

        private string GenerateRandomPassword()
        {
            string rndPassword = "";
            string chars = "+-/*!&$#?=@<>abcdefghijklnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < _lenghtOfPassword; i++)
            {
                rndPassword += chars[_random.Next(0, chars.Length)];
            }
            return rndPassword;
        }
    }

}
