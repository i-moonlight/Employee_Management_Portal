using System;
using System.IO;
using Serilog.Sinks.SystemConsole.Themes;

namespace WebAPI.Service.Authentication.Configurations.Logging
{
    /// <summary>
    /// Custom theme for logging.
    /// </summary>
    public static class LogTheme
    {
        public const string ConsoleOutputTemplate =
            "[{Timestamp:HH:mm:ss}] [{Level}] -> {Message:lj}{NewLine}{Exception}";

        public static CustomConsoleTheme Signal { get; } = new();
    }

    public class CustomConsoleTheme : ConsoleTheme
    {
        /// <summary>
        /// True if styling applied by the theme is written into the output,
        /// and can thus be buffered and measured.
        /// </summary>
        public override bool CanBuffer => false;

        /// <summary>
        /// Begin a span of text in the specified <paramref name="style"/>.
        /// </summary>
        /// <param name="output">Output destination.</param>
        /// <param name="style">Style to apply.</param>
        protected override int ResetCharCount => 0;

        /// <summary>
        /// Reset the output to un-styled colors.
        /// </summary>
        /// <param name="output">The output.</param>
        public override void Reset(TextWriter output)
        {
            Console.ResetColor();
        }

        /// <summary>
        /// The number of characters written by the <see cref="Reset(TextWriter)"/> method.
        /// </summary>
        public override int Set(TextWriter output, ConsoleThemeStyle style)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            if (style == ConsoleThemeStyle.LevelFatal || style == ConsoleThemeStyle.LevelError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            return 0;
        }
    }
}